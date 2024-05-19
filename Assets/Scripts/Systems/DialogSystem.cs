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

        private bool _isProcessing;
        private bool _continueDialog;
        private Text _dialogText;

        private EcsWorld _world;
        private EcsPool<DialogComponent> _dialogPool;
        private EcsPool<DisablePlayerComponent> _disablePlayerPool;
        private EcsFilter _dialogFilter;
        private DialogComponent _dialogComponent;

        private void Start()
        {
            _world = EcsWorldManager.GetEcsWorld();
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
                yield return new WaitForSeconds(_dialogComponent.TextSpeed);
            }

            yield return new WaitUntil(() => _continueDialog);

            var activatePlayer = _world.NewEntity();
            _disablePlayerPool.Add(activatePlayer).Deactivate = false;

            _dialogPanel.SetActive(false);
            _isProcessing = false;
        }
    }
}