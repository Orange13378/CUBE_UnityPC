using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempoCheck : MonoBehaviour
{
    // Start is called before the first frame update
    private bool minusT = false, plusT = false, zeroT = false;
    public GameObject ice, block;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(TempoMech.minus && !minusT)
        {
            minusT = true;
            plusT = false;
            zeroT = false;
            ice.SetActive(true);
            block.SetActive(false);

        }

        if(TempoMech.zero && !zeroT)
        {
            minusT = false;
            plusT = false;
            zeroT = true;
            ice.SetActive(false);
            block.SetActive(true);
        }

        if(TempoMech.plus && !plusT)
        {
            minusT = false;
            plusT = true;
            zeroT = false;
            ice.SetActive(false);
            block.SetActive(false);
        }
    }
}
