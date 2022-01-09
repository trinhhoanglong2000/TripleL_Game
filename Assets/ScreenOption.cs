using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenOption : MonoBehaviour
{
    public Text screenStateText;
    bool fullScreen = true;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void SetScreen()
    {
        fullScreen = !fullScreen;
        if (fullScreen)
        {
            FullScreen();
            screenStateText.text = "Screen: FULL SCREEN";
        }
        else
        {
            screenStateText.text = "Screen: WINDOWED ";
            Windowed(1280, 720 );
        }
    }
    public void FullScreen()
    {
        if (!Screen.fullScreen)
        {
            FullScreenMode fullScreenMode = FullScreenMode.ExclusiveFullScreen;
            Screen.fullScreenMode = fullScreenMode;
            Screen.fullScreen = !Screen.fullScreen;
            Resolution currentResolution = Screen.currentResolution;
            Screen.SetResolution(currentResolution.width, currentResolution.height, fullScreenMode, 60);
        }

    }

    public void Windowed(int width, int height)
    {
        if (Screen.fullScreen)
        {
            FullScreenMode fullScreenMode = FullScreenMode.ExclusiveFullScreen;
            Screen.fullScreenMode = fullScreenMode;
            Resolution currentResolution = Screen.currentResolution;
            Screen.SetResolution(width, height, fullScreenMode, 60);
            Screen.fullScreen = !Screen.fullScreen;
        }
    }
}
