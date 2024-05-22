using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace CubeECS
{
    public class WhiteImageSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorldInject _world;
        private EcsPoolInject<WhiteImageComponent> _imagePool;
        private EcsFilterInject<Inc<WhiteImageComponent>> _imageFilter;
        private EcsCustomInject<GameData> _gameData;

        public void Init(IEcsSystems systems)
        {
            var newEntity = _world.Value.NewEntity();
            ref var image = ref _imagePool.Value.Add(newEntity);
            image.Image = _gameData.Value.WhiteScreenImage;
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _imageFilter.Value)
            {
                ref var image = ref _imagePool.Value.Get(entity);

                if (image.LightUp)
                    image.Image.color = new Color(image.Image.color.r, image.Image.color.g, image.Image.color.b, image.Image.color.a + 0.3f * Time.deltaTime);

                if (image.LightDown)
                    image.Image.color = new Color(image.Image.color.r, image.Image.color.g, image.Image.color.b, image.Image.color.a - 0.3f * Time.deltaTime);
            }
        }
    }
}