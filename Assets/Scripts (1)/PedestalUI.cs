using Cysharp.Threading.Tasks;
using UnityEngine;
using static UnityEngine.EventSystems.EventTrigger;

public class PedestalUI : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject pedestalUI6, pedestalUI5, pedestalUI4, pedestalUI3, pedestalUI2, player;
    public GameObject whiteWorld, blueWorld, orangeWorld, purpleWorld, greenWorld, blackWorld, cube, blackCube;
    [SerializeField]
    private Sprite sprite_White, sprite_Black, sprite_Blue, sprite_Green, sprite_Orange, sprite_Purple;

    [SerializeField]
    private Sprite sprite_CBlack, sprite_CBlue, sprite_CGreen, sprite_COrange, sprite_CPurple;

    public static bool goBlackWorld, goBlueWorld, goOrangeWorld, goGreenWorld, goPurpleWorld;
    private bool entered_pedestal;

    public GameObject greenCube;

    int i = 0;

    void Update()
    {
        if (goBlueWorld)
        {
            i = 2;
            goBlueWorld = false;
            gameObject.GetComponent<SpriteRenderer>().sprite = sprite_Blue;
            cube.GetComponent<SpriteRenderer>().sprite = sprite_CBlue;

        }

        if (goOrangeWorld)
        {
            i = 3;
            goOrangeWorld = false;
            gameObject.GetComponent<SpriteRenderer>().sprite = sprite_Orange;
            cube.GetComponent<SpriteRenderer>().sprite = sprite_COrange;
        }


        if (goGreenWorld)
        {
            i = 4;
            goGreenWorld = false;
            gameObject.GetComponent<SpriteRenderer>().sprite = sprite_Green;
            cube.GetComponent<SpriteRenderer>().sprite = sprite_CGreen;
        }

        if (goPurpleWorld)
        {
            i = 5;
            goPurpleWorld= false;
            greenCube.SetActive(true);
            gameObject.GetComponent<SpriteRenderer>().sprite = sprite_Purple;
            cube.GetComponent<SpriteRenderer>().sprite = sprite_CPurple;
        }

        if (goBlackWorld)
        {
            i = 6;
            goBlackWorld= false;
            gameObject.GetComponent<SpriteRenderer>().sprite = sprite_Black;
            cube.GetComponent<SpriteRenderer>().sprite = sprite_CBlack;
        }

        if(entered_pedestal)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                switch(i)
                {
                    case 2:
                    pedestalUI2.SetActive(true);
                    break;

                    case 3:
                    pedestalUI3.SetActive(true);
                    break;

                    case 4:
                    pedestalUI4.SetActive(true);
                    break;

                    case 5:
                    pedestalUI5.SetActive(true);
                    break;

                    case 6:
                    pedestalUI6.SetActive(true);
                    break;

                    default: 
                    if (DialogSystem.message.Count == 0)
                    {
                        DialogSystem.message.Add("Ничего не происходит");
                        DialogSystem.on = true;
                    }
                    break;
                }

                Stop();
            }
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            entered_pedestal = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            entered_pedestal = false;
        }
    }

    public void PressedWhite(){
        whiteWorld.SetActive(true);
        blueWorld.SetActive(false);
        orangeWorld.SetActive(false);
        purpleWorld.SetActive(false);
        greenWorld.SetActive(false);
        blackWorld.SetActive(false);
        pedestalUI6.SetActive(false);
        pedestalUI5.SetActive(false);
        pedestalUI4.SetActive(false);
        pedestalUI3.SetActive(false);
        pedestalUI2.SetActive(false);
        gameObject.GetComponent<SpriteRenderer>().sprite = sprite_White;
        blackCube.SetActive(true);
        Go();
    }

    public void PressedBlue(){
        whiteWorld.SetActive(false);
        blueWorld.SetActive(true);
        orangeWorld.SetActive(false);
        purpleWorld.SetActive(false);
        greenWorld.SetActive(false);
        blackWorld.SetActive(false);
        pedestalUI6.SetActive(false);
        pedestalUI5.SetActive(false);
        pedestalUI4.SetActive(false);
        pedestalUI3.SetActive(false);
        pedestalUI2.SetActive(false);
        gameObject.GetComponent<SpriteRenderer>().sprite = sprite_Blue;
        blackCube.SetActive(false);
        Go();
    }

    public void PressedOrange(){
        whiteWorld.SetActive(false);
        blueWorld.SetActive(false);
        orangeWorld.SetActive(true);
        purpleWorld.SetActive(false);
        greenWorld.SetActive(false);
        blackWorld.SetActive(false);
        pedestalUI6.SetActive(false);
        pedestalUI5.SetActive(false);
        pedestalUI4.SetActive(false);
        pedestalUI3.SetActive(false);
        pedestalUI2.SetActive(false);
        gameObject.GetComponent<SpriteRenderer>().sprite = sprite_Orange;
        Go();
        blackCube.SetActive(false);
    }

    public void PressedPurple(){
        whiteWorld.SetActive(false);
        blueWorld.SetActive(false);
        orangeWorld.SetActive(false);
        purpleWorld.SetActive(true);
        greenWorld.SetActive(false);
        blackWorld.SetActive(false);
        pedestalUI6.SetActive(false);
        pedestalUI5.SetActive(false);
        pedestalUI4.SetActive(false);
        pedestalUI3.SetActive(false);
        pedestalUI2.SetActive(false);
        gameObject.GetComponent<SpriteRenderer>().sprite = sprite_Purple;
        Go();
        blackCube.SetActive(false);
    }

    public void PressedGreen(){
        whiteWorld.SetActive(false);
        blueWorld.SetActive(false);
        orangeWorld.SetActive(false);
        purpleWorld.SetActive(false);
        greenWorld.SetActive(true);
        blackWorld.SetActive(false);
        pedestalUI6.SetActive(false);
        pedestalUI5.SetActive(false);
        pedestalUI4.SetActive(false);
        pedestalUI3.SetActive(false);
        pedestalUI2.SetActive(false);
        gameObject.GetComponent<SpriteRenderer>().sprite = sprite_Green;
        Go();
        blackCube.SetActive(false);
    }

    public void PressedBlack(){
        whiteWorld.SetActive(false);
        blueWorld.SetActive(false);
        orangeWorld.SetActive(false);
        purpleWorld.SetActive(false);
        greenWorld.SetActive(false);
        blackWorld.SetActive(true);
        gameObject.GetComponent<SpriteRenderer>().sprite = sprite_Black;
        pedestalUI6.SetActive(false);
        pedestalUI5.SetActive(false);
        pedestalUI4.SetActive(false);
        pedestalUI3.SetActive(false);
        pedestalUI2.SetActive(false);
        Go();
        blackCube.SetActive(false);
    }

    public void Stop()
    {
        player.gameObject.GetComponent<Player1>().enabled = false;
        //footStep.gameObject.GetComponent<Footsteps>().enabled = false; TODO
    }
    public void Go()
    {
        player.gameObject.GetComponent<Player1>().enabled = true;
        //footStep.gameObject.GetComponent<Footsteps>().enabled = true; TODO
    }
}