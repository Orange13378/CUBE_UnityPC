using UnityEngine;

namespace CubeMVC
{
    public class PedestalModel
    {
        public PedestalWorld CurrentWorld;
        public PedestalWorld CurrentUI;
        public bool IsEntered;
        public GameObject[] Worlds;
        public GameObject[] PedestalsUI;
        public PedestalItem[] PedestalItems;
        public GameObject PedestalGO;
        public GameObject PedestalCubeGO;

        public delegate void OnInteracted();

        public OnInteracted OnInteractedCallback;
    }

    public enum PedestalWorld
    {
        White = 0,
        Blue = 1,
        Orange = 2,
        Green = 3,
        Purple = 4,
        Black = 5,
    }
}