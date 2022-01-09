using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class InGameMenu : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject Menu;
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void ReturnMenu()
    {
        SceneManager.LoadScene("MainMenu");
        Time.timeScale = 1;
    }
    public void OpenOption()
    {

        GameObject a = Menu.transform.Find("Main").gameObject;
        a.SetActive(false);
        GameObject b = Menu.transform.Find("OptionConfig").gameObject;
        b.SetActive(true);
    }
    public void ReturnInGameMenu()
    {

        GameObject a = Menu.transform.Find("Main").gameObject;
        a.SetActive(true);
        GameObject b = Menu.transform.Find("OptionConfig").gameObject;
        b.SetActive(false);
    }
}
