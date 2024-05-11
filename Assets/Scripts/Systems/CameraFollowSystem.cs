using Leopotam.EcsLite;
using UnityEngine;

namespace CubeECS
{
    public class CameraFollowSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsFilter _filter;
        private EcsPool<CameraComponent> _cameraPool;
        private CameraComponent _cameraComponent;
        private EcsPool<PlayerComponent> _playerPool;

        public void Init(IEcsSystems systems)
        {
            var gameData = systems.GetShared<GameData>();

            var cameraEntity = systems.GetWorld().NewEntity();
            _cameraPool = systems.GetWorld().GetPool<CameraComponent>();
            _cameraPool.Add(cameraEntity);
            ref var cameraComponent = ref _cameraPool.Get(cameraEntity);
            _cameraComponent = cameraComponent;
            _cameraComponent.CameraTransform = Camera.main.transform;
            _cameraComponent.CameraSmoothness = gameData.Configuration.CameraFollowSmoothness;
            _cameraComponent.CurVelocity = Vector3.zero;
            _cameraComponent.Offset = new Vector3(0f, 0f, -1f);

            _filter = systems.GetWorld().Filter<PlayerComponent>().End();
            _playerPool = systems.GetWorld().GetPool<PlayerComponent>();
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filter)
            {
                ref var playerComponent = ref _playerPool.Get(entity);

                if (!playerComponent.IsPlayerActive)
                    return;

                var currentPosition = _cameraComponent.CameraTransform.position;
                var targetPoint = playerComponent.PlayerTransform.position + _cameraComponent.Offset;

                _cameraComponent.CameraTransform.position = Vector3.SmoothDamp(currentPosition, targetPoint, ref _cameraComponent.CurVelocity, _cameraComponent.CameraSmoothness);
            }
        }
    }
}