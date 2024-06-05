using UnityEngine;

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
        private GameObject _player;
        [SerializeField]
        private GameObject _pedestalGO;
        [SerializeField]
        private GameObject _pedestalCubeGO;
        [SerializeField]
        private GameObject[] _worlds;
        [SerializeField]
        private GameObject[] _pedestalsUI;
        [SerializeField]
        private PedestalItem[] _pedestalItems;

        public Context GetContext()
        {
            if (_context != null)
                return _context;

            _context = new();
            _context.ContextOwnerGO = gameObject;
            _context.Player = _player;
            _context.PlayerView = _playerView;
            _context.CameraView = _cameraView;
            _context.PlayerInputModel = new();
            _context.InventoryModel = new();
            _context.DialogModel = new(0.02f);

            _context.PedestalModel = new();
            _context.PedestalModel.PedestalGO = _pedestalGO;
            _context.PedestalModel.PedestalCubeGO = _pedestalCubeGO;
            _context.PedestalModel.Worlds = _worlds;
            _context.PedestalModel.PedestalsUI = _pedestalsUI;
            _context.PedestalModel.PedestalItems = _pedestalItems;

            _context.FootstepsModel = new(_footSteps);

            return _context;
        }

        private void Awake()
        {
            GetContext();
        }
    }
}