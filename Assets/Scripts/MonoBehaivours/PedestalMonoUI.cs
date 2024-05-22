using Leopotam.EcsLite;
using UnityEngine;

namespace CubeECS
{
    public class PedestalMonoUI : MonoBehaviour
    {
        private EcsWorld _ecsWorld;
        private EcsFilter _filter;
        private EcsPool<PedestalComponent> _pedestalPool;
        private EcsPool<DisablePlayerComponent> _disablePlayerPool;
        private PedestalComponent _pedestalComponent;
        private SpriteRenderer _pedestalSprite;
        private SpriteRenderer _pedestalCubeSprite;

        private void Start()
        {
            _ecsWorld = EcsWorldManager.GetEcsWorld();
            _filter = _ecsWorld.Filter<PedestalComponent>().End();
            _pedestalPool = _ecsWorld.GetPool<PedestalComponent>();
            _disablePlayerPool = _ecsWorld.GetPool<DisablePlayerComponent>();

            foreach (var entity in _filter)
            {
                ref var pedestalCmp = ref _pedestalPool.Get(entity);
                pedestalCmp.OnInteractedCallback += ChangePedestalView;
                _pedestalComponent = pedestalCmp;
                _pedestalSprite = _pedestalComponent.PedestalGO.GetComponent<SpriteRenderer>();
                _pedestalCubeSprite = _pedestalComponent.PedestalCubeGO.GetComponent<SpriteRenderer>();
            }
        }

        private void ChangePedestalView()
        {
            foreach (var entity in _filter)
            {
                ref var pedestalCmp = ref _pedestalPool.Get(entity);
                _pedestalComponent = pedestalCmp;
            }

            _pedestalCubeSprite.sprite = _pedestalComponent.PedestalItems[(int)_pedestalComponent.CurrentWorld].CubeSprite;
            SetCurrentWorld(_pedestalComponent.CurrentWorld);
        }

        private void Interact()
        {
            var currentWorldIndex = (int)_pedestalComponent.CurrentWorld;
            var currentUIIndex = (int)_pedestalComponent.CurrentUI;
            _pedestalSprite.sprite = _pedestalComponent.PedestalItems[currentWorldIndex].Sprite;

            foreach (var world in _pedestalComponent.Worlds)
            {
                world.SetActive(false);
            }

            _pedestalComponent.Worlds[currentWorldIndex].SetActive(true);
            _pedestalComponent.PedestalsUI[currentUIIndex].SetActive(false);

            var disablePlayer = _ecsWorld.NewEntity();
            _disablePlayerPool.Add(disablePlayer).Deactivate = false;
        }

        private void SetCurrentWorld(PedestalWorld currentWorld)
        {
            foreach (var entity in _filter)
            {
                ref var pedestalCmp = ref _pedestalPool.Get(entity);
                pedestalCmp.CurrentWorld = currentWorld;
                _pedestalComponent = pedestalCmp;
                Interact();
            }
        }

        public void PressedWhite()
        {
            SetCurrentWorld(PedestalWorld.White);
        }

        public void PressedBlue()
        {
            SetCurrentWorld(PedestalWorld.Blue);
        }

        public void PressedOrange()
        {
            SetCurrentWorld(PedestalWorld.Orange);
        }

        public void PressedGreen()
        {
            SetCurrentWorld(PedestalWorld.Green);
        }

        public void PressedPurple()
        {
            SetCurrentWorld(PedestalWorld.Purple);
        }

        public void PressedBlack()
        {
            SetCurrentWorld(PedestalWorld.Black);
        }
    }
}