using CubeECS;
using Leopotam.EcsLite;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonXHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private EcsWorld _world;
    private EcsFilter _filter;
    private EcsPool<PlayerInputComponent> _playerInputPool;

    private void Start()
    {
        _world = EcsWorldManager.GetEcsWorld();
        _filter = _world.Filter<PlayerInputComponent>().End();
        _playerInputPool = _world.GetPool<PlayerInputComponent>();
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        foreach (var entity in _filter)
        {
            ref var playerInputComponent = ref _playerInputPool.Get(entity);
            playerInputComponent.PressedX = true;
        }
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        foreach (var entity in _filter)
        {
            ref var playerInputComponent = ref _playerInputPool.Get(entity);
            playerInputComponent.PressedX = false;
        }
    }
}
