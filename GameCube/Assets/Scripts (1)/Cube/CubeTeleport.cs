using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class CubeTeleport : MonoBehaviour
{
    private bool entered = false;

    public Vector3 vector = new Vector3();

    [SerializeField] private GameObject player;

    [SerializeField] CinemachineVirtualCamera vCamera;

    [SerializeField] private Image image;

    bool teleported = false, pressedE = false, dialog2 = true;
    public static bool dialog = true, switched = false;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
            if(entered)
            {
                if((Input.GetKeyDown(KeyCode.E)))
                {
                    if(!pressedE)
                    {
                        vCamera.gameObject.SetActive(true);
                        vCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 1f;
                        vCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 1f;
                        StartCoroutine(Teleport());
                    }
                }

                if (teleported)
                {
                    StartCoroutine(Teleporting());
                }

        }
    }


    IEnumerator Teleport()
    {
        DisablePlayerScript.off = true;
        teleported = true;
        pressedE = true;
        yield return new WaitUntil((() => !dialog2));

        if (DialogSystem.message.Count == 0)
        {
            DialogSystem.message.Add("Опять меня куда-то переместило");
            DialogSystem.on = true;
        }
    }

    IEnumerator Teleporting()
    {
        image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a + 0.3f * Time.deltaTime);
        yield return new WaitForSeconds(5f);
        WorldControl.goBlueWorld = true;
        player.transform.position = vector;
        image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a - 0.3f * Time.deltaTime);
        vCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0f;
        vCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 0f;
        PedestalUI.goBlueWorld = true;
        yield return new WaitForSeconds(5f);
        teleported = false;
        dialog2 = false;
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            entered = true;
        }
    }

     private void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            entered = false;
        }
    }
}
