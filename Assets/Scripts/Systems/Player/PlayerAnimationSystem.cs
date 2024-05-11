using CubeECS;
using Leopotam.EcsLite;
using UnityEngine;

public class PlayerAnimationSystem : IEcsInitSystem, IEcsRunSystem
{
    private EcsFilter _filter;
    private EcsPool<PlayerInputComponent> _playerInputPool;
    private EcsPool<PlayerComponent> _playerPool;

    public void Init(IEcsSystems systems)
    {
        _filter = systems.GetWorld().Filter<PlayerInputComponent>().Inc<PlayerComponent>().End();

        _playerInputPool = systems.GetWorld().GetPool<PlayerInputComponent>();
        _playerPool = systems.GetWorld().GetPool<PlayerComponent>();
    }

    public void Run(IEcsSystems systems)
    {
        foreach (var entity in _filter)
        {
            ref var playerComponent = ref _playerPool.Get(entity);

            if (Input.GetKeyDown(KeyCode.R))
            {
                playerComponent.IsPlayerActive = true;
            }

            if (!playerComponent.IsPlayerActive)
                return;

            ref var playerInputComponent = ref _playerInputPool.Get(entity);
            playerComponent.PlayerAnimator.SetFloat("Horizontal", playerInputComponent.MoveInput.x);
            playerComponent.PlayerAnimator.SetFloat("Vertical", playerInputComponent.MoveInput.y);
            playerComponent.PlayerAnimator.SetFloat("Speed", playerInputComponent.MoveInput.sqrMagnitude);
        }
    }
}