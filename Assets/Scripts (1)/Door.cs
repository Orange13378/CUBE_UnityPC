using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public Animator doorAnim;
    public GameObject door, codePanel, panel;
    private bool entered, close;

    [System.NonSerialized] public static bool correct;
    void Start()
    {
        correct = false;
        entered = false;
        close = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (entered)
        {
            if ((Input.GetKeyDown(KeyCode.E)) && !correct)
            {
                DisablePlayerScript.off = true;
                codePanel.SetActive(true);
            }
        }

        if (correct & close) 
        {
            DisablePlayerScript.on = true;
            doorAnim.SetBool("open", true);
            panel.SetActive(false);
            StartCoroutine(Open());
            close = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            entered = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            entered = false;
        }
    }

    IEnumerator Open()
    {
        yield return new WaitForSeconds(4f);
        door.GetComponent<BoxCollider2D>().enabled = false;
        this.gameObject.SetActive(false);
    }
}
