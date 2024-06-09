using UnityEngine;

namespace CubeMVC
{
    public class Context
    {
        public PlayerView PlayerView { get; set; }
        public CameraView CameraView { get; set; }
        public PlayerInputModel PlayerInputModel { get; set; }
        public FootstepsModel FootstepsModel { get; set; }
        public InventoryModel InventoryModel { get; set; }
        public PedestalModel PedestalModel { get; set; }
        public DialogModel DialogModel { get; set; }
        public ChestModel ChestModel { get; set; }
        public CubeModel CubeModel { get; set; }
        public ImageModel ImageModel { get; set; }
        public PlayerAnimationModel PlayerAnimationModel { get; set; }
    }
}