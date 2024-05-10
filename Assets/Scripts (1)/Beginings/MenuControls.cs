using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControls : MonoBehaviour
{
    public GameObject player; 
    public void StartPressed()
    {
        SceneManager.LoadScene("CubeWorld");
        Time.timeScale = 1;
    }
    public void MenuPressed()
    {
        SceneManager.LoadScene("UIGame");
    }

    public void Back()
    {
        player.gameObject.GetComponent<Player1>().enabled = true;
    }
    public void ExitPressed()
    {
        Application.Quit();
    }
}
