using Leopotam.EcsLite;
using UnityEngine;

namespace CubeECS
{
    public class PlayerInitSystem : IEcsInitSystem
    {
        public void Init(IEcsSystems systems)
        {
            var ecsWorld = systems.GetWorld();
            var gameData = systems.GetShared<GameData>();

            var playerEntity = ecsWorld.NewEntity();

            var playerPool = ecsWorld.GetPool<PlayerComponent>();
            playerPool.Add(playerEntity);
            ref var playerComponent = ref playerPool.Get(playerEntity);
            var playerInputPool = ecsWorld.GetPool<PlayerInputComponent>();
            playerInputPool.Add(playerEntity);

            var playerGO = GameObject.FindGameObjectWithTag("Player");
            playerComponent.IsPlayerActive = true;
            playerComponent.PlayerSpeed = gameData.Configuration.PlayerSpeed;
            playerComponent.PlayerTransform = playerGO.transform;
            playerComponent.PlayerCollider = playerGO.GetComponent<BoxCollider2D>();
            playerComponent.PlayerRB = playerGO.GetComponent<Rigidbody2D>();
            playerComponent.PlayerAudioSource = playerGO.GetComponentInChildren<AudioSource>();
            playerComponent.PlayerAnimator = playerGO.GetComponentInChildren<Animator>();
        }
    }
}