using Leopotam.EcsLite;
using UnityEngine;
using UnityEngine.UI;

namespace CubeECS
{
    public class StartUp : MonoBehaviour
    {
        private EcsWorld ecsWorld;
        private IEcsSystems initSystems;
        private IEcsSystems updateSystems;
        private IEcsSystems fixedUpdateSystems;
        [SerializeField] private ConfigurationSO configuration;
        [SerializeField] private Text coinCounter;
        [SerializeField] private GameObject playerWonPanel;
        [SerializeField] private AudioClip[] footStepsAudioClips;
        [SerializeField] private Item[] items;
        [SerializeField] private Sprite[] chestSprites;
        [SerializeField] private GameObject[] chests;

        private void Awake()
        {
            ecsWorld = new EcsWorld();
            EcsWorldManager.SetEcsWorld(ecsWorld);

            var gameData = new GameData();

            gameData.Configuration = configuration;
            gameData.CoinCounter = coinCounter;
            gameData.PlayerWonPanel = playerWonPanel;
            gameData.FootStepsAudioClips = footStepsAudioClips;
            gameData.Items = items;
            gameData.OpenedChestSprites = chestSprites;
            gameData.Chests = chests;
            //gameData.sceneService = Service<SceneService>.Get(true);

            initSystems = new EcsSystems(ecsWorld, gameData)
                    .Add(new PlayerInitSystem())
                    .Add(new InventoryInitSystem())
                    .Add(new ChestInitSystem())
                ;

            initSystems?.Init();

            updateSystems = new EcsSystems(ecsWorld, gameData)
                    .Add(new PlayerInputSystem())
                    .Add(new FootstepsSystem())
                    .Add(new PlayerAnimationSystem())
                    .Add(new ChestOpenSystem())
                ;

            updateSystems?.Init();

            fixedUpdateSystems = new EcsSystems(ecsWorld, gameData)
                .Add(new PlayerMoveSystem())
                .Add(new CameraFollowSystem());

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
            ecsWorld?.Destroy();
        }
    }
}
