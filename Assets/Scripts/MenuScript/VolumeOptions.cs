using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class VolumeOptions : MonoBehaviour
{
    public AudioMixer audioMixer;
    private bool volumeState = true;
    public Text volumeStateText;
    private void Start()
    {
        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
        }
        Load();
    }
    public void ChangeTextVolumeState()
    {
        if (volumeState)
        {
            volumeStateText.text = "Volume: ON";
        }
        else
        {
            volumeStateText.text = "Volume: OFF";
        }
    }
    public void ChangeVolumeState()
    {
        volumeState = !volumeState;
        if (volumeState == true)
        {
            Load();
        }
        else
        {
            audioMixer.SetFloat("volume", -1000);
        }
        Debug.Log(volumeState);
    }
    public void SetVolume(float silderValue)
    {


        if (volumeState == true)
        {
            volumeStateText.text = "Volume: ON";
            audioMixer.SetFloat("volume", Mathf.Log10(silderValue) * 20);
            PlayerPrefs.SetFloat("musicVolume", silderValue);
        }
        else
        {
            volumeStateText.text = "Volume: OFF";
            audioMixer.SetFloat("volume", -1000);
        }

    }
    public void Load()
    {
        ChangeTextVolumeState();
        audioMixer.SetFloat("volume", PlayerPrefs.GetFloat("musicVolume"));
    }
}
