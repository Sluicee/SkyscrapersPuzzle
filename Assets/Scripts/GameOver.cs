using UnityEngine;
using TMPro;
//using CrazyGames;
using YG;
using System;

public class GameOver : MonoBehaviour
{
    [Header ("Game Complited")]
    [SerializeField] private GameObject winPopUp;
    [SerializeField] private GameObject losePopUp;
    [SerializeField] private GameObject secondChanceButton;
    [SerializeField] private TMP_Text timerWinText;
    [SerializeField] private TMP_Text timerLoseText;
    [SerializeField] private GameObject checkPopUp;
    [SerializeField] private GameObject hintBtn;
    [SerializeField] private TMP_Text timerPauseText;

    private string technoName = "LEADERBOARD";
    private int score;
    private int lastScore;

    private MetricaSender metricaSender;


    private void Start()
    {
        winPopUp.SetActive(false);
        losePopUp.SetActive(false);
        checkPopUp.SetActive(false);
        //CrazySDK.Init(() => { /** initialization finished callback */ });
    }

    public void OnGameComplete(bool win, string levelID, int col)
    {
        Timer.instance.StopTimer();
        checkPopUp.SetActive(false);
        //CrazySDK.Game.GameplayStop();
        //hintBtn.SetActive(false);
        metricaSender.Send("lvlFinished");
        if (YandexGame.savesData.firstLVL)
        {
            metricaSender.TrigerSend("firstLvlFinished");
            YandexGame.savesData.firstLVL = false;
            YandexGame.SaveProgress();
        }
        if (win)
        {
            timerWinText.SetText(Timer.instance.GetCurrentTimerText().text);
            winPopUp.SetActive(true);

            //NORMAL SDK
            /*if (!PlayerPrefs.HasKey(levelID.ToString() + col.ToString()))
            {
                var count = PlayerPrefs.GetInt("complited");
                var count4 = PlayerPrefs.GetInt(col.ToString() + "lvl");
                PlayerPrefs.SetInt(levelID.ToString() + col.ToString(), levelID);
                PlayerPrefs.SetInt("complited", count+1);
                PlayerPrefs.SetInt(col.ToString() + "lvl", count4+1);
                Debug.Log(levelID.ToString() + col.ToString());
            }*/

            //YG SDK SAVES
            var lvls = YandexGame.savesData.openLevels;
            if (!lvls.Contains(levelID))
            {
                Debug.Log("Level not found in saves. Saving...");
                YandexGame.savesData.openLevels.Add(levelID);
                Debug.Log($"ID {levelID} {col} saved");
                YandexGame.savesData.complited++;
                YandexGame.savesData.lvls[col]++;
                Debug.Log("Counter increased");
                
                switch (col)
                {
                    case 4:
                        score = 5000 - Timer.instance.GetTime();
                        break;
                    case 5:
                        score = 7000 - Timer.instance.GetTime();
                        break;
                    case 6:
                        score = 10000 - Timer.instance.GetTime();
                        break;
                }
                lastScore = YandexGame.savesData.score;
                score = score + lastScore;
                YandexGame.savesData.score = score;
                YandexGame.NewLeaderboardScores(technoName, score);
                Debug.Log("Score:" + score); 

                YandexGame.SaveProgress();
                Debug.Log("Saved");
            }
            else
            {
                Debug.LogError("Save failure!");
            }

            metricaSender.TrigerSend("win");
        }
        else
        {
            timerLoseText.SetText(Timer.instance.GetCurrentTimerText().text);
            if (GameController.instance.GetSecondChance)
                secondChanceButton.SetActive(false);
            losePopUp.SetActive(true);
            metricaSender.TrigerSend("lose");
        }

    }

    public void Pause()
    {
        timerPauseText.SetText(Timer.instance.GetCurrentTimerText().text);
    }

    private void OnEnable()
    {
        GameEvents.OnGameComplited += OnGameComplete;
    }

    private void OnDisable()
    {
        GameEvents.OnGameComplited -= OnGameComplete;
    }
}
