using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]

public class CollectableItem : MonoBehaviour
{
    [SerializeField] private Item item;
    //[SerializeField] private int amount = 1;

    private AudioSource audioSource;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!item) return;

        var inventory = other.GetComponent<Inventory>();

        if (inventory)
        {
            if (inventory.Collected(item))
            {
                if (item.id == 0)
                    inventory.keyPickUp = true;
                    
                if (item.id == 1)
                    
                Destroy(gameObject);
            }
        }
    }
}
