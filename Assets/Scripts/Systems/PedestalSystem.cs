using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace CubeECS
{
    public class PedestalSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorldInject _world;
        private EcsPoolInject<PedestalComponent> _pedestalPool;
        private EcsPoolInject<DisablePlayerComponent> _disablePlayerPool;
        private EcsPoolInject<DialogComponent> _dialogPool;
        private EcsFilterInject<Inc<DialogComponent>> _dialogFilter;
        private EcsFilterInject<Inc<PedestalComponent>> _filters;
        private EcsCustomInject<GameData> _gameData;

        public void Init(IEcsSystems systems)
        {
            var pedestalEntity = _world.Value.NewEntity();
            ref var pedestalCmp = ref _pedestalPool.Value.Add(pedestalEntity);
            pedestalCmp.PedestalGO = _gameData.Value.PedestalGO;
            pedestalCmp.PedestalCubeGO = _gameData.Value.PedestalCubeGO;
            pedestalCmp.Worlds = _gameData.Value.Worlds;
            pedestalCmp.PedestalItems = _gameData.Value.Pedestals;
            pedestalCmp.PedestalsUI = _gameData.Value.PedestalsUI;
        }

        public void Run(IEcsSystems systems)
        {
            foreach (var entity in _filters.Value)
            {
                ref var pedestalCmp = ref _pedestalPool.Value.Get(entity);

                if (!pedestalCmp.IsEntered || !Input.GetKeyDown(KeyCode.E))
                    return;

                pedestalCmp.IsEntered = false;

                if (pedestalCmp.CurrentUI == PedestalComponent.PedestalWorld.White)
                {
                    foreach (var dialogEntity in _dialogFilter.Value)
                    {
                        ref var dialogComponent = ref _dialogPool.Value.Get(dialogEntity);
                        dialogComponent.DialogItem.InputText = "Странный куб";
                        dialogComponent.DialogSystem.StartDialog();
                    }

                    return;
                }

                pedestalCmp.PedestalsUI[(int)pedestalCmp.CurrentUI].SetActive(true);
            }

            var disablePlayer = _world.Value.NewEntity();
            _disablePlayerPool.Value.Add(disablePlayer).Deactivate = true;
        }
    }
}