using CubeECS;
using Leopotam.EcsLite;
using UnityEngine;

public class StoneScript : MonoBehaviour
{
    public GameObject diaolog;

    #region Singleton

	public static StoneScript instance;

	void Awake ()
	{
		instance = this;
	}

    #endregion

    private EcsFilter _dialogFilter;
    private EcsPool<DialogComponent> _dialogPool;

    private void Start()
    {
        var ecsWorld = EcsWorldManager.GetEcsWorld();
        _dialogFilter = ecsWorld.Filter<DialogComponent>().End();
        _dialogPool = ecsWorld.GetPool<DialogComponent>();
    }

    public void GetText(Stones stoneID)
    {
        foreach (var entity in _dialogFilter)
        {
            ref var dialogComponent = ref _dialogPool.Get(entity);
            dialogComponent.InputText = stoneID.text;
            dialogComponent.DialogSystem.StartDialog();
        }
    }

    public void GetElectroText(Stones stoneID)
    {
        foreach (var entity in _dialogFilter)
        {
            ref var dialogComponent = ref _dialogPool.Get(entity);
            dialogComponent.InputText = stoneID.electroText;
            dialogComponent.DialogSystem.StartDialog();
        }
    }
}
