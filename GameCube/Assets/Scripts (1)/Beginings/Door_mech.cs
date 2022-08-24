using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door_mech : MonoBehaviour
{
    public GameObject block;
    public Sprite newSprite;
    public AudioClip buttonSound;
    private AudioSource audioSource;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }


    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            block.SetActive(false);
            audioSource.clip = buttonSound;
            audioSource.Play();
            gameObject.GetComponent<SpriteRenderer>().sprite = newSprite;
        }

    }

}
