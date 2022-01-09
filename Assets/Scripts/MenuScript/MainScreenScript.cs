using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainScreenScript : MonoBehaviour
{
    public AudioMixer audioMixer;
    public GameObject Gameinfo;
    private void Start()
    {
        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
        }

    }
    public void StartGame()
    {
        LoadScene(1);
    }


    public void LoadScene(int index)
    {
        SceneManager.LoadScene(index);
        GameObject gameinfo = GameObject.FindGameObjectWithTag("gameinfo");
        if (gameinfo!=null){
            GameObject.Destroy(gameinfo);
        }
        Instantiate(Gameinfo, new Vector3(0, 0, 0), Quaternion.identity);
            
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
