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

    }
    public void StartGame()
    {
        LoadScene(3);
    }
  
   
   public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
    }
    
    public void QuitGame()
    {
        Application.Quit();
    }
}
