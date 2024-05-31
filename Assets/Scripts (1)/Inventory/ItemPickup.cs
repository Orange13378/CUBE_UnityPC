using CubeECS;
using Leopotam.EcsLite;
using UnityEngine;

public class ItemPickup : MonoBehaviour {

	public Item item;	// Item to put in the inventory if picked up

	public static bool electro, time, termo, magnit;

	[SerializeField] private GameObject objectUI;

    private EcsFilter _dialogFilter;
    private EcsPool<DialogComponent> _dialogPool;

    private void Start()
    {
        var ecsWorld = EcsWorldManager.GetEcsWorld();
        _dialogFilter = ecsWorld.Filter<DialogComponent>().End();
        _dialogPool = ecsWorld.GetPool<DialogComponent>();
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        PickUp();
    }

	// Pick up the item
	void PickUp()
	{
		if(item.id == 0)
		{
			electro = true;

            foreach (var entity in _dialogFilter)
            {
                ref var dialogComponent = ref _dialogPool.Get(entity);
                dialogComponent.DialogItem.InputText = "Похоже на лампочку " +
                                                       "<Открылась возможность использовать предмет в отдельном меню>";
                dialogComponent.DialogSystem.StartDialog();
            }

			objectUI.SetActive(true);
			gameObject.SetActive(false);
		}
		
		else if(item.id == 1)
		{
			termo = true;

            foreach (var entity in _dialogFilter)
            {
                ref var dialogComponent = ref _dialogPool.Get(entity);
                dialogComponent.DialogItem.InputText = "Похоже на термометр " +
                                                       "<Открылась возможность использовать термометр>";
                dialogComponent.DialogSystem.StartDialog();
            }

			objectUI.SetActive(true);
			gameObject.SetActive(false);
		}

		else if(item.id == 2)
		{
			magnit = true;

            foreach (var entity in _dialogFilter)
            {
                ref var dialogComponent = ref _dialogPool.Get(entity);
                dialogComponent.DialogItem.InputText = "Это магнит " +
                                                       "<Открылась возможность использовать магнит>";
                dialogComponent.DialogSystem.StartDialog();
            }

            objectUI.SetActive(true);
			gameObject.SetActive(false);
		}

		else if(item.id == 3)
		{
			time = true;

            foreach (var entity in _dialogFilter)
            {
                ref var dialogComponent = ref _dialogPool.Get(entity);
                dialogComponent.DialogItem.InputText = "Странные часы " +
                                                       "<Открылась возможность использовать часы>";
                dialogComponent.DialogSystem.StartDialog();
            }

            objectUI.SetActive(true);
			gameObject.SetActive(false);
		}
	}

}
