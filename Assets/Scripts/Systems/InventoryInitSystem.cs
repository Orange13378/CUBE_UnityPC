using System.Collections.Generic;
using Leopotam.EcsLite;

namespace CubeECS
{
    public class InventoryInitSystem : IEcsInitSystem
    {
        private EcsFilter _filter;
        private EcsPool<InventoryComponent> _inventoryPool;

        public void Init(IEcsSystems systems)
        {
            var ecsWorld = systems.GetWorld();

            var inventoryEntity = ecsWorld.NewEntity();

            _inventoryPool = ecsWorld.GetPool<InventoryComponent>();
            _inventoryPool.Add(inventoryEntity);

            _filter = systems.GetWorld().Filter<InventoryComponent>().End();

            foreach (var entity in _filter)
            {
                ref var inventoryComponent = ref _inventoryPool.Get(entity);
                inventoryComponent.Items = new List<Item>();
            }
        }
    }
}