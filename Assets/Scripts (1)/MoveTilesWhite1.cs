using System.Collections;
using UnityEngine;
using DG.Tweening;

public class MoveTilesWhite1 : MonoBehaviour
{
    [SerializeField] private GameObject block;

    [SerializeField] Vector3 nextPos = new ();

    private bool stoped, stoped1;
    private void Start()
    {
        stoped = false;
        stoped1 = false;
    }

    private void Update()
    {
        if (PliteScripts.correctCode && !stoped)
        {
            stoped = true;
        }

        if (PliteScripts.correctElectroCode && !stoped1)
        {
            StartCoroutine(Moving());
            stoped1 = true;
        }
    }

    private IEnumerator Moving()
    {
        gameObject.transform.DOLocalMove(nextPos, 5f, false);
        yield return new WaitForSeconds(0.1f);
        yield return new WaitForSeconds(5f);
        block.gameObject.SetActive(false);
    }
}
