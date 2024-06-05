using CubeECS;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace CubeECS
{
    public class WhiteImageSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorldInject _world;
        private EcsPoolInject<WhiteImageComponent> _imagePool;
        private EcsPoolInject<PedestalComponent> _pedestalPool;
        private EcsFilterInject<Inc<WhiteImageComponent>> _imageFilter;
        private EcsFilterInject<Inc<PedestalComponent>> _pedestalFilter;
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

                foreach (var pedestalEntity in _pedestalFilter.Value)
                {
                    ref var pedestalCmp = ref _pedestalPool.Value.Get(pedestalEntity);
                    if (pedestalCmp.CurrentWorld == PedestalWorld.Black)
                    {
                        image.Image = _gameData.Value.BlackScreenImage;
                    }
                }

                if (image.LightUp)
                    image.Image.color = new Color(image.Image.color.r, image.Image.color.g, image.Image.color.b, image.Image.color.a + 0.3f * Time.deltaTime);

                if (image.LightDown)
                    image.Image.color = new Color(image.Image.color.r, image.Image.color.g, image.Image.color.b, image.Image.color.a - 0.3f * Time.deltaTime);
            }
        }
    }
}