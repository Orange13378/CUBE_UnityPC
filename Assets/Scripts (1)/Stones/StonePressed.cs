using CubeECS;
using Leopotam.EcsLite;
using UnityEngine;

public class StonePressed : MonoBehaviour
{
    public Stones stone;

    [System.NonSerialized] public static bool pressedE;
    private bool _isEntered;

    private SpriteRenderer _spriteRenderer;

    private EcsFilter _playerInputFilter;
    private EcsPool<PlayerInputComponent> _playerInputPool;

    private void Start()
    {
        _isEntered = false;
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();

        var world = EcsWorldManager.GetEcsWorld();
        _playerInputFilter = world.Filter<PlayerInputComponent>().End();
        _playerInputPool = world.GetPool<PlayerInputComponent>();
    }

    private void Update()
    {
        if (ElectroMech.switched && !ElectroMech.electro) 
        {
            _spriteRenderer.sprite = stone.icon;
            ElectroMech.switched = false;
        }

        if (ElectroMech.switched && ElectroMech.electro)
        {
            _spriteRenderer.sprite = stone.newIcon;
            ElectroMech.switched = false;
        }

        if (!_isEntered) return;

        foreach (var entity in _playerInputFilter)
        {
            ref var playerInputComponent = ref _playerInputPool.Get(entity);
            if (!playerInputComponent.PressedX)
                return;
        }
            
        pressedE = false;

        if (ElectroMech.electro == false && !pressedE)
        {
            StoneScript.instance.GetText(stone);
            pressedE = true;
        }
        else if (ElectroMech.electro == true && !pressedE)
        {
            StoneScript.instance.GetElectroText(stone);
            pressedE = true;
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            _isEntered = true;
    }

     private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            _isEntered = false;
    }
}
