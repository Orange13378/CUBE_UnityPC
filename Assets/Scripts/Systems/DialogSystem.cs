using System.Collections;
using Leopotam.EcsLite;
using UnityEngine;
using UnityEngine.UI;

namespace CubeECS
{
    public class DialogSystem : MonoBehaviour
    {
        [SerializeField]
        private GameObject _dialogPanel;
        [SerializeField]
        private float _textSpeed = 0.02f;

        private DialogComponent _dialogComponent;

        private EcsWorld _world;
        private EcsPool<DialogComponent> _dialogPool;
        private EcsPool<DisablePlayerComponent> _disablePlayerPool;
        private EcsFilter _dialogFilter;
        private Text _dialogText;
        private bool _isProcessing;
        private bool _continueDialog;

        public void Construct(EcsWorld world)
        {
            _world = world;
        }

        private void Start()
        {
            _dialogPool = _world.GetPool<DialogComponent>();
            _disablePlayerPool = _world.GetPool<DisablePlayerComponent>();
            _dialogFilter = _world.Filter<DialogComponent>().End();
            _dialogText = _dialogPanel.GetComponentInChildren<Text>();
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                _continueDialog = true;
            }
        }

        public void StartDialog()
        {
            foreach (var entity in _dialogFilter)
            {
                ref var dialogCmp = ref _dialogPool.Get(entity);
                dialogCmp.IsActive = false;
                _dialogComponent = dialogCmp;
            }

            if (_isProcessing)
                return;

            StartCoroutine(StartCoroutine());
        }

        public IEnumerator StartCoroutine()
        {
            var disablePlayer = _world.NewEntity();
            _disablePlayerPool.Add(disablePlayer).Deactivate = true;

            _isProcessing = true;
            _continueDialog = false;

            _dialogPanel.SetActive(true);
            _dialogText.text = string.Empty;

            foreach (char c in _dialogComponent.DialogItem.InputText)
            {
                _dialogText.text += c;
                yield return new WaitForSeconds(_textSpeed);
            }

            yield return new WaitUntil(() => _continueDialog);

            var activatePlayer = _world.NewEntity();
            _disablePlayerPool.Add(activatePlayer).Deactivate = false;

            _dialogPanel.SetActive(false);
            _isProcessing = false;
        }
    }
}