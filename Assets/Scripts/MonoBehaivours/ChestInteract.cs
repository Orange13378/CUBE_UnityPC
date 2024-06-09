using System.Linq;
using Leopotam.EcsLite;
using UnityEngine;

namespace CubeECS
{
    public class ChestInteract : MonoBehaviour
    {
        [SerializeField] public ChestItem chestItem;
        [SerializeField] private GameObject openedItem;

        private EcsFilter _playerInputFilter;
        private EcsFilter _chestFilter;
        private EcsPool<ChestComponent> _chestPool;
        private EcsPool<PlayerInputComponent> _playerInputPool;

        private bool _isEntered;

        private void Start()
        {
            var world = EcsWorldManager.GetEcsWorld();
            _chestFilter = world.Filter<ChestComponent>().End();
            _playerInputFilter = world.Filter<PlayerInputComponent>().End();
            _chestPool = world.GetPool<ChestComponent>();
            _playerInputPool = world.GetPool<PlayerInputComponent>();

            chestItem.IsOpened = false;
            chestItem.IsUsed = false;
        }

        private void Update()
        {
            if (!_isEntered)
                return;

            foreach (var entity in _playerInputFilter)
            {
                ref var playerInputComponent = ref _playerInputPool.Get(entity);
                if (!playerInputComponent.PressedX)
                    return;
            }

            foreach (var entity in _chestFilter)
            {
                ref var chestComponent = ref _chestPool.Get(entity);

                if (chestComponent.Items.FirstOrDefault(x => x.Id == chestItem.Id && x.IsUsed) != null)
                    return;

                if (!chestComponent.Items.Contains(chestItem))
                {
                    chestComponent.Items.Add(chestItem);
                    chestComponent.CurrentOpenedItem = openedItem;
                    _isEntered = false;
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Player"))
                return;

            _isEntered = true;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (!other.CompareTag("Player"))
                return;

            _isEntered = false;
        }
    }
}
