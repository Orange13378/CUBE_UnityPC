using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;

public class CubeTeleport2 : MonoBehaviour
{
    private bool entered = false;

    public Vector3 vector = new Vector3();

    [SerializeField] private GameObject player;

    [SerializeField] CinemachineVirtualCamera vCamera;

    [SerializeField] private Image image;

    bool teleported = false, pressedE = false, dialog2 = true;

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
                        DisablePlayerScript.off = true;
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
        pressedE = false;
        yield return new WaitForSeconds(1f);
        yield return new WaitUntil((() => !dialog2));
        
        /*if (DialogSystem.message.Count == 0)
        {
            DialogSystem.message.Add("Опять меня куда-то переместило");
            DialogSystem.on = true;
        }*/
    }

    IEnumerator Teleporting()
    {
        image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a + 0.3f * Time.deltaTime);
        yield return new WaitForSeconds(5f);
        WorldControl.goOrangeWorld = true;
        player.transform.position = vector;
        image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a - 0.3f * Time.deltaTime);
        vCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0f;
        vCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 0f;
        PedestalUI.goOrangeWorld = true;
        yield return new WaitForSeconds(6f);
        teleported = false;
        dialog2 = false;
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
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
