using UniRx;
using UnityEngine;

namespace CubeMVC
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField]
        private ContextProvider _contextProvider;

        private CameraView _cameraView;
        private CameraModel cameraModel;

        private void Start()
        {
            _cameraView = _contextProvider.GetContext().CameraView;
            cameraModel = new CameraModel(_cameraView.CameraTransform.position);

            cameraModel.TargetPosition.Subscribe(targetPosition =>
            {
                Vector3 currentVelocity = cameraModel.CurrentVelocity.Value;
                _cameraView.MoveCamera(targetPosition, ref currentVelocity);
            });

        }

        private void FixedUpdate()
        {
            var targetPoint = _contextProvider.GetContext().PlayerInputModel.CurrentPosition.Value + _cameraView.Offset;
            cameraModel.UpdateTargetPosition(targetPoint);
        }
    }
}