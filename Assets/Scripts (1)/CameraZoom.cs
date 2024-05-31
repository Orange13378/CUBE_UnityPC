using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using CubeECS;
using Leopotam.EcsLite;

public class CameraZoom : MonoBehaviour
{
    public Camera cam;
    [SerializeField] CinemachineVirtualCamera vCamera;
    //public float maxZoom;
    //public float minZoom;
    public float speed =30;
    //float targetZoom;

    private Vector3 scaleChange;

    bool gameBegin, played;
    [System.NonSerialized] public static bool zoomed;

    [SerializeField] private GameObject mainMenu, player, cube, vcam1, vcam2, gamePanel;
    [SerializeField] private Image image;

    public AudioClip steepsSound;
    private AudioSource audioSource;

    private EcsWorld _world;
    private EcsFilter _dialogFilter;
    private EcsPool<DialogComponent> _dialogPool;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        scaleChange = new Vector3(-0.0010f, -0.0010f, 0f);
        gameBegin = false;
        zoomed = false;

        _world = EcsWorldManager.GetEcsWorld();
        _dialogFilter = _world.Filter<DialogComponent>().End();
        _dialogPool = _world.GetPool<DialogComponent>();
    }

    void Update()
    {
        if (gameBegin)
        {
            if(!zoomed) 
            {
                StartCoroutine(GameBegin());
            }
            else if (CinemachineBegin.touch) 
            {
                audioSource.Stop();
                vCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 1f;
                vCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 1f;
                StartCoroutine(PickedCube());
            }
            else if (!CinemachineBegin.touch && played)
            {
                vCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0f;
                vCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 0f;
                StartCoroutine(StartGame());
            }
        }
        else return;
        
    }

    public void NewGame()
    {
        gameBegin = true;
    }

    public void Sound()
    {
        audioSource.clip = steepsSound;
        audioSource.Play();
    }

    IEnumerator GameBegin()
    {
        yield return new WaitForSeconds(1f);
        cube.transform.localScale += scaleChange;
        mainMenu.SetActive(false);
        //targetZoom = Mathf.Clamp(targetZoom, maxZoom, minZoom);
        //float newSize = Mathf.MoveTowards(cam.orthographicSize, targetZoom, speed * Time.deltaTime);
        //cam.orthographicSize = newSize;
        yield return new WaitForSeconds(5f);
        zoomed = true;
        //cam.gameObject.GetComponent<CameraControl>().enabled = true;
    }

    IEnumerator PickedCube()
    {
        image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a + 0.25f * Time.deltaTime);
        yield return new WaitForSeconds(5f);
        CinemachineBegin.touch = false;
        played = true;
        //image.gameObject.SetActive(false);
    }
    IEnumerator StartGame()
    {
        image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a - 0.25f * Time.deltaTime);
        vCamera.Follow = player.transform;
        vcam1.SetActive(false);
        vcam2.SetActive(true);
        gamePanel.SetActive(true);
        yield return new WaitForSeconds(5f);
        StartDialog();
        //vCamera.gameObject.SetActive(false);
        gameObject.SetActive(false);
    }

    private void StartDialog()
    {
        foreach (var entity in _dialogFilter)
        {
            ref var dialogComponent = ref _dialogPool.Get(entity);
            dialogComponent.DialogItem.InputText = "Ãäå ÿ?";
            dialogComponent.DialogSystem.StartDialog();
        }
    }
}
