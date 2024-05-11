using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace CubeECS
{
    public class DialogInitSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorldInject _world;
        private EcsFilterInject<Inc<DialogComponent>> _filters;
        private EcsPoolInject<DialogComponent> _dialogPool;
        private EcsCustomInject<GameData> _gameData;

        public void Init(IEcsSystems systems)
        {
            var dialogEntity = _world.Value.NewEntity();
            _dialogPool.Value.Add(dialogEntity);

            foreach (var entity in _filters.Value)
            {
                ref var dialogComponent = ref _dialogPool.Value.Get(entity);
                dialogComponent.DialogSystem = _gameData.Value.DialogSystem;
                dialogComponent.DialogSystem.Construct(_world.Value);
            }
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filters.Value)
            {
                ref var dialogComponent = ref _dialogPool.Value.Get(entity);

                if (!dialogComponent.IsActive)
                    return;
                
                dialogComponent.DialogSystem.StartDialog();
            }
        }
    }
}