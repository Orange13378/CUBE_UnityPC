using Cinemachine;
using UnityEngine;
using UnityEngine.UI;

namespace CubeMVC
{
    public class ContextProvider : MonoBehaviour, IContextProvider
    {
        private Context _context;

        [SerializeField]
        private PlayerView _playerView;
        [SerializeField]
        private CameraView _cameraView;
        [SerializeField]
        private AudioClip[] _footSteps;
        [SerializeField]
        private GameObject _player, _pedestalGO, _pedestalCubeGO;
        [SerializeField]
        private GameObject[] _worlds, _pedestalsUI, _chests;
        [SerializeField]
        private PedestalItem[] _pedestalItems;
        [SerializeField]
        private CinemachineVirtualCamera _virtualCamera;
        [SerializeField]
        private Image WhiteScreenImage, BlackScreenImage;

        public Context GetContext()
        {
            if (_context != null)
                return _context;

            _context = new();
            _context.ContextOwnerGO = gameObject;
            _context.PlayerView = _playerView;
            _context.PlayerInputModel = new(); 
            _context.CameraView = _cameraView;
            _context.InventoryModel = new();
            _context.DialogModel = new(0.02f);

            _context.ChestModel = new();
            _context.ChestModel.Chests = _chests;
            _context.ChestModel.Chests = _chests;

            _context.CubeModel = new(_player, _virtualCamera, 
                _virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>());

            _context.ImageModel = new ImageModel(WhiteScreenImage, BlackScreenImage);

            _context.PedestalModel = new();
            _context.PedestalModel.PedestalGO = _pedestalGO;
            _context.PedestalModel.PedestalCubeGO = _pedestalCubeGO;
            _context.PedestalModel.Worlds = _worlds;
            _context.PedestalModel.PedestalsUI = _pedestalsUI;
            _context.PedestalModel.PedestalItems = _pedestalItems;

            _context.FootstepsModel = new(_footSteps);

            _context.PlayerAnimationModel = new(_player.GetComponentInChildren<Animator>());

            return _context;
        }

        private void Awake()
        {
            GetContext();
        }
    }
}