using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControls : MonoBehaviour
{
    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void LoadGame(string size)
    {
        StaticClass.CrossSceneInformation = size;
        SceneManager.LoadScene("GameScene");
    }

    public void ActivateObject(GameObject gameObject)
    {
        gameObject.SetActive(true);
    }
    public void DisableObject(GameObject gameObject)
    {
        gameObject.SetActive(false);
    }

    public void Pause(bool pause)
    {
        GameController.instance.isPaused = pause;
    }
}
