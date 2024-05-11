using UnityEngine;

public class Player1 : MonoBehaviour
{
    [SerializeField]
    private GameObject EscPanel;
    private PedestalUI _pedestalUI;

    void Awake()
    {
        _pedestalUI = GetComponent<PedestalUI>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            EscPanel.SetActive(true);
            _pedestalUI.Stop();
        }
    }
    
}