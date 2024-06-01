using System.Collections;
using UnityEngine;
using DG.Tweening;

public class MoveTilesWhite : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private GameObject block;

    [SerializeField] Vector3 nextPos = new();

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
            StartCoroutine(Moving());
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
        gameObject.transform.DOLocalMove(nextPos, 5f, false);
        yield return new WaitForSeconds(0.1f);
        yield return new WaitForSeconds(5f);
        block.gameObject.SetActive(false);
    }
}
