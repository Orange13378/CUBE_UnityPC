using System.Collections.Generic;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace CubeECS
{
    public class InventoryInitSystem : IEcsInitSystem
    {
        private EcsWorldInject _world;
        private EcsPoolInject<InventoryComponent> _inventoryPool;
        private EcsFilterInject<Inc<InventoryComponent>> _inventoryFilter;
        private EcsCustomInject<GameData> _gameData;

        public void Init(IEcsSystems systems)
        {
            var inventoryEntity = _world.Value.NewEntity();

            _inventoryPool.Value.Add(inventoryEntity);

            foreach (var entity in _inventoryFilter.Value)
            {
                ref var inventoryComponent = ref _inventoryPool.Value.Get(entity);
                inventoryComponent.Items = new List<Item>();
            }
        }
    }
}