using UnityEngine;

namespace CubeMVC
{
    public class FootstepsController : MonoBehaviour
    {
        [SerializeField]
        private ContextProvider _contextProvider;

        private FootstepsModel footstepsModel;
        private AudioSource _audioSource;

        private void Start()
        {
            _audioSource = gameObject.GetComponent<AudioSource>();
            footstepsModel = _contextProvider.GetContext().FootstepsModel;
        }

        private void Update()
        {
            if (_contextProvider.GetContext().PlayerInputModel.X.Value == 0 &&
                _contextProvider.GetContext().PlayerInputModel.Y.Value == 0) 
                return;

            switch (footstepsModel.Timer)
            {
                case > 0:
                    footstepsModel.Timer -= Time.deltaTime;
                    break;
                case <= 0:
                    _audioSource.PlayOneShot(footstepsModel.FootSteps[Random.Range(0, footstepsModel.FootSteps.Length)]);
                    footstepsModel.Timer = 0.42f;
                    break;
            }
        }
    }
}