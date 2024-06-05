using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace CubeMVC
{
    public class DialogController : MonoBehaviour
    {
        [SerializeField]
        private GameObject _dialogPanel;

        [SerializeField]
        private ContextProvider _contextProvider;

        private bool _isProcessing;
        private bool _continueDialog;
        private Text _dialogText;

        private DialogModel _dialogModel;
        private PlayerInputModel _inputModel;

        private void Start()
        {
            _dialogModel = _contextProvider.GetContext().DialogModel;
            _inputModel = _contextProvider.GetContext().PlayerInputModel; 
            _dialogText = _dialogPanel.GetComponentInChildren<Text>();

            _dialogModel.OnDialogStart += StartDialog;
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0) && !_continueDialog)
            {
                _continueDialog = true;
            }
        }

        public void StartDialog()
        {
            if (_isProcessing)
                return;

            StartCoroutine(StartCoroutine());
        }

        public IEnumerator StartCoroutine()
        {
            _inputModel.IsPlayerActive.Value = false;

            _isProcessing = true;
            _continueDialog = false;

            _dialogPanel.SetActive(true);
            _dialogText.text = string.Empty;

            foreach (char c in _dialogModel.InputText)
            {
                _dialogText.text += c;
                yield return new WaitForSeconds(_dialogModel.TextSpeed);
            }

            yield return new WaitUntil(() => _continueDialog);

            _inputModel.IsPlayerActive.Value = true;

            _dialogPanel.SetActive(false);
            _isProcessing = false;
        }
    }
}