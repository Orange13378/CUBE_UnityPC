using UnityEngine;
using UnityEngine.UI;

namespace CubeECS
{
    public class GameData
    {
        public ConfigurationSO Configuration;
        public Text CoinCounter;
        public GameObject PlayerWonPanel;
        public AudioClip[] FootStepsAudioClips;
        public Item[] Items;
        public Sprite[] OpenedChestSprites;
        public GameObject[] Chests;
    }
}