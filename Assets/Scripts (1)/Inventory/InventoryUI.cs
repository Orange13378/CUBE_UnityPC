using Leopotam.EcsLite;
using UnityEngine;
using static UnityEditor.Progress;

namespace CubeECS
{
    public class InventoryUI : MonoBehaviour
    {
        private EcsWorld _ecsWorld;
        private InventoryComponent _inventory;

        private void Start()
        {
            _ecsWorld = EcsWorldManager.GetEcsWorld();
            var filter = _ecsWorld.Filter<InventoryComponent>().End();
            var inventoryPool = _ecsWorld.GetPool<InventoryComponent>();

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
