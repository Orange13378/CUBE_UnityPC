using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManagerScript : MonoBehaviour
{
    public Animator contentPanel;

    public void ToggleMenu()
    {
        bool isHidden = contentPanel.GetBool("isHidden");
        contentPanel.SetBool("isHidden", !isHidden);
    }

    // Update is called once per frame
    void Update()
    {
        if(ItemPickup.time)
        {
            contentPanel.SetBool("Time", true);
            ItemPickup.time = false;
        }

        if(ItemPickup.termo)
        {
            contentPanel.SetBool("Termo", true);
            ItemPickup.termo = false;
        }

        if(ItemPickup.magnit)
        {
            contentPanel.SetBool("Magnit", true);
            ItemPickup.magnit = false;
        }

        if(ItemPickup.electro)
        {
            contentPanel.SetBool("Electro", true);
            ItemPickup.electro = false;
        }
    }
}
