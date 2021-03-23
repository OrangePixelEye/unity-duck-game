using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
        Screen.orientation = ScreenOrientation.Landscape;
        
    }
    public void Play() {
        SceneManager.LoadScene(2);

    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
