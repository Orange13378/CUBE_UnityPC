using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace CubeECS
{
    public class DialogInitSystem : IEcsInitSystem
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
                dialogComponent.DialogBehavior = _gameData.Value.DialogBehavior;
                dialogComponent.TextSpeed = 0.02f;
            }
        }
    }
}