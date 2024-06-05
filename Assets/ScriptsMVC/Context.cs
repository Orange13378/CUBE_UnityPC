using UnityEngine;

namespace CubeMVC
{
    public class Context
    {
        public GameObject ContextOwnerGO { get; set; }
        public PlayerView PlayerView { get; set; }
        public CameraView CameraView { get; set; }
        public PlayerInputModel PlayerInputModel { get; set; }
        public FootstepsModel FootstepsModel { get; set; }
        public InventoryModel InventoryModel { get; set; }
        public PedestalModel PedestalModel { get; set; }
        public DialogModel DialogModel { get; set; }

        public GameObject Player;
    }
}