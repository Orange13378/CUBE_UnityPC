using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class CodeLock : MonoBehaviour
{
	public GameObject stone;

	[Header("General")]
	
	public bool unlock;
	public InputField _InputField;
	public string password = "569";

	public string doorCode = "274";

	public GameObject[] objectsOn;
	public GameObject[] objectsOff;
	[Header("Messages")]
	public string error = "Не верный код";
	public Color errorColor = Color.red;
	public string success = "Введен правильный код";
	public Color successColor = Color.green;
	public string defaultText = "Введите код";
	public Color defaultColor = Color.black;
	[Header("Buttons")]
	public float offset = 10;
	public RectTransform button;
	public RectTransform panel;
	public bool buildButtons;
	public RectTransform[] allButtons;

	void Start()
	{
		unlock = false;
		_InputField.interactable = false;
		_InputField.characterLimit = password.Length;
		ResetPass();
		if (buildButtons) BuildGrid(); else SetButton();
	}

    private void Update()
    {
        if (unlock)
        {
			stone.SetActive(true);
        }
    }

    void SetButton()
	{
		int i = 1;
		for (int j = 0; j < allButtons.Length; j++)
		{
			switch (i)
			{
				case 10:
					allButtons[j].GetComponentInChildren<Text>().text = "R";
					allButtons[j].GetComponent<Button>().onClick.AddListener(() => { ResetPass(); });
					break;
				case 11:
					allButtons[j].GetComponentInChildren<Text>().text = "0";
					allButtons[j].GetComponent<Button>().onClick.AddListener(() => { AddKeyPass("0"); });
					break;
				case 12:
					allButtons[j].GetComponentInChildren<Text>().text = "E";
					allButtons[j].GetComponent<Button>().onClick.AddListener(() => { EnterPass(); });
					break;
				default:
					string number = i.ToString();
					allButtons[j].GetComponentInChildren<Text>().text = number;
					allButtons[j].GetComponent<Button>().onClick.AddListener(() => { AddKeyPass(number); });
					break;
			}
			i++;
		}
	}

	void BuildGrid()
	{
		float sizeX = button.sizeDelta.x + offset;
		float sizeY = button.sizeDelta.y + offset;
		float posX = -sizeX * 3 / 2 - sizeX / 2;
		float posY = Mathf.Abs(posX) - sizeY / 2;
		float Xreset = posX;
		int i = 0;
		allButtons = new RectTransform[12];
		for (int y = 0; y < 4; y++)
		{
			posY -= sizeY;
			for (int x = 0; x < 3; x++)
			{
				posX += sizeX;
				allButtons[i] = Instantiate(button) as RectTransform;
				allButtons[i].SetParent(panel);
				allButtons[i].anchoredPosition = new Vector2(posX, posY);
				allButtons[i].gameObject.name = "Button_ID_" + i;
				i++;
			}
			posX = Xreset;
		}
		SetButton();
		button.gameObject.SetActive(false);
	}

	public void AddKeyPass(string key)
	{
		if (_InputField.text.Length < password.Length)
		{
			_InputField.text += key;
		}
	}

	void ClearText()
	{
		_InputField.text = string.Empty;
	}

	public void EnterPass()
	{
		if (_InputField.text == password)
		{
			unlock = true;
			ClearText();
			_InputField.placeholder.GetComponent<Text>().text = success;
			_InputField.placeholder.GetComponent<Text>().color = successColor;
		}
		else if(_InputField.text == doorCode)
		{
			Door.correct = true;
			ClearText();
			gameObject.SetActive(false);
		}
		else
		{
			ClearText();
			_InputField.placeholder.GetComponent<Text>().text = error;
			_InputField.placeholder.GetComponent<Text>().color = errorColor;
		}
	}

	public void ResetPass()
	{
		ClearText();
		_InputField.placeholder.GetComponent<Text>().text = defaultText;
		_InputField.placeholder.GetComponent<Text>().color = defaultColor;
	}
}