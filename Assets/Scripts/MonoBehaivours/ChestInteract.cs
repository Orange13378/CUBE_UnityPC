using System.Linq;
using Leopotam.EcsLite;
using UnityEngine;

namespace CubeECS
{
    public class ChestInteract : MonoBehaviour
    {
        [SerializeField] public ChestItem chestItem;

        private EcsFilter _filter;
        private EcsPool<ChestComponent> _chestPool;
        private bool _isEntered;

        void Start()
        {
            var ecsWorld = EcsWorldManager.GetEcsWorld();
            _filter = ecsWorld.Filter<ChestComponent>().End();
            _chestPool = ecsWorld.GetPool<ChestComponent>();

            chestItem.IsOpened = false;
            chestItem.IsUsed = false;
            chestItem.IsClosed = false;
        }

        private void OnTriggerStay2D(Collider2D other)
        {
            if (other.CompareTag("Player") && Input.GetKey(KeyCode.E))
            {
                foreach (var entity in _filter)
                {
                    ref var chestComponent = ref _chestPool.Get(entity);

                    if (chestComponent.Items.FirstOrDefault(x => x.Id == chestItem.Id && x.IsUsed) != null)
                        return;

                    if (!chestComponent.Items.Contains(chestItem))
                        chestComponent.Items.Add(chestItem);
                }
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (other.CompareTag("Player"))
            {
                foreach (var entity in _filter)
                {
                    ref var chestComponent = ref _chestPool.Get(entity);

                    var chest = chestComponent.Items.FirstOrDefault(x => x.IsClosed);

                    if (chest != null)
                        chest.IsClosed = false;
                }
            }
        }
    }
}
