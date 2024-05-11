using Leopotam.EcsLite;
using UnityEngine;

namespace CubeECS
{
    public class FootstepsSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter _filter;
        private EcsPool<FootstepsComponent> _footstepsPool;
        private EcsPool<PlayerInputComponent> _playerInputPool;
        private EcsPool<PlayerComponent> _playerPool;
        private FootstepsComponent _footstepsComponent;

        public void Init(IEcsSystems systems)
        {
            var ecsWorld = systems.GetWorld();
            var footstepsEntity = ecsWorld.NewEntity();
            _filter = systems.GetWorld().Filter<PlayerInputComponent>().Inc<PlayerComponent>().End();

            _footstepsPool = systems.GetWorld().GetPool<FootstepsComponent>();
            _playerInputPool = systems.GetWorld().GetPool<PlayerInputComponent>();
            _playerPool = systems.GetWorld().GetPool<PlayerComponent>();

            foreach (var entity in _filter)
            {
                ref var playerComponent = ref _playerPool.Get(entity);
                _footstepsComponent.AudioSource = playerComponent.PlayerAudioSource;
            }

            var gameData = systems.GetShared<GameData>();
            _footstepsComponent.FootSteps = gameData.FootStepsAudioClips;
            _footstepsComponent.Timer = 0.1f;

            _footstepsPool.Add(footstepsEntity);
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var playerComponent = ref _playerPool.Get(entity);

                if (!playerComponent.IsPlayerActive)
                    return;

                ref var playerInputComponent = ref _playerInputPool.Get(entity);

                if (playerInputComponent.MoveInput.x != 0 || playerInputComponent.MoveInput.y != 0)
                {
                    if (_footstepsComponent.Timer > 0)
                    {
                        _footstepsComponent.Timer -= Time.deltaTime;
                    }

                    if (_footstepsComponent.Timer <= 0)
                    {
                        _footstepsComponent.AudioSource.PlayOneShot(_footstepsComponent.FootSteps[Random.Range(0, _footstepsComponent.FootSteps.Length)]);
                        _footstepsComponent.Timer = 0.42f;
                    }
                }
            }
        }
    }
}