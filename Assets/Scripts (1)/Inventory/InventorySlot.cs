using CubeMVC;
using UnityEngine;
using UnityEngine.UI;

public class InventorySlot : MonoBehaviour {

	public Image icon;

	private Item _item;

	private ContextProvider _contextProvider;
	private DialogModel _dialogModel;

    private void Start()
    {
        _contextProvider = FindObjectOfType<ContextProvider>();
        _dialogModel = _contextProvider.GetContext().DialogModel;
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
            _dialogModel.OnDialogStart?.Invoke(_item.text);
        }
    }
}
