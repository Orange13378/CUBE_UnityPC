using CubeMVC;
using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonXHandler : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField]
    private ContextProvider _contextProvider;

    public void OnPointerDown(PointerEventData eventData)
    {
        _contextProvider.GetContext().PlayerInputModel.PressedX.Value = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        _contextProvider.GetContext().PlayerInputModel.PressedX.Value = false;
    }
}
