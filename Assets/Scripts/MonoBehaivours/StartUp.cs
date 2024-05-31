using Cinemachine;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;
using UnityEngine.UI;

namespace CubeECS
{
    public class StartUp : MonoBehaviour
    {
        private EcsWorld _world;
        private IEcsSystems initSystems;
        private IEcsSystems updateSystems;
        private IEcsSystems fixedUpdateSystems;
        [SerializeField] private CinemachineVirtualCamera virtualCamera;
        [SerializeField] private Image whiteScreenImage;
        [SerializeField] private Image blackScreenImage;
        [SerializeField] private ConfigurationSO configuration;
        [SerializeField] private GameObject player;
        [SerializeField] private GameObject dialogPanel;
        [SerializeField] private GameObject pedestalGO;
        [SerializeField] private GameObject pedestalCubeGO;
        [SerializeField] private AudioClip[] footStepsAudioClips;
        [SerializeField] private Item[] items;
        [SerializeField] private Sprite[] chestSprites;
        [SerializeField] private GameObject[] chests;
        [SerializeField] private GameObject[] worlds;
        [SerializeField] private PedestalItem[] pedestals;
        [SerializeField] private GameObject[] pedestalsUI;
        [SerializeField] private DialogSystem dialogSystem;

        private void Awake()
        {
            _world = new EcsWorld();
            EcsWorldManager.SetEcsWorld(_world);

            var gameData = new GameData();
            gameData.VirtualCamera = virtualCamera;
            gameData.WhiteScreenImage = whiteScreenImage;
            gameData.BlackScreenImage = blackScreenImage;
            gameData.Configuration = configuration;
            gameData.Player = player;
            gameData.DialogPanel = dialogPanel;
            gameData.PedestalGO = pedestalGO;
            gameData.PedestalCubeGO = pedestalCubeGO;
            gameData.FootStepsAudioClips = footStepsAudioClips;
            gameData.Items = items;
            gameData.OpenedChestSprites = chestSprites;
            gameData.Chests = chests;
            gameData.Worlds = worlds;
            gameData.Pedestals = pedestals;
            gameData.PedestalsUI = pedestalsUI;
            gameData.DialogSystem = dialogSystem;
            //gameData.sceneService = Service<SceneService>.Get(true);

            initSystems = new EcsSystems(_world, gameData)
                    .Add(new PlayerInitSystem())
                    .Add(new InventoryInitSystem())
                    .Add(new DialogInitSystem())
                    .Add(new CubeInitSystem())
                    .Inject(gameData)
                ;

            initSystems?.Init();

            updateSystems = new EcsSystems(_world, gameData)
                    .Add(new PlayerInputSystem())
                    .Add(new FootstepsSystem())
                    .Add(new PlayerAnimationSystem())
                    .Add(new ChestOpenSystem())
                    .Add(new DisablePlayerSystem())
                    .Add(new PedestalSystem())
                    .Add(new WhiteImageSystem())

#if UNITY_EDITOR
                    .Add(new Leopotam.EcsLite.UnityEditor.EcsWorldDebugSystem())
#endif
                    .Inject(gameData)
                ;

            updateSystems?.Init();

            fixedUpdateSystems = new EcsSystems(_world, gameData)
                .Add(new PlayerMoveSystem())
                .Add(new CameraFollowSystem())
                .Inject(gameData);

            fixedUpdateSystems?.Init();
        }

        private void Update()
        {
            updateSystems?.Run();
        }

        private void FixedUpdate()
        {
            fixedUpdateSystems?.Run();
        }

        private void OnDestroy()
        {
            initSystems?.Destroy();
            updateSystems?.Destroy();
            fixedUpdateSystems?.Destroy();
            _world?.Destroy();
        }
    }

    public static class EcsWorldManager
    {
        private static EcsWorld _ecsWorld;

        public static void SetEcsWorld(EcsWorld world)
        {
            _ecsWorld = world;
        }

        public static EcsWorld GetEcsWorld()
        {
            if (_ecsWorld == null)
            {
                Debug.LogError("EcsWorld is not initialized!");
            }
            return _ecsWorld;
        }
    }
}
