using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestCode : MonoBehaviour
{
    public GameObject codePanel, player;
    public AudioClip codeSound;
    private AudioSource audioSource;
    private bool paused = false, entered = false;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {        
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (entered)
            {
                if (!paused)
                {
                    audioSource.clip = codeSound;
                    audioSource.Play();
                    paused = true;
                }
                codePanel.SetActive(true);
                player.gameObject.GetComponent<Player1>().enabled = false;

                Time.timeScale = 0;
            }
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (entered)
            {
                paused = false;
                player.gameObject.GetComponent<Player1>().enabled = true;
                codePanel.SetActive(false);
                Time.timeScale = 1;
            }
        }

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            entered = true;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            entered = false;
        }
    }
}
