using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainScreenScript : MonoBehaviour
{
    public AudioMixer audioMixer;
    private void Start()
    {
        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
        }
        Load();
    }
    public void StartGame()
    {
        LoadScene(3);
    }
   public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }
    public void SetVolume(float silderValue)
    {
        audioMixer.SetFloat("volume",Mathf.Log10(silderValue)*20);
        PlayerPrefs.SetFloat("musicVolume", silderValue);
    }
    public void Load() 
    {
        audioMixer.SetFloat("volume" , PlayerPrefs.GetFloat("musicVolume"));
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
