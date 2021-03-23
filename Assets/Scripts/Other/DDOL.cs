using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DDOL : MonoBehaviour
{
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        GameManager.Instance.Scene(1);
    }
}
