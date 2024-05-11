using CubeECS;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;

public class PlayerAnimationSystem : IEcsRunSystem
{
    private EcsFilterInject<Inc<PlayerInputComponent, PlayerComponent>> _filters;
    private EcsPoolInject<PlayerInputComponent> _playerInputPool;
    private EcsPoolInject<PlayerComponent> _playerPool;

    public void Run(IEcsSystems systems)
    {
        foreach (var entity in _filters.Value)
        {
            ref var playerComponent = ref _playerPool.Value.Get(entity);

            if (Input.GetKeyDown(KeyCode.R))
            {
                playerComponent.IsPlayerActive = true;
            }

            if (!playerComponent.IsPlayerActive)
                return;

            ref var playerInputComponent = ref _playerInputPool.Value.Get(entity);
            playerComponent.PlayerAnimator.SetFloat("Horizontal", playerInputComponent.MoveInput.x);
            playerComponent.PlayerAnimator.SetFloat("Vertical", playerInputComponent.MoveInput.y);
            playerComponent.PlayerAnimator.SetFloat("Speed", playerInputComponent.MoveInput.sqrMagnitude);
        }
    }
}