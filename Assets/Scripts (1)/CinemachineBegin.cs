using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CinemachineBegin : MonoBehaviour
{

    [SerializeField] Vector3 nextPos = new Vector3();
    [SerializeField] GameObject cameraControl;
    public static bool touch;
    void Start()
    {
        touch = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Begin()
    {
        StartCoroutine(Walk());
    }

    IEnumerator Walk()
    {
        yield return new WaitUntil((() => CameraZoom.zoomed));
        yield return new WaitForSeconds(1f);
        gameObject.transform.DOLocalMove(nextPos, 10f, false);
        var script1 = cameraControl.GetComponent<CameraZoom>();
        script1.Sound();
        yield return new WaitForSeconds(5f);
        touch = true;
    }
}
