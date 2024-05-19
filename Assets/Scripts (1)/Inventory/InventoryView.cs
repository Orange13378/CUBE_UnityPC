using UnityEngine;

namespace CubeECS
{
    public class InventoryView : MonoBehaviour
    {
        private InventoryComponent _inventory;

        private void Start()
        {
            var ecsWorld = EcsWorldManager.GetEcsWorld();
            var filter = ecsWorld.Filter<InventoryComponent>().End();
            var inventoryPool = ecsWorld.GetPool<InventoryComponent>();

            foreach (var entity in filter)
            {
                ref var inventoryComponent = ref inventoryPool.Get(entity);
                _inventory = inventoryComponent;
                _inventory.OnItemChangedCallback += UpdateUI;
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
    }
}
