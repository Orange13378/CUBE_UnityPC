using System.Collections;
using Leopotam.EcsLite;
using UnityEngine;

namespace CubeECS
{
    public class CubeInteract : MonoBehaviour
    {
        [SerializeField] public CubeItem cubeItem;
        
        private EcsWorld _world;
        private EcsFilter _cubeFilter;
        private EcsFilter _pedestalFilter;
        private EcsFilter _imageFilter;
        private EcsFilter _dialogFilter;
        private EcsPool<CubeComponent> _cubePool;
        private EcsPool<PedestalComponent> _pedestalPool;
        private EcsPool<WhiteImageComponent> _imagePool;
        private EcsPool<DialogComponent> _dialogPool;
        private EcsPool<DisablePlayerComponent> _disablePlayerPool;
        private CubeComponent _cube;
        private PedestalComponent _pedestal;

        private bool _isActivated;

        private void Start()
        {
            _world = EcsWorldManager.GetEcsWorld();
            _cubeFilter = _world.Filter<CubeComponent>().End();
            _dialogFilter = _world.Filter<DialogComponent>().End();
            _pedestalFilter = _world.Filter<PedestalComponent>().End();
            _imageFilter = _world.Filter<WhiteImageComponent>().End();
            _cubePool = _world.GetPool<CubeComponent>();
            _dialogPool = _world.GetPool<DialogComponent>();
            _imagePool = _world.GetPool<WhiteImageComponent>();
            _pedestalPool = _world.GetPool<PedestalComponent>();
            _disablePlayerPool = _world.GetPool<DisablePlayerComponent>();
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (_isActivated || !other.CompareTag("Player") || !Input.GetKey(KeyCode.E))
                return;

            _isActivated = true;

            if (cubeItem.NextWorld == PedestalWorld.Black)
            {
                // TODO черный свет
            }

            foreach (var entity in _cubeFilter)
            {
                ref var cubeComponent = ref _cubePool.Get(entity);

                _cube = cubeComponent;
            }

            foreach (var entity in _pedestalFilter)
            {
                ref var pedestalComponent = ref _pedestalPool.Get(entity);
                pedestalComponent.CurrentWorld = cubeItem.NextWorld;
                pedestalComponent.CurrentUI = cubeItem.NextWorld;
                _pedestal = pedestalComponent;
            }

            StartCoroutine(Interact());
        }

        IEnumerator Interact()
        {
            DisablePlayer();
            StartShakeCamera();
            LightUp();
            yield return new WaitForSeconds(5f);

            _cube.Player.transform.position = cubeItem.Position;
            _pedestal.OnInteractedCallback?.Invoke();

            DisablePlayer();
            LightDown();
            StopShakeCamera();
            yield return new WaitForSeconds(5f);
            ClearImage();
            StartDialog();
            gameObject.SetActive(false);
        }

        private void DisablePlayer()
        {
            var disablePlayer = _world.NewEntity();
            _disablePlayerPool.Add(disablePlayer).Deactivate = true;
        }

        private void StartShakeCamera()
        {
            _cube.VirtualCameraChannel.m_AmplitudeGain = 1.0f;
            _cube.VirtualCameraChannel.m_FrequencyGain = 1.0f;
        }

        private void StopShakeCamera()
        {
            _cube.VirtualCameraChannel.m_AmplitudeGain = 0f;
            _cube.VirtualCameraChannel.m_FrequencyGain = 0f;
        }

        private void LightUp()
        {
            foreach (var entity in _imageFilter)
            {
                ref var imageComponent = ref _imagePool.Get(entity);
                imageComponent.LightUp = true;
            }
        }

        private void LightDown()
        {
            foreach (var entity in _imageFilter)
            {
                ref var imageComponent = ref _imagePool.Get(entity);
                imageComponent.LightUp = false;
                imageComponent.LightDown = true;
            }
        }

        private void ClearImage()
        {
            foreach (var entity in _imageFilter)
            {
                ref var imageComponent = ref _imagePool.Get(entity);
                imageComponent.LightDown = false;
            }
        }

        private void StartDialog()
        {
            foreach (var entity in _dialogFilter)
            {
                ref var dialogComponent = ref _dialogPool.Get(entity);
                dialogComponent.DialogItem.InputText = cubeItem.DialogText;
                dialogComponent.DialogSystem.StartDialog();
            }
        }
    }
}