using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldControl : MonoBehaviour
{
    // Start is called before the first frame update

    [SerializeField] private GameObject whiteWorld, blueWorld, orangeWorld, purpleWorld, greenWorld, blackWorld;
    [SerializeField] private GameObject blueCube, orangeCube, purpleCube, greenCube, blackCube;

    public static bool goBlackWorld = false, goBlueWorld = false, goOrangeWorld = false, goGreenWorld = false, goPurpleWorld = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (goBlueWorld)
        {
            goBlueWorld = false;
            blueCube.SetActive(true);
            orangeCube.SetActive(true);
            blueWorld.SetActive(true);
            whiteWorld.SetActive(false);
            blackCube.SetActive(false);
            return;
        }

        if (goOrangeWorld)
        {
            goOrangeWorld = false;
            //orangeCube.SetActive(true);
            orangeWorld.SetActive(true);
            blueWorld.SetActive(false);
            return;
        }

        if (goGreenWorld)
        {
            goGreenWorld = false;
            purpleCube.SetActive(true);
            greenWorld.SetActive(true);
            blueWorld.SetActive(false);
            return;
        }

        if (goPurpleWorld)
        {
            goPurpleWorld= false;
            //purpleCube.SetActive(true);
            purpleWorld.SetActive(true);
            greenWorld.SetActive(false);
            return;
        }

        if (goBlackWorld)
        {
            goBlackWorld= false;
            //blackCube.SetActive(true);
            blackWorld.SetActive(true);
            whiteWorld.SetActive(false);
            return;
        }
    }
}
