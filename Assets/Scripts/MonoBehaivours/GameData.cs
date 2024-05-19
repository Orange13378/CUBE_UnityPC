using UnityEngine;

namespace CubeECS
{
    public class GameData
    {
        public ConfigurationSO Configuration;
        public GameObject DialogPanel;
        public AudioClip[] FootStepsAudioClips;
        public Item[] Items;
        public Sprite[] OpenedChestSprites;
        public GameObject[] Chests;
        public DialogSystem DialogSystem { get; set; }
    }
}