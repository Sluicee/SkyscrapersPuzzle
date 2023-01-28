using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class Timer : MonoBehaviour
{
    private List<int> timer = new List<int>(2);
    private TMP_Text timerText;
    private float timerValue;
    private bool timerRunning = false;

    public static Timer instance;

    private void Awake()
    {
        if (instance)
            Destroy(instance);

        instance = this;

        timerText = GetComponent<TMP_Text>();
        timerValue = 0;
    }

    void Start()
    {
        timerRunning = true;
    }

    void Update()
    {
        if (timerRunning)
        {
            timerValue += Time.deltaTime;
            TimeSpan ts = TimeSpan.FromSeconds(timerValue);
            string minute = LeadingZero(ts.Minutes);
            string second = LeadingZero(ts.Seconds);

            timerText.SetText(minute + ":" + second);
        }
    }

    private string LeadingZero(int n)
    {
        return n.ToString().PadLeft(2, '0');
    }

    public void OnGameOver()
    {
        timerRunning = false;
    }

    public TMP_Text GetCurrentTimerText()
    {
        return timerText;
    }

    private void OnEnable()
    {
        GameEvents.OnGameOver += OnGameOver;
    }

    private void OnDisable()
    {
        GameEvents.OnGameOver -= OnGameOver;
    }
}
