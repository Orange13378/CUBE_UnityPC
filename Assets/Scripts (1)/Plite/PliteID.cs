using UnityEngine;

[CreateAssetMenu(fileName = "New Plite", menuName = "PliteMenu/Plite")]
public class PliteID : ScriptableObject
{
	new public string name = "New Plite";	// Name of the plite
	public Sprite icon = null, newIcon = null, electroIcon = null;  // Item icon
	public int id = 0;
	[SerializeField]
	public int counts = 0;
	[SerializeField]
	public int electroCounts = 0;


	public void OnEnable(){
		PliteID[] instances = Resources.FindObjectsOfTypeAll<PliteID>();
		for(int i = 0; i < instances.Length; i++)
		{
			instances[i].counts = 0;
			instances[i].electroCounts = 0;
		}
	}
}
