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

    private void Start()
    {
        winPopUp.SetActive(false);
        losePopUp.SetActive(false);
    }

    public void OnGameComplete(bool win)
    {
        Timer.instance.StopTimer();
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

    private void OnEnable()
    {
        GameEvents.OnGameComplited += OnGameComplete;
    }

    private void OnDisable()
    {
        GameEvents.OnGameComplited -= OnGameComplete;
    }
}
