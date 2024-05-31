using Cinemachine;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace CubeECS
{
    public class CubeInitSystem : IEcsInitSystem
    {
        private EcsWorldInject _world;
        private EcsPoolInject<CubeComponent> _cubePool;
        private EcsFilterInject<Inc<CubeComponent>> _cubeFilter;
        private EcsCustomInject<GameData> _gameData;

        public void Init(IEcsSystems systems)
        {
            var cubeEntity = _world.Value.NewEntity();
            _cubePool.Value.Add(cubeEntity);

            foreach (var entity in _cubeFilter.Value)
            {
                ref var cubeCmp = ref _cubePool.Value.Get(entity);
                cubeCmp.Player = _gameData.Value.Player;
                cubeCmp.VirtualCamera = _gameData.Value.VirtualCamera;
                cubeCmp.VirtualCameraChannel = _gameData.Value.VirtualCamera
                    .GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            }
        }
    }
}