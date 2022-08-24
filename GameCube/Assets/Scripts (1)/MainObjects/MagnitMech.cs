using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnitMech : MonoBehaviour
{
    public static bool magnit;

    void Start()
    {
        magnit = false;
    }

    public void PressedNorth()
    {
        magnit = true;
    }

    public void PressedSouth()
    {
        magnit = false;
    }
}
