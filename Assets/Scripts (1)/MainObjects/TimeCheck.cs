using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeCheck : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject wall;

    private bool future = false, past = false, now = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(TimeMech.time == TimeMech.Time.Future && !future)
        {
            future = true;
            past = false;
            now = false;
            
        }

        if(TimeMech.time == TimeMech.Time.Now && !now)
        {
            future = false;
            past = false;
            now = true;
        }

        if(TimeMech.time == TimeMech.Time.Past && !past)
        {
            future = true;
            past = true;
            now = false;
            wall.SetActive(false);
        }
    }
}
