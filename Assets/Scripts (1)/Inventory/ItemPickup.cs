using UnityEngine;

public class ItemPickup : MonoBehaviour {

	public Item item;	// Item to put in the inventory if picked up

	public static bool electro, time, termo, magnit;

	[SerializeField] private GameObject objectUI;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        //PickUp();
    }

	// Pick up the item
	void PickUp()
	{
		if(item.id == 0)
		{
			electro = true;
			DialogSystem.message.Add("Похоже на лампочку");
            DialogSystem.message.Add("<Открылась возможность использовать предмет в отдельном меню>");
			DialogSystem.on = true;
			objectUI.SetActive(true);
			gameObject.SetActive(false);
		}
		
		else if(item.id == 1)
		{
			termo = true;
			DialogSystem.message.Add("Похоже на термометр");
            DialogSystem.message.Add("<Открылась возможность использовать термометр>");
			DialogSystem.on = true;
			objectUI.SetActive(true);
			gameObject.SetActive(false);
		}

		else if(item.id == 2)
		{
			magnit = true;
			DialogSystem.message.Add("Это магнит");
			DialogSystem.message.Add("<Открылась возможность использовать магнит>");
			DialogSystem.on = true;
			objectUI.SetActive(true);
			gameObject.SetActive(false);
		}

		else if(item.id == 3)
		{
			time = true;
			DialogSystem.message.Add("Странные часы");
			DialogSystem.message.Add("<Открылась возможность использовать часы>");
			DialogSystem.on = true;
			objectUI.SetActive(true);
			gameObject.SetActive(false);
		}
		else 
		{
			Inventory.instance.Add(item);	// Add to inventory
			Destroy(gameObject);			// Destroy item from scene
		}
	}

}
