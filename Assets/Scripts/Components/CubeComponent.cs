using Cinemachine;
using UnityEngine;

public struct CubeComponent
{
    public GameObject Player;
    public string DialogText;

    public delegate void OnInteracted();
    public OnInteracted OnInteractedCallback;

    public CinemachineVirtualCamera VirtualCamera;
    public CinemachineBasicMultiChannelPerlin VirtualCameraChannel;
}