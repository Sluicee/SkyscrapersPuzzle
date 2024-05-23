using UnityEngine;
//using CrazyGames;
using YG;

public class GameController : MonoBehaviour
{
    public static GameController instance;
    private bool SecondChance = false;
    public GameObject losePopUp;
    public MenuControls menuControls;

    private MetricaSender metricaSender;

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
        //CrazySDK.Init(() => { /** initialization finished callback */ });

        
    }
    private bool pause;

    public bool isPaused
    {
        get { return pause; }
        set { pause = value; gameplayControl(); }
        
    }

    public void SecondChanceBtn()
    {
        YandexGame.RewVideoShow(2); // 2 = sc award
    }

    private void SecondChanceActivated()
    {
        //CRAZY SDK
        /*CrazySDK.Ad.RequestAd(CrazyAdType.Rewarded, () =>
        {
            CrazySDK.Game.GameplayStop();
        }, (error) =>
        {
            Debug.Log(error);
        }, () =>
        {
            SecondChance = true;
            losePopUp.SetActive(false);
            CrazySDK.Game.GameplayStart();
        });*/

        //YG SDK
        SecondChance = true;
        losePopUp.SetActive(false);
        Timer.instance.StartTimer();

        metricaSender.TrigerSend("SecondChance");
    }

    public void PlayAdToNewGame()
    {
        /*CrazySDK.Ad.RequestAd(CrazyAdType.Rewarded, () =>
        {
            CrazySDK.Game.GameplayStop();
            Debug.Log("MIDGAME AD STARTED");
        }, (error) =>
        {
            Debug.Log(error);
        }, () =>
        {
            CrazySDK.Game.GameplayStart();
            Debug.Log("MIDGAME AD FINISHED");
        });*/
    }

    public void PlayAdToLeave()
    {
        /*CrazySDK.Ad.RequestAd(CrazyAdType.Midgame, () =>
        {
            CrazySDK.Game.GameplayStop();
        }, (error) =>
        {
            Debug.Log(error);

        }, () =>
        {
            CrazySDK.Game.GameplayStart();
            menuControls.LoadScene("MainMenu");
        });*/
    }

    public bool GetSecondChance
    {
        get { return SecondChance; }
    }

    private void gameplayControl()
    {
        if (pause)
        {
            //CrazySDK.Game.GameplayStop();
        }
        else
        {
            //CrazySDK.Game.GameplayStart();
        }
    }

    private void Rewarded(int id)
    {
        if (id == 2)
        {
            SecondChanceActivated();
        }
    }

    public void ResetSaves()
    {
        YandexGame.ResetSaveProgress();
        YandexGame.SaveProgress();
    }

    private void OnEnable() => YandexGame.RewardVideoEvent += Rewarded;

    private void OnDisable() => YandexGame.RewardVideoEvent -= Rewarded;
}
