using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogSystem : MonoBehaviour
{
    [SerializeField] private GameObject panelDialog;
    [SerializeField] private Sprite sprite;
    [SerializeField] private float textSpeed;
    [SerializeField] private Text dialog;
    public static List<string> message = new List<string>();

    private bool continue_dialog = false;

    [System.NonSerialized] public static bool printText, started, on, dialogSkiped = false;

    void Start()
    {
        //imageDialog.GetComponent<SpriteRenderer>().sprite = sprite;
        //message.Add("Что произошло? Как я сюда попал? Последнее что я помню это яркую вспышку.");
        started = false;
        dialogSkiped = false;
        panelDialog.SetActive(false);
    }

    void Update()
    {
        if ((Input.GetMouseButtonDown(0)))
        {
            continue_dialog = true;
        }

        if (printText && !started) 
        {
            printText = false;
            StartCoroutine(StartDialog());
        }

        if(on)
        {
            on = false;
            StartCoroutine(StartDialog());
        }
    }

    public IEnumerator StartDialog()
    {
        DisablePlayerScript.off = true;
            started = true;

            panelDialog.SetActive(true);
            for(int i = 0; i < message.Count; i++)
            {
                dialog.text = string.Empty;
                continue_dialog = false;

                foreach(char c in message[i])
                {
                    dialog.text += c;
                    yield return new WaitForSeconds(textSpeed);
                }
                yield return new WaitUntil((() => continue_dialog));
            }
            message.Clear();
            panelDialog.SetActive(false);
    }

    public void St()
    {
        Item.same.Clear();
        Item.same.Add(-1);
        started = false;
    }
}