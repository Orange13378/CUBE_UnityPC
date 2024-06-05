using UnityEngine;

namespace CubeMVC
{
    public class FootstepsController : MonoBehaviour
    {
        [SerializeField]
        private ContextProvider _contextProvider;

        private FootstepsModel _footstepsModel;
        private PlayerInputModel _playerInputModel;
        private AudioSource _audioSource;

        private void Start()
        {
            _audioSource = gameObject.GetComponent<AudioSource>();
            _footstepsModel = _contextProvider.GetContext().FootstepsModel;
            _playerInputModel = _contextProvider.GetContext().PlayerInputModel;
        }

        private void Update()
        {
            if (!_playerInputModel.IsPlayerActive.Value)
                return;

            if (_contextProvider.GetContext().PlayerInputModel.X.Value == 0 &&
                _contextProvider.GetContext().PlayerInputModel.Y.Value == 0) 
                return;

            switch (_footstepsModel.Timer)
            {
                case > 0:
                    _footstepsModel.Timer -= Time.deltaTime;
                    break;
                case <= 0:
                    _audioSource.PlayOneShot(_footstepsModel.FootSteps[Random.Range(0, _footstepsModel.FootSteps.Length)]);
                    _footstepsModel.Timer = 0.42f;
                    break;
            }
        }
    }
}