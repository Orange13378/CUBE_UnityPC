using System.Linq;
using UnityEngine;

namespace CubeMVC
{
    public class ChestOpenController : MonoBehaviour
    {
        [SerializeField]
        private ContextProvider _contextProvider;

        private InventoryModel _inventoryModel;
        private ChestModel _chestModel;
        private DialogModel _dialogModel;

        public void Start()
        {
            _inventoryModel = _contextProvider.GetContext().InventoryModel;
            _chestModel = _contextProvider.GetContext().ChestModel;
            _dialogModel = _contextProvider.GetContext().DialogModel;
        }

        public void Update()
        {
            var chest = _chestModel.Items.FirstOrDefault(x => x.IsOpened == false);

            if (chest != null)
            {
                var item = _inventoryModel.Items.FirstOrDefault(x => x.id == chest.KeyId);

                if (item != null)
                {
                    chest.IsOpened = true;
                    _inventoryModel.Items.Remove(item);
                    _inventoryModel.OnItemChangedUICallback?.Invoke();
                }

                StartDialog(chest);

                _chestModel.Items.Remove(chest);
            }
        }

        private void StartDialog(ChestItem chest)
        {
            if (chest.IsOpened)
            {
                chest.IsUsed = true;

                var chestObject = _chestModel.Chests.First(x => x.GetComponent<ChestInteract>().chestItem.Id == chest.Id);
                chestObject.GetComponent<SpriteRenderer>().sprite = chest.Sprite;

                _dialogModel.OnDialogStart(chest.Success);

                _chestModel.CurrentOpenedItem.SetActive(true);
            }
            else
            {
                _dialogModel.OnDialogStart(chest.Bad);
            }
        }
    }
}