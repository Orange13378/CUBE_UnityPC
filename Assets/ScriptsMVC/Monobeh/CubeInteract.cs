using System.Collections;
using UnityEngine;

namespace CubeMVC
{
    public class CubeInteract : MonoBehaviour
    {
        [SerializeField] public CubeItem cubeItem;

        private ContextProvider _contextProvider;

        private PlayerInputModel _inputModel;
        private PedestalModel _pedestalModel;
        private DialogModel _dialogModel;
        private CubeModel _cubeModel;
        private ImageModel _imageModel;

        private bool _isActivated;
        private bool _isEntered;

        private void Start()
        {
            _contextProvider = FindObjectOfType<ContextProvider>();
            _inputModel = _contextProvider.GetContext().PlayerInputModel;
            _pedestalModel = _contextProvider.GetContext().PedestalModel;
            _dialogModel = _contextProvider.GetContext().DialogModel;
            _cubeModel = _contextProvider.GetContext().CubeModel;
            _imageModel = _contextProvider.GetContext().ImageModel;
        }

        private void Update()
        {
            if (!_isEntered || _isActivated || !_inputModel.PressedX.Value)
                return;

            _isActivated = true;

            _pedestalModel.CurrentWorld = cubeItem.NextWorld;
            _pedestalModel.CurrentUI = cubeItem.NextWorld;

            StartCoroutine(Interact());
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Player"))
                return;

            _isEntered = true;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (!other.CompareTag("Player"))
                return;

            _isEntered = false;
        }

        private IEnumerator Interact()
        {
            DisablePlayer();
            StartShakeCamera();
            LightUp();
            yield return new WaitForSeconds(5f);

            _cubeModel.Player.transform.position = cubeItem.Position;
            _pedestalModel.OnInteractedCallback?.Invoke();

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
            _inputModel.IsPlayerActive.Value = false;
        }

        private void StartShakeCamera()
        {
            _cubeModel.VirtualCameraChannel.m_AmplitudeGain = 1.0f;
            _cubeModel.VirtualCameraChannel.m_FrequencyGain = 1.0f;
        }

        private void StopShakeCamera()
        {
            _cubeModel.VirtualCameraChannel.m_AmplitudeGain = 0f;
            _cubeModel.VirtualCameraChannel.m_FrequencyGain = 0f;
        }

        private void LightUp()
        {
            _imageModel.LightUp = true;
        }

        private void LightDown()
        {
            _imageModel.LightUp = false;
            _imageModel.LightDown = true;
        }

        private void ClearImage()
        {
            _imageModel.LightDown = false;
        }

        private void StartDialog()
        {
            _dialogModel.OnDialogStart(cubeItem.DialogText);
        }
    }
}