using Cinemachine;
using UnityEngine;

namespace CubeMVC
{
    public class CubeModel
    {
        public GameObject Player;

        public CinemachineVirtualCamera VirtualCamera;
        public CinemachineBasicMultiChannelPerlin VirtualCameraChannel;

        public CubeModel(GameObject player, CinemachineVirtualCamera virtualCamera,
            CinemachineBasicMultiChannelPerlin channel)
        {
            Player = player;
            VirtualCamera = virtualCamera;
            VirtualCameraChannel = channel;
        }
    }
}