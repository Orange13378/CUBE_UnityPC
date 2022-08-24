using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[CreateAssetMenu(fileName = "New Stone", menuName = "StoneMenu/Stone")]
public class Stones : ScriptableObject
{
    new public string name = "Stone";	// Name of the plite
	public Sprite icon = null, newIcon = null;  // Item icon
	//public int id = 0;

    [TextArea()] public string text, electroText;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
