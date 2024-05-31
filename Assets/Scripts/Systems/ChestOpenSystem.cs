using System.Collections.Generic;
using System.Linq;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace CubeECS
{
    public class ChestOpenSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorldInject _world;
        private EcsFilterInject<Inc<ChestComponent>> _chestFilter;
        private EcsFilterInject<Inc<InventoryComponent>> _inventoryFilter;
        private EcsFilterInject<Inc<DialogComponent>> _dialogFilter;
        private EcsPoolInject<InventoryComponent> _inventoryPool;
        private EcsPoolInject<ChestComponent> _chestPool;
        private EcsPoolInject<DialogComponent> _dialogPool;
        private EcsCustomInject<GameData> _gameData;

        private InventoryComponent _inventoryComponent;
        private ChestComponent _chestComponent;

        public void Init(IEcsSystems systems)
        {
            var chestEntity = _world.Value.NewEntity();
            _chestPool.Value.Add(chestEntity);

            foreach (var entity in _chestFilter.Value)
            {
                ref var chestComponent = ref _chestPool.Value.Get(entity);
                chestComponent.Items = new List<ChestItem>();
                _chestComponent = chestComponent;
            }

            foreach (var entity in _inventoryFilter.Value)
            {
                ref var inventoryComponent = ref _inventoryPool.Value.Get(entity);
                _inventoryComponent = inventoryComponent;
            }
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _chestFilter.Value)
            {
                ref var chestComponent = ref _chestPool.Value.Get(entity);
                _chestComponent = chestComponent;
            }

            var chest = _chestComponent.Items.FirstOrDefault(x => x.IsOpened == false);

            if (chest != null)
            {
                var item = _inventoryComponent.Items.FirstOrDefault(x => x.id == chest.KeyId);

                if (item != null)
                {
                    chest.IsOpened = true;
                    _inventoryComponent.Items.Remove(item);
                    _inventoryComponent.OnItemChangedUICallback?.Invoke();
                }

                StartDialog(chest);

                _chestComponent.Items.Remove(chest);
            }
        }

        private void StartDialog(ChestItem chest)
        {
            if (chest.IsOpened)
            {
                chest.IsUsed = true;

                var chestObject = _gameData.Value.Chests.First(x => x.GetComponent<ChestInteract>().chestItem.Id == chest.Id);
                chestObject.GetComponent<SpriteRenderer>().sprite = chest.Sprite;

                foreach (var entity in _dialogFilter.Value)
                {
                    ref var dialogComponent = ref _dialogPool.Value.Get(entity);
                    dialogComponent.DialogItem.InputText = chest.Success;
                    dialogComponent.DialogSystem.StartDialog();
                }

                _chestComponent.CurrentOpenedItem.SetActive(true);
            }
            else
            {
                foreach (var entity in _dialogFilter.Value)
                {
                    ref var dialogComponent = ref _dialogPool.Value.Get(entity);
                    dialogComponent.DialogItem.InputText = chest.Bad;
                    dialogComponent.DialogSystem.StartDialog();
                }
            }
        }
    }
}