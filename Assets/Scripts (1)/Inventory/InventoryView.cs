using Leopotam.EcsLite;
using UnityEngine;

namespace CubeECS
{
    public class InventoryView : MonoBehaviour
    {
        private EcsWorld _ecsWorld;
        private InventoryComponent _inventory;

        public void Construct(EcsWorld world)
        {
            _ecsWorld = world;
        }

        private void Start()
        {
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
