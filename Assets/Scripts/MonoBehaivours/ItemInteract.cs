using Leopotam.EcsLite;
using UnityEngine;

namespace CubeECS
{
    public class ItemInteract : MonoBehaviour
    {
        [SerializeField] private Item item;

        private EcsWorld _ecsWorld;

        private void Start()
        {
            _ecsWorld = EcsWorldManager.GetEcsWorld();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                var filter = _ecsWorld.Filter<InventoryComponent>().End();
                var inventoryPool = _ecsWorld.GetPool<InventoryComponent>();

                foreach (var entity in filter)
                {
                    ref var inventoryComponent = ref inventoryPool.Get(entity);

                    inventoryComponent.Items.Add(item); 
                    if (inventoryComponent.OnItemChangedCallback != null)
                        inventoryComponent.OnItemChangedCallback.Invoke();
                }

                Destroy(gameObject);
            }
        }
    }
}
