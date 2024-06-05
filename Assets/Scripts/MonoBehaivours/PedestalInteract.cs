using Leopotam.EcsLite;
using UnityEngine;

namespace CubeMVC
{
    public class PedestalInteract : MonoBehaviour
    {
        [SerializeField]
        private ContextProvider _contextProvider;

        private PedestalModel _pedestalModel;

        public void Start()
        {
            _pedestalModel = _contextProvider.GetContext().PedestalModel;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("Player"))
                return;

            _pedestalModel.IsEntered = true;
        }

        private void OnTriggerExit2D(Collider2D other)
        {
            if (!other.CompareTag("Player"))
                return;

            _pedestalModel.IsEntered = false;
        }
    }
}