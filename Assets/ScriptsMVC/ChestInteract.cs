using System.Linq;
using UnityEngine;

namespace CubeMVC
{
    public class ChestInteract : MonoBehaviour
    {
        [SerializeField] public ChestItem chestItem;
        [SerializeField] public GameObject openedItem;

        private ContextProvider _contextProvider;
        private PlayerInputModel _inputModel;
        private ChestModel _chestModel;
        private bool _isEntered;

        private void Start()
        {
            _contextProvider = FindObjectOfType<ContextProvider>();
            _inputModel = _contextProvider.GetContext().PlayerInputModel;
            _chestModel = _contextProvider.GetContext().ChestModel;

            chestItem.IsOpened = false;
            chestItem.IsUsed = false;
        }

        private void Update()
        {
            if (!_isEntered || !_inputModel.PressedX.Value)
                return;

            if (_chestModel.Items.Contains(chestItem) ||
                _chestModel.Items.FirstOrDefault(x => x.Id == chestItem.Id && x.IsUsed) != null)
                return;

            _chestModel.Items.Add(chestItem);
            _chestModel.CurrentOpenedItem = openedItem;
            _isEntered = false;
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
