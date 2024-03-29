using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOver : MonoBehaviour
{
    [Header ("Game Complited")]
    [SerializeField] private GameObject winPopUp;
    [SerializeField] private GameObject losePopUp;
    [SerializeField] private TMP_Text timerWinText;
    [SerializeField] private TMP_Text timerLoseText;
    [SerializeField] private GameObject checkPopUp;
    [SerializeField] private TMP_Text timerPauseText;


    private void Start()
    {
        winPopUp.SetActive(false);
        losePopUp.SetActive(false);
        checkPopUp.SetActive(false);
    }

    public void OnGameComplete(bool win)
    {
        Timer.instance.StopTimer();
        checkPopUp.SetActive(false);
        if (win)
        {
            timerWinText.SetText(Timer.instance.GetCurrentTimerText().text);
            winPopUp.SetActive(true);
        }
        else
        {
            timerLoseText.SetText(Timer.instance.GetCurrentTimerText().text);
            losePopUp.SetActive(true);
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
