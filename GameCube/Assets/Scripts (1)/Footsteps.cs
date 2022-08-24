using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Footsteps : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip[] footSteps;
    public float time;
    private float timer = 0f;
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        timer = 0.1f;
    }

    void Update()
    {
        if (Input.GetButton("Horizontal") | Input.GetButton("Vertical"))
        {
            if (timer > 0)
            {
                timer -= Time.deltaTime;
            }

            if (timer < 0)
            {
                audioSource.PlayOneShot(footSteps[Random.Range(0, footSteps.Length)]);
                timer = time;
            }
        }
    }

}
