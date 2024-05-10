using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StonePressed : MonoBehaviour
{
    // Start is called before the first frame update
    public Stones stone;

    [System.NonSerialized] public static bool pressedE = false;
    bool entered;

    
    private void Start()
    {
        entered = false;
    }

    private void Update()
    {

        if (ElectroMech.switched && !ElectroMech.electro) 
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = stone.icon;
            ElectroMech.switched = false;
        }

        if (ElectroMech.switched && ElectroMech.electro)
        {
            gameObject.GetComponent<SpriteRenderer>().sprite = stone.newIcon;
            ElectroMech.switched = false;
        }

        if (entered)
        {
            if((Input.GetKeyDown(KeyCode.E)))
            {
                pressedE = false;
                if (ElectroMech.electro == false && !pressedE)
                {
                    StoneScript.instance.GetText(stone);
                    pressedE = true;
                }
                else if (ElectroMech.electro == true && !pressedE)
                {
                    StoneScript.instance.GetElectroText(stone);
                    pressedE = true;
                }
            }
        }
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            entered = true;
        }
    }

     private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            entered = false;
        }
    }
}
