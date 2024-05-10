using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundController : MonoBehaviour
{
    public AudioClip DoorSound, KeySound, StoneSound;

    private AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void FixedUpdate()
    {
        //Inventory inventory = GetComponent<Inventory>();
        //if (inventory.keyPickUp)
        //{
        //    audioSource.clip = KeySound;
        //    audioSource.Play();
        //    inventory.keyPickUp = false;
        //}

        //if (inventory.stonePickUp)
        //{
        //    audioSource.clip = StoneSound;
        //    audioSource.Play();
        //    inventory.stonePickUp = false;
        //}
        //audioSource.clip = DoorSound;
        //audioSource.Play();

    }


}
