using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace CubeMVC
{
    public class InventoryView : MonoBehaviour
    {
        [SerializeField] 
        private GameObject panelDrop;

        [SerializeField]
        private ContextProvider _contextProvider;

        private Text _itemPickedText;
        private InventoryModel _inventoryModel;

        private void Start()
        {
            _itemPickedText = panelDrop.GetComponentInChildren<Text>();

            _inventoryModel = _contextProvider.GetContext().InventoryModel;
            _inventoryModel.OnItemChangedUICallback += UpdateUI;
            _inventoryModel.OnItemChangedCallback += PickedUp;
        }

        private void UpdateUI()
        {
            InventorySlot[] slots = GetComponentsInChildren<InventorySlot>();

            for (int i = 0; i < slots.Length; i++)
            {
                if (i < _inventoryModel.Items.Count)
                {
                    slots[i].AddItem(_inventoryModel.Items[i]);
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
            if (_inventoryModel.Items.Count > 0)
                _itemPickedText.text = $"Предмет <color=purple>{_inventoryModel.Items[^1].name}</color> был подобран";
            yield return new WaitForSeconds(2.5f);
            panelDrop.SetActive(false);
        }
    }
}
