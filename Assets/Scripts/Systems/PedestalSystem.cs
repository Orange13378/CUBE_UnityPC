using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

namespace CubeECS
{
    public class PedestalSystem : IEcsInitSystem, IEcsRunSystem
    {
        private EcsWorldInject _world;
        private EcsPoolInject<PedestalComponent> _pedestalPool;
        private EcsPoolInject<DisablePlayerComponent> _disablePlayerPool;
        private EcsPoolInject<DialogComponent> _dialogPool;
        private EcsPoolInject<PlayerInputComponent> _playerInputPool;
        private EcsFilterInject<Inc<DialogComponent>> _dialogFilter;
        private EcsFilterInject<Inc<PedestalComponent>> _pedestalFilter;
        private EcsFilterInject<Inc<PlayerInputComponent>> _playerInputFilter;
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
            foreach (var playerInputEntity in _playerInputFilter.Value)
            {
                ref var playerInputComponent = ref _playerInputPool.Value.Get(playerInputEntity);

                if (!playerInputComponent.PressedX)
                    return;
            }

            foreach (var pedestalEntity in _pedestalFilter.Value)
            {
                ref var pedestalCmp = ref _pedestalPool.Value.Get(pedestalEntity);

                if (!pedestalCmp.IsEntered)
                    return;
                
                pedestalCmp.IsEntered = false;

                if (pedestalCmp.CurrentUI == PedestalWorld.White)
                {
                    foreach (var dialogEntity in _dialogFilter.Value)
                    {
                        ref var dialogComponent = ref _dialogPool.Value.Get(dialogEntity);
                        dialogComponent.InputText = "Странный куб";
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