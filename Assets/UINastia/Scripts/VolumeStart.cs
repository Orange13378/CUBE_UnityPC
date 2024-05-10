using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeStart : MonoBehaviour
{

    public string volumeParameter = "MasterVolume";
    public AudioMixer mixer;


    // Start is called before the first frame update
    void Start()
    {
        var volumeValue = PlayerPrefs.GetFloat(volumeParameter, volumeParameter == "music" ? -20f : -10f);
        mixer.SetFloat(volumeParameter, volumeValue);

        
    }


}
