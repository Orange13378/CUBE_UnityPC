using UniRx;
using UnityEngine;

namespace CubeMVC
{
    public class PlayerMoveController : MonoBehaviour
    {
        private PlayerView _playerView;
        private PlayerInputModel _playerModel;
        private Vector2 _currentMoveInput;
        private Rigidbody2D _playerRB;

        [SerializeField]
        private ContextProvider _contextProvider;

        [SerializeField]
        private FixedJoystick _joystick;

        private void Start()
        {
            _playerView = _contextProvider.GetContext().PlayerView;
            _playerModel = _contextProvider.GetContext().PlayerInputModel;

            _playerModel.X.Subscribe(x =>
            {
                _currentMoveInput.x = x;
            });

            _playerModel.Y.Subscribe(y =>
            {
                _currentMoveInput.y = y;
            });

            _playerRB = _playerView.GetRigidBody2D();
        }

        private void Update()
        {
            if (!_playerModel.IsPlayerActive.Value)
                return;

            _playerModel.UpdateInput(_joystick.Horizontal, _joystick.Vertical);
        }

        private void FixedUpdate()
        {
            if (!_playerModel.IsPlayerActive.Value)
                return;

            _playerView.Move(_currentMoveInput);
            _playerModel.CurrentPosition.Value = _playerRB.position;
        }
    }
}