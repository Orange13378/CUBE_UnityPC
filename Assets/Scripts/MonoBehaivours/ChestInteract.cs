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
        private EcsWorld _world;


        private void Start()
        {
            _world = EcsWorldManager.GetEcsWorld();
            _filter = _world.Filter<ChestComponent>().End();
            _chestPool = _world.GetPool<ChestComponent>();

            chestItem.IsOpened = false;
            chestItem.IsUsed = false;
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
    }
}
