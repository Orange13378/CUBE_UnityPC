using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TimeMech : MonoBehaviour
{
            //if(TimeMech.time == TimeMech.Time.Past) Debug.Log("Перемещаемся в прошлое");
            //if(TimeMech.time == TimeMech.Time.Now) Debug.Log("Перемещаемся в настоящее");
            //if(TimeMech.time == TimeMech.Time.Future) Debug.Log("Перемещаемся в будущее");
    public enum Time
    {
        Past = -1,
        Now = 0,
        Future = 1
    }



    bool check = true;
    [SerializeField] GameObject wall;
    [SerializeField] Sprite pastWall, nowWall;

    public static Time time;
    void Start()
    {
        time = Time.Now;
    }

    void Update()
    {
        if(check && time == Time.Now)
        {
            wall.SetActive(true);
            wall.GetComponent<SpriteRenderer>().sprite = nowWall;
            check = false;

        }

        if(check && time == Time.Past)
        {
            wall.SetActive(true);
            wall.GetComponent<SpriteRenderer>().sprite = pastWall;
            check = false;
        }

        if(check && time == Time.Future)
        {
            wall.SetActive(false);
            check = false;
        }
    }

    public void PressedPast()
    {
        check = true;
        if(time == Time.Future)
        {
            time = Time.Past;
        }
        else if(time == Time.Now)
        {
            time = Time.Past;
        }
    }

    public void PressedNow()
    {
        check = true;
        if(time == Time.Future)
        {
            time = Time.Now;
        }
        else if(time == Time.Past)
        {
            time = Time.Now;
        }
    }

    public void PressedFuture()
    {
        check = true;
        if(time == Time.Now)
        {
            time = Time.Future;
        }
        else if(time == Time.Past)
        {
            time = Time.Future;
        }
    }
}
