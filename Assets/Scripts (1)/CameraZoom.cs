using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using Cinemachine;
using CubeMVC;

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
    private AudioSource _audioSource;
    private bool _gameBegin, _played;
    private Vector3 _scaleChange;

    [SerializeField]
    private ContextProvider _contextProvider;
    private DialogModel _dialogModel;

    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _scaleChange = new Vector3(-0.0010f, -0.0010f, 0f);
        _gameBegin = false;
        zoomed = false;

        _dialogModel = _contextProvider.GetContext().DialogModel;
    }

    private void Update()
    {
        if (!_gameBegin) return;

        if(!zoomed) 
        {
            StartCoroutine(GameBegin());
        }
        else if (CinemachineBegin.touch) 
        {
            _audioSource.Stop();
            vCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 1f;
            vCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 1f;
            StartCoroutine(PickedCube());
        }
        else if (!CinemachineBegin.touch && _played)
        {
            vCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_AmplitudeGain = 0f;
            vCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>().m_FrequencyGain = 0f;
            StartCoroutine(StartGame());
        }
    }

    public void NewGame()
    {
        _gameBegin = true;
    }

    public void Sound()
    {
        _audioSource.clip = steepsSound;
        _audioSource.Play();
    }

    private IEnumerator GameBegin()
    {
        yield return new WaitForSeconds(1f);
        cube.transform.localScale += _scaleChange;
        mainMenu.SetActive(false);
        yield return new WaitForSeconds(5f);
        zoomed = true;
    }

    private IEnumerator PickedCube()
    {
        image.color = new Color(image.color.r, image.color.g, image.color.b, image.color.a + 0.25f * Time.deltaTime);
        yield return new WaitForSeconds(5f);
        CinemachineBegin.touch = false;
        _played = true;
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
        _dialogModel.OnDialogStart("√де это €?");
    }
}
