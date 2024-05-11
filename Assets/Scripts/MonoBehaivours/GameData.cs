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
        public DialogSystem Dialog { get; set; }
        public ChestInteract ChestInteract { get; set; }
        public InventoryView InventoryView { get; set; }
        public ItemInteract ItemInteract { get; set; }
        public DialogSystem DialogSystem { get; set; }
    }
}