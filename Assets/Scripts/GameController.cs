using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    private void Awake()
    {
        if (instance)
        {
            Destroy(instance);
        }
        else
        {
            DontDestroyOnLoad(this);
            instance = this;
        }
    }
    private bool pause;

    public bool isPaused
    {
        get { return pause; }
        set { pause = value; }
    }

}
