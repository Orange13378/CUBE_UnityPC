using CubeECS;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;

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

            if (!playerComponent.IsPlayerActive)
            {
                playerComponent.PlayerAnimator.SetBool("Stoped", true);
                return;
            }

            playerComponent.PlayerAnimator.SetBool("Stoped", false);
            ref var playerInputComponent = ref _playerInputPool.Value.Get(entity);
            playerComponent.PlayerAnimator.SetFloat("Horizontal", playerInputComponent.MoveInput.x);
            playerComponent.PlayerAnimator.SetFloat("Vertical", playerInputComponent.MoveInput.y);
            playerComponent.PlayerAnimator.SetFloat("Speed", playerInputComponent.MoveInput.sqrMagnitude);
        }
    }
}