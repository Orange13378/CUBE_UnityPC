using CubeECS;
using Leopotam.EcsLite;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour {

	public Image icon;

	private Item _item;

	private EcsFilter _dialogFilter;
	private EcsPool<DialogComponent> _dialogPool;

    private void Start()
    {
        var ecsWorld = EcsWorldManager.GetEcsWorld();
        _dialogFilter = ecsWorld.Filter<DialogComponent>().End();
        _dialogPool = ecsWorld.GetPool<DialogComponent>();
    }

    public void AddItem(Item newItem)
	{
		_item = newItem;

		icon.sprite = _item.icon;
		icon.enabled = true;
	}

	public void ClearSlot()
	{
		_item = null;

		icon.sprite = null;
		icon.enabled = false;
	}

	public void UseItem()
	{
		if (_item != null)
		{
            foreach (var entity in _dialogFilter)
            {
                ref var dialogComponent = ref _dialogPool.Get(entity);
                dialogComponent.InputText = _item.text;
                dialogComponent.DialogBehavior.StartDialog();
            }
        }
	}
}
