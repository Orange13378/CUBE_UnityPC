using CubeMVC;
using UnityEngine;

public class PrintText : MonoBehaviour
{
    [SerializeField] 
    [TextArea] string text = null;

    private ContextProvider _contextProvider;
    private DialogModel _dialogModel;

    private void Start()
    {
        _contextProvider = FindObjectOfType<ContextProvider>();
        _dialogModel = _contextProvider.GetContext().DialogModel;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) 
            return;

        _dialogModel.OnDialogStart(text);

        gameObject.SetActive(false);
    }
}
