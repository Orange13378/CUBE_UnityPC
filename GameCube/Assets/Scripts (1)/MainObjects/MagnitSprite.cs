using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnitSprite : MonoBehaviour
{
    // Start is called before the first frame update
    public Sprite spriteN, spriteS;
    public GameObject plite;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (MagnitMech.magnit && !MoveBlocks.sprite)
        {
            plite.GetComponent<SpriteRenderer>().sprite = spriteS;
        }

        if (!MagnitMech.magnit)
        {
            plite.GetComponent<SpriteRenderer>().sprite = spriteN;
        }
    }
}
