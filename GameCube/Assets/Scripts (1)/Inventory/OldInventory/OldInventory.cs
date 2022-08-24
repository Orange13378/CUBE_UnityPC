using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

[System.Serializable]

public class OldInventory : MonoBehaviour
{
    public class OldInventorySlot
    {
        public Item item;
        public int amount;

        public OldInventorySlot(Item item, int amount = 1)
        {
            this.item = item;
            this.amount = amount;
        }
    }

    [SerializeField] private List<OldInventorySlot> items = new List<OldInventorySlot>();
    //[SerializeField] private int size = 4;

    [SerializeField] public UnityEvent OnInventoryChange;
    private bool entered_chest = false, entered_portal = false, entered_ark = false;
    [System.NonSerialized]public bool del = false, used_chest = false, used_portal = false;
    [SerializeField] public Sprite sprite_chest, sprite_ark;
    [SerializeField] public GameObject cloud_chest, cloud_portal;
    [SerializeField] public GameObject newCloud_chest, newCloud_portal;
    [SerializeField] public GameObject chest, portal;
    [SerializeField] public GameObject stone, ark_portal;
    [System.NonSerialized] public bool keyPickUp = false, stonePickUp = false;
    
    public bool AddItems(Item item, int amount = 1)
    {
        foreach (OldInventorySlot slot in items)
        {
            if (slot.item.id == item.id)
            {
                slot.amount += amount;
                OnInventoryChange.Invoke();
                return true;
            }
        }

        OldInventorySlot new_slot = new OldInventorySlot(item, amount);

        items.Add(new_slot);
        OnInventoryChange.Invoke();
        return false;
    }

    public Item GetItem(int i)
    {
        return i < items.Count ? items[i].item : null;
    }

    public int GetAmount(int i)
    {
        return i < items.Count ? items[i].amount : 0;
    }

    public int GetSize()
    {
        return items.Count;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (entered_chest)
            {
                if (!used_chest)
                {
                    if (items.Exists(x => x.item.id == 0))
                    {
                        used_chest = !used_chest;
                        chest.GetComponent<SpriteRenderer>().sprite = sprite_chest;
                        stone.SetActive(true);
                        cloud_portal.SetActive(true);
                        del = true;
                        OnInventoryChange.Invoke();
                        items.Remove(items.Find(x => x.item.id == 0));
                        OnInventoryChange.Invoke();
                        cloud_chest.SetActive(false);
                    }
                    else
                    {
                        newCloud_chest.SetActive(true);
                        cloud_chest.SetActive(false);
                    }
                }
            }
            if (entered_portal)
            {
                if (!used_portal)
                {
                    if (items.Exists(x => x.item.id == 1))
                    {
                        used_portal = !used_portal;
                        ark_portal.GetComponent<SpriteRenderer>().sprite = sprite_ark;
                        del = true;
                        portal.SetActive(true);
                        OnInventoryChange.Invoke();
                        items.Remove(items.Find(x => x.item.id == 1));
                        OnInventoryChange.Invoke();
                        cloud_portal.SetActive(false);
                    }
                    else
                    {
                        newCloud_portal.SetActive(true);
                        cloud_portal.SetActive(false);
                    }
                }
            }
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (entered_ark)
            {
                SceneManager.LoadScene("End");
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Chest")
        {
            entered_chest = true;
            if (!used_chest) 
                cloud_chest.SetActive(true);
            else 
                cloud_chest.SetActive(false);
        }

        if (other.tag == "Portal")
        {
            entered_portal = true;
            if (!used_portal)
                cloud_portal.SetActive(true);
            else
                cloud_portal.SetActive(false);
        }

        if (other.tag == "Finish")
        {
            entered_ark = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Chest")
        {
            entered_chest = false;
            newCloud_chest.SetActive(false);
            cloud_chest.SetActive(false);
        }

        if (other.tag == "Portal")
        {
            entered_portal = false;
            if (!used_portal)
                cloud_portal.SetActive(true);
            else
                cloud_portal.SetActive(false);
        }

        if (other.tag == "Finish")
        {
            entered_ark = false;
        }
    }
}
