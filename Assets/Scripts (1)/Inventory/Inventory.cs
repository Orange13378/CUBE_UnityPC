using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour {

	#region Singleton

	public static Inventory instance;

	void Awake ()
	{
		instance = this;
	}

	#endregion

	public delegate void OnItemChanged();
	public OnItemChanged onItemChangedCallback;

	public int space = 20;	// Amount of item spaces

	// Our current list of items in the inventory
    [SerializeField] Sprite sprite_chest, sprite_chest2, sprite_chest3, sprite_chest4, sprite_chest5, sprite_chest6;

    [SerializeField] private GameObject chest, chest2, chest3, chest4, chest5, chest6, panelDrop, magnit, stoneBlue, stoneOrange, termo, time, objectUI;
    [SerializeField] private Text itemPicked;
    [System.NonSerialized] public bool entered_chest;
    private bool used_chest = false, used_chest2 = false, used_chest3 = false, used_chest4 = false, used_chest5 = false, used_chest6 = false;

    public List<Item> items = new List<Item>();


    // Add a new item if enough room
    public void Add (Item item)
	{
		if (item.showInInventory) {
			if (items.Count >= space) {
				Debug.Log ("Not enough room.");
				return;
			}

			items.Add(item);

            onItemChangedCallback?.Invoke();

            StartCoroutine(ItemPickedUp());
		}
    }

    public void ReloadUI()
    {
		if (onItemChangedCallback != null) onItemChangedCallback.Invoke();
    }

    IEnumerator ItemPickedUp()
    {
        panelDrop.SetActive(true);
        if (items.Count >= 1) 
            itemPicked.text =  $"Предмет <color=purple>{items[^1].name}</color> был подобран";
        yield return new WaitForSeconds(2.5f);
        panelDrop.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (entered_chest)
            {
                if (!used_chest)
                {
                    if(items.Exists(x => x.id == 4))
                    {
                        used_chest = true;
                        chest.GetComponent<SpriteRenderer>().sprite = sprite_chest;
                        items.Remove(items.Find(x => x.id == 4));
                        if (onItemChangedCallback != null)
                            onItemChangedCallback.Invoke();

                        Debug.Log("Что это выпало из сундука?");

                        stoneBlue.SetActive(true);
                        return;
                    }
                    else
                    {
                        Debug.Log("Что бы открыть сундук нужен белый ключ");
                        
                        return;
                    }
                }

                if (!used_chest3)
                {
                    if(items.Exists(x => x.id == 6))
                    {
                        used_chest3 = true;
                        chest3.GetComponent<SpriteRenderer>().sprite = sprite_chest3;
                        items.Remove(items.Find(x => x.id == 6));
                        if (onItemChangedCallback != null)
                            onItemChangedCallback.Invoke();

                        termo.SetActive(true);
                        objectUI.SetActive(true);
                        return;
                    }
                    else
                    {
                        Debug.Log("Что бы открыть сундук нужен оранжевый ключ");
                        return;
                    }
                }

                if (!used_chest2)
                {
                    if(items.Exists(x => x.id == 5))
                    {
                        used_chest2 = true;
                        chest2.GetComponent<SpriteRenderer>().sprite = sprite_chest2;
                        items.Remove(items.Find(x => x.id == 5));
                        if (onItemChangedCallback != null)
                            onItemChangedCallback.Invoke();

                        Debug.Log("Еще один куб");

                        stoneOrange.SetActive(true);
                        return;
                    }
                    else
                    {
                        Debug.Log("Что бы открыть сундук нужен синий ключ");
                        return;
                    }
                }

                if (!used_chest4)
                {
                    if(items.Exists(x => x.id == 7))
                    {
                        used_chest4 = true;
                        chest4.GetComponent<SpriteRenderer>().sprite = sprite_chest4;
                        items.Remove(items.Find(x => x.id == 7));
                        if (onItemChangedCallback != null)
                            onItemChangedCallback.Invoke();

                        magnit.SetActive(true);
                        //objectUI.SetActive(true);
                        return;
                    }
                    else
                    {
                        Debug.Log("Что бы открыть сундук нужен зеленый ключ");

                        return;
                    }
                }

                if (!used_chest5)
                {

                    if(items.Exists(x => x.id == 8))
                    {
                        used_chest5 = true;
                        chest5.GetComponent<SpriteRenderer>().sprite = sprite_chest5;
                        items.Remove(items.Find(x => x.id == 8));
                        if (onItemChangedCallback != null)
                            onItemChangedCallback.Invoke();

                        time.SetActive(true);
                        //objectUI.SetActive(true);
                        return;
                    }
                    else
                    {
                        Debug.Log("Что бы открыть сундук нужен фиолетовый ключ");
                        return;
                    }
                }
                
                if (!used_chest6)
                {

                    if(items.Exists(x => x.id == 9))
                    {
                        used_chest6 = true;
                        chest6.GetComponent<SpriteRenderer>().sprite = sprite_chest6;
                        items.Remove(items.Find(x => x.id == 9));
                        if (onItemChangedCallback != null)
                            onItemChangedCallback.Invoke();

                        
                        //objectUI.SetActive(true);
                        return;
                    }
                    else
                    {
                        Debug.Log("Что бы открыть сундук нужен черный ключ");

                        return;
                    }
                }
                entered_chest = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Chest"))
        {
            //entered_chest = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Chest")
        {
            entered_chest = false;
        }
    }

}