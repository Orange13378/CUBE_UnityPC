using UnityEngine;

namespace CubeMVC
{
    public class ItemInteract : MonoBehaviour
    {
        [SerializeField] 
        private Item item;

        [SerializeField]
        private ContextProvider _contextProvider;

        private InventoryModel _inventoryModel;

        private void Start()
        {
            _inventoryModel = _contextProvider.GetContext().InventoryModel;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Player")) return;

            _inventoryModel.Items.Add(item);
            _inventoryModel.OnItemChangedCallback?.Invoke();

            Destroy(gameObject);
        }
    }
}
