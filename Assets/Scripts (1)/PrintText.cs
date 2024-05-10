using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrintText : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] [TextArea()] string text = null;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            DialogSystem.message.Add(text);
            DialogSystem.on = true;
            gameObject.SetActive(false);

        }
    }
}
