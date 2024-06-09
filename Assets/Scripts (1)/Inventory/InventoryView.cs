using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace CubeECS
{
    public class InventoryView : MonoBehaviour
    {
        [SerializeField] 
        private GameObject panelDrop;

        private InventoryComponent _inventory;
        private Text _itemPickedText;

        private void Start()
        {
            var ecsWorld = EcsWorldManager.GetEcsWorld();
            var filter = ecsWorld.Filter<InventoryComponent>().End();
            var inventoryPool = ecsWorld.GetPool<InventoryComponent>();
            _itemPickedText = panelDrop.GetComponentInChildren<Text>();

            foreach (var entity in filter)
            {
                ref var inventoryComponent = ref inventoryPool.Get(entity);
                inventoryComponent.OnItemChangedUICallback += UpdateUI;
                inventoryComponent.OnItemChangedCallback += PickedUp;
                _inventory = inventoryComponent;
            }
        }

        private void UpdateUI()
        {
            InventorySlot[] slots = GetComponentsInChildren<InventorySlot>();

            for (int i = 0; i < slots.Length; i++)
            {
                if (i < _inventory.Items.Count)
                {
                    slots[i].AddItem(_inventory.Items[i]);
                }
                else
                {
                    slots[i].ClearSlot();
                }
            }
        }

        private void PickedUp()
        {
            StartCoroutine(ItemPickedUp());
        }


        private IEnumerator ItemPickedUp() 
        {
            panelDrop.SetActive(true);
            if (_inventory.Items.Count > 0)
                _itemPickedText.text = $"Предмет <color=purple>{_inventory.Items[^1].name}</color> был подобран";
            yield return new WaitForSeconds(2.5f);
            panelDrop.SetActive(false);
        }
    }
}
