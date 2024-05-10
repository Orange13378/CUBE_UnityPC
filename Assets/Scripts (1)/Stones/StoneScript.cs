using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StoneScript : MonoBehaviour
{
    public GameObject diaolog;

    #region Singleton

	public static StoneScript instance;

	void Awake ()
	{
		instance = this;
	}

	#endregion
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GetText(Stones stoneID)
    {
        DialogSystem.message.Add($"{stoneID.text}");
        var script1 = diaolog.GetComponent<DialogSystem>();
        StartCoroutine(script1.StartDialog());
    }

    public void GetElectroText(Stones stoneID)
    {
        DialogSystem.message.Add($"{stoneID.electroText}");
        var script1 = diaolog.GetComponent<DialogSystem>();
        StartCoroutine(script1.StartDialog());
    }
}
