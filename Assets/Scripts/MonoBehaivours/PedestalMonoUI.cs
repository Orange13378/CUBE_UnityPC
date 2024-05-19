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
                _pedestalComponent = pedestalCmp;
                _pedestalComponent.OnInteractedCallback += Interact;
                _pedestalSprite = _pedestalComponent.PedestalGO.GetComponent<SpriteRenderer>();
                _pedestalCubeSprite = _pedestalComponent.PedestalCubeGO.GetComponent<SpriteRenderer>();
            }
        }

        private void Interact()
        {
            var currentWorldIndex = (int)_pedestalComponent.CurrentWorld;
            var currentUIIndex = (int)_pedestalComponent.CurrentUI;
            _pedestalSprite.sprite = _pedestalComponent.PedestalItems[currentWorldIndex].Sprite;
            
            // _pedestalCubeSprite.sprite = _pedestalComponent.PedestalItems[currentWorldIndex].CubeSprite;
            // При добавлении нового мира.

            foreach (var world in _pedestalComponent.Worlds)
            {
                world.SetActive(false);
            }

            _pedestalComponent.Worlds[currentWorldIndex].SetActive(true);
            _pedestalComponent.PedestalsUI[currentUIIndex].SetActive(false);

            var disablePlayer = _ecsWorld.NewEntity();
            _disablePlayerPool.Add(disablePlayer).Deactivate = false;
        }

        private void SetCurrentWorld(PedestalComponent.PedestalWorld currentWorld)
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
            SetCurrentWorld(PedestalComponent.PedestalWorld.White);
        }

        public void PressedBlue()
        {
            SetCurrentWorld(PedestalComponent.PedestalWorld.Blue);
        }

        public void PressedOrange()
        {
            SetCurrentWorld(PedestalComponent.PedestalWorld.Orange);
        }

        public void PressedGreen()
        {
            SetCurrentWorld(PedestalComponent.PedestalWorld.Green);
        }

        public void PressedPurple()
        {
            SetCurrentWorld(PedestalComponent.PedestalWorld.Purple);
        }

        public void PressedBlack()
        {
            SetCurrentWorld(PedestalComponent.PedestalWorld.Black);
        }
    }
}