using Leopotam.EcsLite;
using UnityEngine;

namespace CubeECS
{
    public class PlayerMoveSystem : IEcsRunSystem, IEcsInitSystem
    {
        private EcsFilter _filter;
        private EcsPool<PlayerComponent> _playerPool;
        private EcsPool<PlayerInputComponent> _playerInputPool;

        public void Init(IEcsSystems systems)
        {
            _filter = systems.GetWorld().Filter<PlayerComponent>().Inc<PlayerInputComponent>().End();
            _playerPool = systems.GetWorld().GetPool<PlayerComponent>();
            _playerInputPool = systems.GetWorld().GetPool<PlayerInputComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var playerComponent = ref _playerPool.Get(entity);

                if (!playerComponent.IsPlayerActive)
                    return;

                ref var playerInputComponent = ref _playerInputPool.Get(entity);

                playerComponent.PlayerRB.MovePosition(playerComponent.PlayerRB.position +
                                                      playerInputComponent.MoveInput * playerComponent.PlayerSpeed *
                                                      Time.fixedDeltaTime);
            }

        }
    }
}