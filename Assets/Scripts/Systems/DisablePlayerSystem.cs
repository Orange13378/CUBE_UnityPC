using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace CubeECS
{
    public class DisablePlayerSystem : IEcsRunSystem
    {
        private EcsWorldInject _world;
        private EcsFilterInject<Inc<DisablePlayerComponent>> _disablePlayerFilter;
        private EcsFilterInject<Inc<PlayerComponent>> _playerFilter;
        private EcsPoolInject<DisablePlayerComponent> _disablePlayerPool;
        private EcsPoolInject<PlayerComponent> _playerPool;
        private EcsCustomInject<GameData> _gameData;

        public void Run(IEcsSystems systems)
        {
            foreach (var disablePlayerEntity in _disablePlayerFilter.Value)
            {
                ref var disablePlayerComponent = ref _disablePlayerPool.Value.Get(disablePlayerEntity);
                
                if (disablePlayerComponent.Deactivate)
                    DeactivatePlayer();
                else
                    ActivatePlayer();

                _disablePlayerPool.Value.Del(disablePlayerEntity);
            }
        }

        private void DeactivatePlayer()
        {
            foreach (var entity in _playerFilter.Value)
            {
                ref var player = ref _playerPool.Value.Get(entity);
                player.IsPlayerActive = false;
            }
        }

        private void ActivatePlayer()
        {
            foreach (var entity in _playerFilter.Value)
            {
                ref var player = ref _playerPool.Value.Get(entity);
                player.IsPlayerActive = true;
            }
        }
    }
}