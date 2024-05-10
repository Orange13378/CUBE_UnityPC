using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoveBlocks : MonoBehaviour
{
    [SerializeField] Vector3 nextPos = new Vector3();

    bool moved, pressedE, entered, once;
    Vector3 startPos;
    [SerializeField] private GameObject player, underPlite1, underPlite2;
    [SerializeField] private Sprite sprite_goPlite, sprite_backPlite, spriteN , spriteS;

    public static bool sprite = false;
    void Start()
    {
        startPos = gameObject.transform.position;
        moved = true;
        pressedE = false;
        entered = false;
        once = true;
        //waited = true;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && entered && once)
        {
            StartCoroutine(Plite());
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            entered = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            entered = false;
        }
    }

    IEnumerator Plite()
    {
        once = false;
        pressedE = !pressedE;
        Debug.Log("Нажата E");
        if(moved)
        {
            if (pressedE)
            {
                DisablePlayerScript.off = true;
                gameObject.GetComponent<SpriteRenderer>().sprite = sprite_goPlite;
                gameObject.GetComponent<EdgeCollider2D>().enabled = true;
                gameObject.transform.DOLocalMove(startPos + Vector3.up, 1f, false);
                player.transform.DOLocalMove(startPos + (player.transform.position - gameObject.transform.position) + Vector3.up, 1f, false);
                yield return new WaitForSeconds(1f);
                DisablePlayerScript.on = true;
                once = true;
                sprite = true;
                yield return new WaitUntil((() => MagnitMech.magnit));
                underPlite1.GetComponent<SpriteRenderer>().sprite = spriteN;
                underPlite2.GetComponent<SpriteRenderer>().sprite = spriteS;
                //Debug.Log($"magnit go - {MagnitMech.magnit}");
                once = false;
                if (pressedE)
                {
                    sprite = false;
                    player.transform.DOLocalMove(nextPos + (player.transform.position - gameObject.transform.position) + Vector3.up, 5f, false);
                    gameObject.transform.DOLocalMove(nextPos + Vector3.up, 5f, false);
                    yield return new WaitForSeconds(5f);
                    gameObject.transform.DOLocalMove(nextPos, 1f, false);
                    player.transform.DOLocalMove(nextPos + (player.transform.position - gameObject.transform.position), 1f, false);
                    yield return new WaitForSeconds(1f);
                    gameObject.GetComponent<SpriteRenderer>().sprite = sprite_backPlite;
                    gameObject.GetComponent<EdgeCollider2D>().enabled = false;
                    moved = !moved;
                    pressedE = !pressedE;
                    DisablePlayerScript.on = true;
                }
                //DisablePlayerScript.on = true;
                once = true;
            }
            else 
            {
                player.transform.DOLocalMove(startPos + (player.transform.position - gameObject.transform.position), 1f, false);
                gameObject.transform.DOLocalMove(startPos, 1f, false);
                yield return new WaitForSeconds(1f);
                once = true;
                gameObject.GetComponent<SpriteRenderer>().sprite = sprite_backPlite;
                gameObject.GetComponent<EdgeCollider2D>().enabled = false;
                DisablePlayerScript.on = true;
            }
        }
        else
        {
            if (pressedE)
            {
                sprite = true;
                DisablePlayerScript.off = true;
                gameObject.GetComponent<SpriteRenderer>().sprite = sprite_goPlite;
                gameObject.GetComponent<EdgeCollider2D>().enabled = true;
                gameObject.transform.DOLocalMove(nextPos + Vector3.up, 1f, false);
                player.transform.DOLocalMove(nextPos + (player.transform.position - gameObject.transform.position) + Vector3.up, 1f, false);
                yield return new WaitForSeconds(1f);
                once = true;
                DisablePlayerScript.on = true;
                yield return new WaitUntil((() => !MagnitMech.magnit));
                underPlite1.GetComponent<SpriteRenderer>().sprite = spriteS;
                underPlite2.GetComponent<SpriteRenderer>().sprite = spriteN;
                sprite = false;
                //Debug.Log($"magnit back - {MagnitMech.magnit}");
                once = false;
                if (pressedE)
                {
                    player.transform.DOLocalMove(startPos + (player.transform.position  - gameObject.transform.position + Vector3.up), 5f, false);
                    gameObject.transform.DOLocalMove(startPos + Vector3.up, 5f, false);
                    yield return new WaitForSeconds(5f);
                    gameObject.transform.DOLocalMove(startPos, 1f, false);
                    player.transform.DOLocalMove(startPos + (player.transform.position - gameObject.transform.position), 1f, false);
                    yield return new WaitForSeconds(1f);
                    gameObject.GetComponent<SpriteRenderer>().sprite = sprite_backPlite;
                    gameObject.GetComponent<EdgeCollider2D>().enabled = false;
                    moved = !moved;
                    pressedE = !pressedE;
                    DisablePlayerScript.on = true;
                    
                }
                //DisablePlayerScript.on = true;
                once = true;
            }
            else 
            {
                player.transform.DOLocalMove(nextPos + (player.transform.position - gameObject.transform.position), 1f, false);
                gameObject.transform.DOLocalMove(nextPos, 1f, false);
                yield return new WaitForSeconds(1f);
                once = true;
                gameObject.GetComponent<SpriteRenderer>().sprite = sprite_backPlite;
                gameObject.GetComponent<EdgeCollider2D>().enabled = false;
                DisablePlayerScript.on = true;
            }
        }
        
    }

}
