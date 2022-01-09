using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tutorial : MonoBehaviour
{
    // Start is called before the first frame update
    public Text text;
    public GameObject image;

    public State[] states;
    State state;
    private bool isShown = true;
    void Start()
    {
        Time.timeScale = 0;
        image.SetActive(true);
        text.text = states[0].GetStateText();
    }

    // Update is called once per frame
    void Update()
    {
        if (isShown && Input.GetKeyDown(KeyCode.Return))
        {
            isShown = false;
            image.SetActive(false);
            Time.timeScale = 1;

        }
    }
    public void RunDiaLog(int index)
    {
        image.SetActive(true);
        Time.timeScale = 0;
        text.text = states[index].GetStateText();
        isShown = true;


    }
}
