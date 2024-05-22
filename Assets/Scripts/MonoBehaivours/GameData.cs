using Cinemachine;
using UnityEngine;
using UnityEngine.UI;

namespace CubeECS
{
    public class GameData
    {
        public CinemachineVirtualCamera VirtualCamera;
        public Image WhiteScreenImage;
        public ConfigurationSO Configuration;
        public GameObject DialogPanel;
        public AudioClip[] FootStepsAudioClips;
        public Item[] Items;
        public Sprite[] OpenedChestSprites;
        public GameObject[] Chests;

        public GameObject Player;
        public GameObject PedestalGO;
        public GameObject PedestalCubeGO;
        public GameObject[] Worlds;
        public GameObject[] PedestalsUI;
        public PedestalItem[] Pedestals;
        public DialogSystem DialogSystem { get; set; }
    }
}