using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class DisablePlayerScript : MonoBehaviour
{
    public GameObject player, footsteps;

    public static bool on = false, off = false, antiMouse = false;
    public AnimationClip a;

    void Update()
    {
        if(on)
        {
            on = false;
            On();
        }

        if(off)
        {
            off = false;
            Off();
        }
    }

    public void Off()
    {
        if(!antiMouse)
        {
            player.gameObject.GetComponent<Player1>().enabled = false;
            Animator animator = player.gameObject.GetComponent<Animator>();
            animator.SetBool("Stoped", true);
            footsteps.gameObject.GetComponent<AudioSource>().enabled = false;
        }
    }

    public void On()
    {
        if(!antiMouse)
        {
            player.gameObject.GetComponent<Player1>().enabled = true;
            Animator animator = player.gameObject.GetComponent<Animator>();
            animator.SetBool("Stoped", false);
            footsteps.gameObject.GetComponent<AudioSource>().enabled = true;
        }
    }
}

namespace CubeECS
{
    public struct DisablePlayerComponent
    {
        public bool Deactivate;
    }

    public class DisablePlayerSystem : IEcsRunSystem
    {
        private EcsWorldInject _world;
        private EcsFilterInject<Inc<DisablePlayerComponent>> _disablePlayerFilter;
        private EcsFilterInject<Inc<PlayerComponent>> _playerFilter;
        private EcsPoolInject<DisablePlayerComponent> _disablePlayerPool;
        private EcsPoolInject<PlayerComponent> _playerPool;
        private EcsCustomInject<GameData> _gameData;

        public void Run(IEcsSystems systems)
        {
            foreach (var disablePlayerEntity in _disablePlayerFilter.Value)
            {
                ref var disablePlayerComponent = ref _disablePlayerPool.Value.Get(disablePlayerEntity);
                
                if (disablePlayerComponent.Deactivate)
                    DeactivatePlayer();
                else
                    ActivatePlayer();

                _disablePlayerPool.Value.Del(disablePlayerEntity);
            }
        }

        private void DeactivatePlayer()
        {
            foreach (var entity in _playerFilter.Value)
            {
                ref var player = ref _playerPool.Value.Get(entity);
                player.IsPlayerActive = false;
            }
        }

        private void ActivatePlayer()
        {
            foreach (var entity in _playerFilter.Value)
            {
                ref var player = ref _playerPool.Value.Get(entity);
                player.IsPlayerActive = true;
            }
        }
    }
}