using CubeECS;
using Leopotam.EcsLite;
using UnityEngine;

public class PrintText : MonoBehaviour
{
    [SerializeField] 
    [TextArea] string text = null;

    private EcsFilter _dialogFilter;
    private EcsPool<DialogComponent> _dialogPool;

    private void Start()
    {
        var ecsWorld = EcsWorldManager.GetEcsWorld();
        _dialogFilter = ecsWorld.Filter<DialogComponent>().End();
        _dialogPool = ecsWorld.GetPool<DialogComponent>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player")) 
            return;

        foreach (var entity in _dialogFilter)
        {
            ref var dialogComponent = ref _dialogPool.Get(entity);
            dialogComponent.DialogItem.InputText = text;
            dialogComponent.DialogSystem.StartDialog();
        }

        gameObject.SetActive(false);
    }
}
