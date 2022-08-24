using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class AudioControl : MonoBehaviour
{

    public string volumeParameter = "MasterVolume";
    public AudioMixer mixer;
    public Slider slider;

    private const float _multiplier = 20f;
    private float _volumeValue;

    private void Awake()
    {
        slider.onValueChanged.AddListener(HandleSliderValueChanged);

    }

    private void HandleSliderValueChanged(float value)
    {
        _volumeValue = Mathf.Log10(value) * _multiplier;
        mixer.SetFloat(volumeParameter, _volumeValue);
    }

    // Start is called before the first frame update
    void Start()
    {
        _volumeValue = PlayerPrefs.GetFloat(volumeParameter, Mathf.Log10(slider.value) * _multiplier);
        slider.value = Mathf.Pow(10f, _volumeValue / _multiplier);
        
    }

    private void OnDisable()
    {
        PlayerPrefs.SetFloat(volumeParameter, _volumeValue);
    }


}
