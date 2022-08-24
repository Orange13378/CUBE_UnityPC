using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class MoveTilesWhite1 : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject block;

    [SerializeField] Vector3 nextPos = new Vector3();

    bool stoped, stoped1;
    void Start()
    {
        stoped = false;
        stoped1 = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (PliteScripts.correctCode && !stoped)
        {
            //StartCoroutine(Moving());
            stoped = true;
        }

        if (PliteScripts.correctElectroCode && !stoped1)
        {
            StartCoroutine(Moving());
            stoped1 = true;
        }


    }

    IEnumerator Moving()
    {
        DisablePlayerScript.off = true;
        gameObject.transform.DOLocalMove(nextPos, 5f, false);
        yield return new WaitForSeconds(0.1f);
        DisablePlayerScript.antiMouse = true;
        yield return new WaitForSeconds(5f);
        block.gameObject.SetActive(false);
        DisablePlayerScript.antiMouse = false;
        DisablePlayerScript.on = true;
    }
}
