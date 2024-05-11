using UnityEngine;

namespace CubeECS
{
    public struct CameraComponent
    {
        public Transform CameraTransform;
        public Vector3 CurVelocity;
        public Vector3 Offset;
        public float CameraSmoothness;
    }
}