using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

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
    [System.NonSerialized] public bool keyPickUp = false, entered_chest = false;
    private bool used_chest = false, used_chest2 = false, used_chest3 = false, used_chest4 = false, used_chest5 = false, used_chest6 = false;



    public List<Item> items = new List<Item>();
    [SerializeField] public UnityEvent OnInventoryChange;

    //player.gameObject.GetComponent<Player1>().enabled = true;


    // Add a new item if enough room
    public void Add (Item item)
	{
		if (item.showInInventory) {
			if (items.Count >= space) {
				Debug.Log ("Not enough room.");
				return;
			}

			items.Add(item);

            if (onItemChangedCallback != null)
			onItemChangedCallback.Invoke();
            
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
        itemPicked.text =  $"Предмет <color=purple>{items[items.Count - 1].name}</color> был подобран";
        yield return new WaitForSeconds(2.5f);
        panelDrop.SetActive(false);
    }

    public bool Collected(Item item)
    {
        foreach (Item slot in items)
        {
            if (slot.id == item.id)
            {
                //OnInventoryChange.Invoke();
                return true;
            }
        }
        return false;
    }

    // Remove an item
    public void Remove (Item item)
	{
		items.Remove(item);

		if (onItemChangedCallback != null)
			onItemChangedCallback.Invoke();
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

                        if (DialogSystem.message.Count == 0)
                        {
                            DialogSystem.message.Add("Что это выпало из сундука?");
                            DialogSystem.on = true;
                        }
                        stoneBlue.SetActive(true);
                        return;
                    }
                    else
                    {
                        if (DialogSystem.message.Count == 0)
                        {
                            DialogSystem.message.Add("Что бы открыть сундук нужен белый ключ");
                            DialogSystem.on = true;
                        }
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
                        if (DialogSystem.message.Count == 0)
                        {
                            DialogSystem.message.Add("Что бы открыть сундук нужен оранжевый ключ");
                            DialogSystem.on = true;
                        }
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

                        if (DialogSystem.message.Count == 0)
                        {
                            DialogSystem.message.Add("Еще один куб");
                            DialogSystem.on = true;
                        }
                        stoneOrange.SetActive(true);
                        return;
                    }
                    else
                    {
                        if (DialogSystem.message.Count == 0)
                        {
                            DialogSystem.message.Add("Что бы открыть сундук нужен синий ключ");
                            DialogSystem.on = true;
                        }
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
                        if (DialogSystem.message.Count == 0)
                        {
                            DialogSystem.message.Add("Что бы открыть сундук нужен зеленый ключ");
                            DialogSystem.on = true;
                        }
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
                        if (DialogSystem.message.Count == 0)
                        {
                            DialogSystem.message.Add("Что бы открыть сундук нужен фиолетовый ключ");
                            DialogSystem.on = true;
                        }
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
                        if (DialogSystem.message.Count == 0)
                        {
                            DialogSystem.message.Add("Что бы открыть сундук нужен черный ключ");
                            DialogSystem.on = true;
                        }
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
            entered_chest = true;
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
