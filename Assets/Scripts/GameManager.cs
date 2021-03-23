using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    private int collectedCoins;

    public static GameManager Instance {
        get
        {
            if(instance == null)
            {
                instance = FindObjectOfType<GameManager>();
            }
            return instance;
        }
    }

    public int CollectedCoins { get => collectedCoins; set => collectedCoins = value; }
    public void Scene(int s)
    {
        if(s == 0)
        {
            SceneManager.LoadScene(s);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + s);
        }
    }
}
