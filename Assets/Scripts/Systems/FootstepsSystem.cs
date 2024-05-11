using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace CubeECS
{
    public class FootstepsSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorldInject _world;
        
        private EcsFilterInject<Inc<PlayerInputComponent, PlayerComponent>> _filters;
        private EcsPoolInject<FootstepsComponent> _footstepsPool;
        private EcsPoolInject<PlayerInputComponent> _playerInputPool;
        private EcsPoolInject<PlayerComponent> _playerPool;

        private FootstepsComponent _footstepsComponent;

        public void Init(IEcsSystems systems)
        {
            foreach (var entity in _filters.Value)
            {
                ref var playerComponent = ref _playerPool.Value.Get(entity);
                _footstepsComponent.AudioSource = playerComponent.PlayerAudioSource;
            }

            var gameData = systems.GetShared<GameData>();
            _footstepsComponent.FootSteps = gameData.FootStepsAudioClips;
            _footstepsComponent.Timer = 0.1f;

            var footstepsEntity = _world.Value.NewEntity();
            _footstepsPool.Value.Add(footstepsEntity);
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filters.Value)
            {
                ref var playerComponent = ref _playerPool.Value.Get(entity);

                if (!playerComponent.IsPlayerActive)
                    return;

                ref var playerInputComponent = ref _playerInputPool.Value.Get(entity);

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