using System.Collections;
using CubeMVC;
using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField]
    private Animator doorAnim;
    [SerializeField]
    private GameObject door, codePanel, panel;
    private bool entered, close;

    [SerializeField]
    private ContextProvider _contextProvider;
    private PlayerInputModel _inputModel;

    [System.NonSerialized] public static bool correct;
    private void Start()
    {
        correct = false;
        entered = false;
        close = true;

        _inputModel = _contextProvider.GetContext().PlayerInputModel;
    }

    private void Update()
    {
        if (entered)
        {
            if (_inputModel.PressedX.Value && !correct)
                codePanel.SetActive(true);
        }

        if (correct & close) 
        {
            doorAnim.SetBool("open", true);
            panel.SetActive(false);
            StartCoroutine(Open());
            close = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            entered = true;
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            entered = false;
    }

    IEnumerator Open()
    {
        yield return new WaitForSeconds(4f);
        door.GetComponent<BoxCollider2D>().enabled = false;
        gameObject.SetActive(false);
    }
}
