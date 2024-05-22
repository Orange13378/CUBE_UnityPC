using UnityEngine;

public class PressE : MonoBehaviour
{
    [SerializeField] GameObject buttonE;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            buttonE.SetActive(true);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            buttonE.SetActive(false);
        }
    }
}
