using CubeMVC;
using UnityEngine;

public class StonePressed : MonoBehaviour
{
    public Stones stone;

    [SerializeField]
    private ContextProvider _contextProvider;

    private PlayerInputModel _inputModel;

    [System.NonSerialized] public static bool pressedE;
    private bool _isEntered;

    private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        _inputModel = _contextProvider.GetContext().PlayerInputModel;

        _isEntered = false;
        _spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
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

        if (!_isEntered || !_inputModel.PressedX.Value) 
            return;
            
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
