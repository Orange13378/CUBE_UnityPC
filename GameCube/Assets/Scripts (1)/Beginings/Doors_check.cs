using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Doors_check : MonoBehaviour
{
    public Sprite newBlueSprite, newRedSprite, newGreenSprite;
    public Sprite blueSprite, redSprite, greenSprite;
    public AudioClip buttonSound;
    public AudioClip doorOpenSound, doorNotOpenSound;
    private AudioSource audioSource, audioSource1;
    public GameObject door_check;
    private int blueCheck = 0, greenCheck = 0, redCheck = 0;
    private int work = 0;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        audioSource1 = door_check.GetComponent<AudioSource>();
        PlayerPrefs.SetInt("blueCheck", blueCheck);
        PlayerPrefs.SetInt("redCheck", redCheck);
        PlayerPrefs.SetInt("greenCheck", greenCheck);
        PlayerPrefs.SetInt("work", work);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("BlueButton"))
        {
            audioSource.clip = buttonSound;
            audioSource.Play();
            other.GetComponent<SpriteRenderer>().sprite = newBlueSprite;
            if (gameObject.CompareTag("BlueBox"))
            {
                blueCheck = 1;
                PlayerPrefs.SetInt("blueCheck", blueCheck);
            }
            redCheck = PlayerPrefs.GetInt("redCheck");
            blueCheck = PlayerPrefs.GetInt("blueCheck");
            greenCheck = PlayerPrefs.GetInt("greenCheck");
            if (blueCheck == 1 && redCheck == 1 && greenCheck == 1)
            {
                audioSource.clip = doorOpenSound;
                audioSource.Play();
                work = 1;
                PlayerPrefs.SetInt("work", work);
            }
        }

        if (other.CompareTag("RedButton"))
        {
            audioSource.clip = buttonSound;
            audioSource.Play();
            other.GetComponent<SpriteRenderer>().sprite = newRedSprite;
            if (gameObject.CompareTag("RedBox"))
            {
                redCheck = 1;
                PlayerPrefs.SetInt("redCheck", redCheck);
            }
            redCheck = PlayerPrefs.GetInt("redCheck");
            blueCheck = PlayerPrefs.GetInt("blueCheck");
            greenCheck = PlayerPrefs.GetInt("greenCheck");
            if (blueCheck == 1 && redCheck == 1 && greenCheck == 1)
            {
                audioSource.clip = doorOpenSound;
                audioSource.Play();
                work = 1;
                PlayerPrefs.SetInt("work", work);
            }
        }

        if (other.CompareTag("GreenButton"))
        {
            audioSource.clip = buttonSound;
            audioSource.Play();
            other.GetComponent<SpriteRenderer>().sprite = newGreenSprite;
            if (gameObject.CompareTag("GreenBox"))
            {
                greenCheck = 1;
                PlayerPrefs.SetInt("greenCheck", greenCheck);
            }
            redCheck = PlayerPrefs.GetInt("redCheck");
            blueCheck = PlayerPrefs.GetInt("blueCheck");
            greenCheck = PlayerPrefs.GetInt("greenCheck");
            if (blueCheck == 1 && redCheck == 1 && greenCheck == 1)
            {
                audioSource.clip = doorOpenSound;
                audioSource.Play();
                work = 1;
                PlayerPrefs.SetInt("work", work);
            }
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("BlueButton") | other.CompareTag("RedButton") | other.CompareTag("GreenButton"))
        {
            work = PlayerPrefs.GetInt("work");
            redCheck = PlayerPrefs.GetInt("redCheck");
            blueCheck = PlayerPrefs.GetInt("blueCheck");
            greenCheck = PlayerPrefs.GetInt("greenCheck");

            if (work == 1)
            {
                audioSource1.clip = doorNotOpenSound;
                audioSource1.Play();
            }

            else if (other.CompareTag("BlueButton") && work != 1)
            {
                blueCheck = 0;
                PlayerPrefs.SetInt("blueCheck", blueCheck);
                other.GetComponent<SpriteRenderer>().sprite = blueSprite;
                audioSource.clip = buttonSound;
                audioSource.Play();
            }
            
            else if (other.CompareTag("RedButton") && work != 1)
            {
                redCheck = 0;
                PlayerPrefs.SetInt("redCheck", redCheck);
                other.GetComponent<SpriteRenderer>().sprite = redSprite;
                audioSource.clip = buttonSound;
                audioSource.Play();
            }

            else if (other.CompareTag("GreenButton") && work != 1)
            {
                greenCheck = 0;
                PlayerPrefs.SetInt("greenCheck", greenCheck);
                other.GetComponent<SpriteRenderer>().sprite = greenSprite;
                audioSource.clip = buttonSound;
                audioSource.Play();
            }
            work = 0;
            PlayerPrefs.SetInt("work", work);
        }
    }
}
