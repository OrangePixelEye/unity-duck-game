using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Pause : MonoBehaviour
{

    [SerializeField]
    private Text pause;

    [SerializeField]
    private Button exit;


    private bool active = false;
    
    // Start is called before the first frame update
    void Start()
    {
       
    }

    // Update is called once per frame
    void Update()
    {
        if (Application.platform == RuntimePlatform.Android || Input.GetKeyDown(KeyCode.Escape))
        {

            // Check if Back was pressed this frame
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                PauseGame();
            }
        }
        
    }

    public void OpenMenu()
    {
        GameManager.Instance.Scene(0);
        DontDestroyOnLoad(pause);
        DontDestroyOnLoad(gameObject);
    }

    public void PauseGame()
    {
        Time.timeScale = Mathf.Approximately(Time.timeScale, 0.0f) ? 1.0f : 0.0f;

        if (!active)
        {
            pause.gameObject.SetActive(true);
            exit.gameObject.SetActive(true);
            active = true;
        }
        else
        {
            pause.gameObject.SetActive(false);
            exit.gameObject.SetActive(false);
            active = false;
        }
    }
}
