using Leopotam.EcsLite;
using UnityEngine;

namespace CubeECS
{
    public class PedestalInteract : MonoBehaviour
    {
        private EcsWorld _ecsWorld;
        private EcsFilter _filter;
        private EcsPool<PedestalComponent> _pool;

        public void Start()
        {
            _ecsWorld = EcsWorldManager.GetEcsWorld();
            _filter = _ecsWorld.Filter<PedestalComponent>().End();
            _pool = _ecsWorld.GetPool<PedestalComponent>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Player"))
                return;

            foreach (var entity in _filter)
            {
                ref var pedestalCmp = ref _pool.Get(entity);
                pedestalCmp.IsEntered = true;
            }
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (!other.CompareTag("Player"))
                return;

            foreach (var entity in _filter)
            {
                ref var pedestalCmp = ref _pool.Get(entity);
                pedestalCmp.IsEntered = false;
            }
        }
    }
}