using CubeMVC;
using UnityEngine;

public class StoneScript : MonoBehaviour
{
    #region Singleton

	public static StoneScript instance;

	void Awake ()
	{
		instance = this;
	}

    #endregion

    private DialogModel _dialogModel;

    [SerializeField]
    private ContextProvider _contextProvider;

    private void Start()
    {
        _dialogModel = _contextProvider.GetContext().DialogModel;
    }

    public void GetText(Stones stoneID)
    {
        _dialogModel.OnDialogStart(stoneID.text);
    }

    public void GetElectroText(Stones stoneID)
    {
        _dialogModel.OnDialogStart(stoneID.electroText);
    }
}
