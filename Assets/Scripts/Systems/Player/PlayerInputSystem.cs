using Leopotam.EcsLite;
using UnityEngine;

namespace CubeECS
{
    public class PlayerInputSystem : IEcsRunSystem, IEcsInitSystem
    {
        private EcsFilter _filter;
        private EcsPool<PlayerInputComponent> _playerInputPool;
        private EcsPool<PlayerComponent> _playerPool;

        public void Init(IEcsSystems systems)
        {
            _filter = systems.GetWorld().Filter<PlayerInputComponent>().Inc<PlayerComponent>().End();

            _playerInputPool = systems.GetWorld().GetPool<PlayerInputComponent>();
            _playerPool = systems.GetWorld().GetPool<PlayerComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var playerComponent = ref _playerPool.Get(entity);

                if (!playerComponent.IsPlayerActive)
                    return;

                ref var playerInputComponent = ref _playerInputPool.Get(entity);
                playerInputComponent.MoveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
            }
        }
    }
}