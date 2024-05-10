using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlitePressed : MonoBehaviour {

	public PliteID plite;

    [System.NonSerialized] public bool electro_pressed = true;

    public enum States
{
    State0 = 0,
    State1 = 1,
    State2 = 2
}
    States st;
    private void Start()
    {
        st = States.State1;
    }
    
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (ElectroMech.electro == false)
            {
                electro_pressed = true;

                if (st == States.State2)
                {
                    st = States.State0;
                    PliteScripts.instance.OutCodeElectro(plite);
                }

                if (st == States.State0)
                {
                    gameObject.GetComponent<SpriteRenderer>().sprite = plite.icon;
                    PliteScripts.instance.OutCode(plite);
                }
                else if (st == States.State1)
                {
                    gameObject.GetComponent<SpriteRenderer>().sprite = plite.newIcon;
                    PliteScripts.instance.GetCode(plite);
                }
            }

            else if (ElectroMech.electro == true)
            {
                if (electro_pressed == true)
                {
                    st = States.State2;
                    electro_pressed = false;
                }
                
                if (st == States.State0)
                {
                    gameObject.GetComponent<SpriteRenderer>().sprite = plite.icon;
                    PliteScripts.instance.OutCode(plite);
                    PliteScripts.instance.OutCodeElectro(plite);
                }
                else if (st == States.State1)
                {
                    gameObject.GetComponent<SpriteRenderer>().sprite = plite.newIcon;
                    PliteScripts.instance.GetCode(plite);
                }
                else if (st == States.State2)
                {
                    gameObject.GetComponent<SpriteRenderer>().sprite = plite.electroIcon;
                    PliteScripts.instance.GetCodeElectro(plite);
                    PliteScripts.instance.OutCode(plite);
                }
            }
        }
    }

     private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            if (ElectroMech.electro == false)
            {
                if (st == States.State0) st = States.State1;
                else if (st == States.State1) st = States.State0;
            }
            else if (ElectroMech.electro == true)
            {
                if (st == States.State0) st = States.State1;
                else if (st == States.State1) st = States.State2;
                else if (st == States.State2) st = States.State0;
            }
        }
    }
}
