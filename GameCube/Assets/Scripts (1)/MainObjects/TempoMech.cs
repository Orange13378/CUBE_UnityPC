using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TempoMech : MonoBehaviour
{
            //if(TempoMech.temp == TempoMech.Temp.Zero) Debug.Log("Температура нормальная");
            //if(TempoMech.temp == TempoMech.Temp.Plus) Debug.Log("Температура высокая");
            //if(TempoMech.temp == TempoMech.Temp.Minus) Debug.Log("Температура низкая");
    #region Singleton

	public static TempoMech instance;

	void Awake ()
	{
		instance = this;
	}

	#endregion
    
    [System.NonSerializedAttribute] public static bool plus, minus, zero, switched;
    

    void Update()
    {
        
    }

    public void PressedPlus()
    {
        plus = true;
        minus = false;
    }

    public void PressedMinus()
    {
        
        plus = false;
        minus = true;

    }

    public void PressedZero()
    {
        
        plus = false;
        minus = false;

    }
}
