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

            ref var inventoryCmp =  ref _inventoryPool.Value.Add(inventoryEntity);
            inventoryCmp.InventoryView = _gameData.Value.InventoryView;
            inventoryCmp.InventoryView.Construct(_world.Value);

            inventoryCmp.ItemInteract = _gameData.Value.ItemInteract;
            inventoryCmp.ItemInteract.Construct(_world.Value);

            foreach (var entity in _inventoryFilter.Value)
            {
                ref var inventoryComponent = ref _inventoryPool.Value.Get(entity);
                inventoryComponent.Items = new List<Item>();
            }
        }
    }
}