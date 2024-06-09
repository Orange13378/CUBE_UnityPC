using CubeMVC;
using UnityEngine;

public class ItemPickup : MonoBehaviour
{

    public Item item;   // Item to put in the inventory if picked up

    public static bool electro, time, termo, magnit;

    [SerializeField] private GameObject objectUI;

    private ContextProvider _contextProvider;
    private DialogModel _dialogModel;

    private void Start()
    {
        _contextProvider = FindFirstObjectByType<ContextProvider>();
        _dialogModel = _contextProvider.GetContext().DialogModel;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PickUp();
    }

    private void PickUp()
    {
        if (item.id == 0)
        {
            electro = true;

            _dialogModel.OnDialogStart("Похоже на лампочку " +
                                       "<Открылась возможность использовать предмет в отдельном меню>");


            objectUI.SetActive(true);
            gameObject.SetActive(false);
        }

        else if (item.id == 1)
        {
            termo = true;

            _dialogModel.OnDialogStart("Похоже на термометр " +
                                       "<Открылась возможность использовать термометр>");

            objectUI.SetActive(true);
            gameObject.SetActive(false);
        }

        else if (item.id == 2)
        {
            magnit = true;

            _dialogModel.OnDialogStart("Это магнит " +
                                       "<Открылась возможность использовать магнит>");

            objectUI.SetActive(true);
            gameObject.SetActive(false);
        }

        else if (item.id == 3)
        {
            time = true;

            _dialogModel.OnDialogStart("Странные часы " +
                                       "<Открылась возможность использовать часы>");

            objectUI.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
