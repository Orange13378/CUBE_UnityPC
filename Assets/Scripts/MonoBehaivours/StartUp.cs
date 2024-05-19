using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

namespace CubeECS
{
    public class StartUp : MonoBehaviour
    {
        private EcsWorld _world;
        private IEcsSystems initSystems;
        private IEcsSystems updateSystems;
        private IEcsSystems fixedUpdateSystems;
        [SerializeField] private ConfigurationSO configuration;
        [SerializeField] private GameObject dialogPanel;
        [SerializeField] private AudioClip[] footStepsAudioClips;
        [SerializeField] private Item[] items;
        [SerializeField] private Sprite[] chestSprites;
        [SerializeField] private GameObject[] chests;
        [SerializeField] private DialogSystem dialogSystem;

        private void Awake()
        {
            _world = new EcsWorld();
            EcsWorldManager.SetEcsWorld(_world);

            var gameData = new GameData();

            gameData.Configuration = configuration;
            gameData.DialogPanel = dialogPanel;
            gameData.FootStepsAudioClips = footStepsAudioClips;
            gameData.Items = items;
            gameData.OpenedChestSprites = chestSprites;
            gameData.Chests = chests;
            gameData.DialogSystem = dialogSystem;
            //gameData.sceneService = Service<SceneService>.Get(true);

            initSystems = new EcsSystems(_world, gameData)
                    .Add(new PlayerInitSystem())
                    .Add(new InventoryInitSystem())
                    .Add(new DialogInitSystem())
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
                UnityEngine.Debug.LogError("EcsWorld is not initialized!");
            }
            return _ecsWorld;
        }
    }
}
