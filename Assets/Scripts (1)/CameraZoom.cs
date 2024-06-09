using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using CubeECS;
using Leopotam.EcsLite;

public class CameraZoom : MonoBehaviour
{
    [SerializeField] 
    CinemachineVirtualCamera vCamera;

    [System.NonSerialized] 
    public static bool zoomed;

    [SerializeField] 
    private GameObject mainMenu, player, cube, vcam1, vcam2, gamePanel;

    [SerializeField] 
    private Image image;

    public AudioClip steepsSound;
    private AudioSource audioSource;
    private bool gameBegin, played;
    private Vector3 scaleChange;

    private EcsFilter _dialogFilter;
    private EcsPool<DialogComponent> _dialogPool;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
        scaleChange = new Vector3(-0.0010f, -0.0010f, 0f);
        gameBegin = false;
        zoomed = false;

        var world = EcsWorldManager.GetEcsWorld();
        _dialogFilter = world.Filter<DialogComponent>().End();
        _dialogPool = world.GetPool<DialogComponent>();
    }

    private void Update()
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

    private IEnumerator GameBegin()
    {
        yield return new WaitForSeconds(1f);
        cube.transform.localScale += scaleChange;
        mainMenu.SetActive(false);
        yield return new WaitForSeconds(5f);
        zoomed = true;
    }

    private IEnumerator PickedCube()
    {
        image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a + 0.25f * Time.deltaTime);
        yield return new WaitForSeconds(5f);
        CinemachineBegin.touch = false;
        played = true;
    }
    private IEnumerator StartGame()
    {
        image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a - 0.25f * Time.deltaTime);
        vCamera.Follow = player.transform;
        vcam1.SetActive(false);
        vcam2.SetActive(true);
        gamePanel.SetActive(true);
        yield return new WaitForSeconds(5f);
        StartDialog();
        gameObject.SetActive(false);
    }

    private void StartDialog()
    {
        foreach (var entity in _dialogFilter)
        {
            ref var dialogComponent = ref _dialogPool.Get(entity);
            dialogComponent.InputText = "Ãäå ÿ?";
            dialogComponent.DialogBehavior.StartDialog();
        }
    }
}
