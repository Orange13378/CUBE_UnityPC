using Leopotam.EcsLite;
using UnityEngine;

namespace CubeECS
{
    public class ItemInteract : MonoBehaviour
    {
        [SerializeField] private Item item;

        private EcsWorld _ecsWorld;
        private EcsFilter _filter;
        private EcsPool<InventoryComponent> _pool;

        private void Start()
        {
            _ecsWorld = EcsWorldManager.GetEcsWorld();
            _filter = _ecsWorld.Filter<InventoryComponent>().End();
            _pool = _ecsWorld.GetPool<InventoryComponent>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Player")) return;

            foreach (var entity in _filter)
            {
                ref var inventoryCmp = ref _pool.Get(entity);

                inventoryCmp.Items.Add(item);
                //inventoryCmp.OnItemChangedUICallback?.Invoke();
                inventoryCmp.OnItemChangedCallback?.Invoke();
            }

            Destroy(gameObject);
        }
    }
}
