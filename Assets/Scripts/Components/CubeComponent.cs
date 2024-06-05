using Cinemachine;
using UnityEngine;

namespace CubeECS
{
    public struct CubeComponent
    {
        public GameObject Player;

        public CinemachineVirtualCamera VirtualCamera;
        public CinemachineBasicMultiChannelPerlin VirtualCameraChannel;
    }
}