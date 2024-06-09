using CubeMVC;
using UnityEngine;

namespace MVC
{
    public class PedestalController : MonoBehaviour
    {
        [SerializeField]
        private ContextProvider _contextProvider;

        private PedestalModel _pedestalModel;
        private DialogModel _dialogModel;
        private PlayerInputModel _playerInputModel;

        private void Start()
        {
            _pedestalModel = _contextProvider.GetContext().PedestalModel;
            _dialogModel = _contextProvider.GetContext().DialogModel;
            _playerInputModel = _contextProvider.GetContext().PlayerInputModel;
        }

        public void Update()
        {
            if (!_playerInputModel.PressedX.Value || !_pedestalModel.IsEntered)
                return;

            if (_pedestalModel.CurrentUI == PedestalWorld.White)
            {
                _dialogModel.OnDialogStart.Invoke("Странный куб");
            }

            _pedestalModel.PedestalsUI[(int)_pedestalModel.CurrentUI].SetActive(true);

            _playerInputModel.IsPlayerActive.Value = false;
        }
    }
}