using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public Button LeveL2;
    public Button LeveL3;

    int levelComplete;

    void Start()
    {
        levelComplete = PlayerPrefs.GetInt("LevelComplete");
        LeveL2.interactable = false;
        LeveL3.interactable = false;
        switch (levelComplete)
        {
            case 1:
                LeveL2.interactable = true;
                break;
            case 2:
                LeveL2.interactable = true;
                LeveL3.interactable = true;
                break;
        }
    }
    public void LoadTo(int level)
    {
        SceneManager.LoadScene(level);
    }
    public void Reset()
    {
        LeveL2.interactable = false;
        LeveL3.interactable = false;
        PlayerPrefs.DeleteAll();
    }
}
