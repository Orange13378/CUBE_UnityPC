using System.Collections.Generic;
using System.Linq;
using Leopotam.EcsLite;
using UnityEngine;

namespace CubeECS
{
    public class ChestInitSystem : IEcsInitSystem
    {
        private EcsFilter _filter;
        private EcsPool<ChestComponent> _chestPool;
        private ChestComponent _chestComponent;
        private GameData _gameData;

        public void Init(IEcsSystems systems)
        {
            var ecsWorld = systems.GetWorld();
            _gameData = systems.GetShared<GameData>();

            var chestEntity = ecsWorld.NewEntity();

            _chestPool = ecsWorld.GetPool<ChestComponent>();
            _chestPool.Add(chestEntity);

            _filter = systems.GetWorld().Filter<ChestComponent>().End();

            foreach (var entity in _filter)
            {
                ref var chestComponent = ref _chestPool.Get(entity);

                chestComponent.Items = new List<ChestItem>();
                chestComponent.OnItemInteractedCallback += StartDialog;

                _chestComponent = chestComponent;
            }
        }

        private void StartDialog()
        {
            if (_chestComponent.Items.Any(x => x.IsOpened))
            {
                var chest = _chestComponent.Items.FirstOrDefault(x => x.IsOpened);

                if (chest.IsUsed == false)
                {
                    chest.IsUsed = true;

                    var chestObject = _gameData.Chests.First(x => x.GetComponent<ChestInteract>().chestItem.Id == chest.Id);
                    chestObject.GetComponent<SpriteRenderer>().sprite = chest.Sprite;

                    DialogSystem.message.Add(chest.Success);
                    DialogSystem.on = true;
                }
            }
            else
            {
                var chest = _chestComponent.Items.FirstOrDefault(x => !x.IsOpened);

                if (chest == null)
                    return;

                chest.IsClosed = true;

                DialogSystem.message.Add(chest.Bad);
                DialogSystem.on = true;
            }
        }
    }
}