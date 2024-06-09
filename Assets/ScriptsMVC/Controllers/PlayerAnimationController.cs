using UnityEngine;

namespace CubeMVC
{
    public class PlayerAnimationController : MonoBehaviour
    {
        [SerializeField]
        private ContextProvider _contextProvider;

        private PlayerAnimationModel _playerAnimationModel;
        private PlayerInputModel _inputModel;

        private void Start()
        {
            _inputModel = _contextProvider.GetContext().PlayerInputModel;
            _playerAnimationModel = _contextProvider.GetContext().PlayerAnimationModel;
        }

        public void Update()
        {
            if (!_inputModel.IsPlayerActive.Value)
            {
                _playerAnimationModel.Animator.SetBool("Stoped", true);
                return;
            }

            _playerAnimationModel.Animator.SetBool("Stoped", false);
            _playerAnimationModel.Animator.SetFloat("Horizontal", _inputModel.X.Value);
            _playerAnimationModel.Animator.SetFloat("Vertical", _inputModel.Y.Value);
            _playerAnimationModel.Animator.SetFloat("Speed",  new Vector2(_inputModel.X.Value, _inputModel.X.Value).sqrMagnitude);
        }
    }
}