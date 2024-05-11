using System.Linq;
using Leopotam.EcsLite;

namespace CubeECS
{
    public class ChestOpenSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter _inventoryFilter;
        private EcsFilter _chestFilter;
        private EcsPool<InventoryComponent> _inventoryPool;
        private EcsPool<ChestComponent> _chestPool;
        private InventoryComponent _inventoryComponent;
        public void Init(IEcsSystems systems)
        {
            var ecsWorld = systems.GetWorld();

            _inventoryFilter = ecsWorld.Filter<InventoryComponent>().End();
            _chestFilter = ecsWorld.Filter<ChestComponent>().End();

            _chestPool = ecsWorld.GetPool<ChestComponent>();
            _inventoryPool = ecsWorld.GetPool<InventoryComponent>();

            foreach (var entity in _inventoryFilter)
            {
                ref var inventoryComponent = ref _inventoryPool.Get(entity);

                _inventoryComponent = inventoryComponent;
            }
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _chestFilter)
            {
                ref var chestComponent = ref _chestPool.Get(entity);

                var chest = chestComponent.Items.FirstOrDefault(x => x.IsOpened == false);

                if (chest != null && _inventoryComponent.Items.Any(x => x.id == chest.KeyId))
                {
                    chest.IsOpened = true;
                }

                if (chest != null && !chest.IsClosed && chestComponent.OnItemInteractedCallback != null) // TODO без ключа доделать
                    chestComponent.OnItemInteractedCallback.Invoke();
            }
        }
    }
}