using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElectroMech : MonoBehaviour
{
    // Start is called before the first frame update
    [System.NonSerializedAttribute] public static bool electro, switched;
    void Start()
    {
        electro = false;
        switched = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TurnOn()
    {
        electro = true;
        switched = true;
    }

    public void TurnOff()
    {
        electro = false;
        switched = true;
    }
}
