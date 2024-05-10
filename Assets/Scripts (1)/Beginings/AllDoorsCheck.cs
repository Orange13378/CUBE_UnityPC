using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllDoorsCheck : MonoBehaviour
{
    public GameObject door1, door2;
    public GameObject openDoor1, openDoor2;
    private int blueCheck = 0, greenCheck = 0, redCheck = 0;

    // Update is called once per frame
    void Update()
    {
        redCheck = PlayerPrefs.GetInt("redCheck");
        blueCheck = PlayerPrefs.GetInt("blueCheck");
        greenCheck = PlayerPrefs.GetInt("greenCheck");

        if (blueCheck == 1 && redCheck == 1 && greenCheck == 1)
        {
            door1.SetActive(false);
            door2.SetActive(false);
            openDoor1.SetActive(true);
            openDoor2.SetActive(true);
        }
        else if (blueCheck == 0 | redCheck == 0 | greenCheck == 0)
        {
            door1.SetActive(true);
            door2.SetActive(true);
            openDoor1.SetActive(false);
            openDoor2.SetActive(false);
        }
    }
}
