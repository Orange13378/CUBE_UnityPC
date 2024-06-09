using UnityEngine;

namespace CubeMVC
{
    public class CameraView : MonoBehaviour
    {
        public Transform CameraTransform;
        private readonly float _cameraSmoothness = 0.5f;
        public Vector2 Offset = Vector3.zero;

        private void Start()
        {
            CameraTransform = gameObject.transform;
        }

        public void MoveCamera(Vector3 targetPosition, ref Vector3 currentVelocity)
        {
            var currentPosition = CameraTransform.position;
            CameraTransform.position = Vector3.SmoothDamp(currentPosition, targetPosition, ref currentVelocity, _cameraSmoothness);
        }
    }
}