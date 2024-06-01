using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace CubeECS
{
    public class PlayerInputSystem : IEcsRunSystem, IEcsInitSystem
    {
        private EcsFilterInject<Inc<PlayerInputComponent, PlayerComponent>> _filters;
        private EcsPoolInject<PlayerComponent> _playerPool;
        private EcsPoolInject<PlayerInputComponent> _playerInputPool;
        private EcsCustomInject<GameData> _gameData;

        private FixedJoystick _joystick;

        public void Init(IEcsSystems systems)
        {
            _joystick = _gameData.Value.FixedJoystick;
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filters.Value)
            {
                ref var playerComponent = ref _playerPool.Value.Get(entity);

                if (!playerComponent.IsPlayerActive)
                    return;

                ref var playerInputComponent = ref _playerInputPool.Value.Get(entity);
                playerInputComponent.MoveInput = new Vector2(_joystick.Horizontal, _joystick.Vertical);
            }
        }
    }
}