using UnityEngine;
using UnityEngine.SceneManagement;
//using CrazyGames;

public class MenuControls : MonoBehaviour
{
    private MetricaSender metricaSender;

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
        if(name == "GameScene")
        {
            //CrazySDK.Game.GameplayStart();
        }
        metricaSender.TrigerSend("LoadScene"+ name);
    }

    public void LoadGame(string size)
    {
        StaticClass.CrossSceneInformation = size;
        SceneManager.LoadScene("GameScene");
        //CrazySDK.Game.GameplayStart();
        metricaSender.TrigerSend("lvl"+size);

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
