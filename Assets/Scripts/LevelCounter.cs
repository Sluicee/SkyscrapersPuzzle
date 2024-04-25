using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class LevelCounter : MonoBehaviour
{
    [SerializeField] private int overall = 0;
    [SerializeField] private int complited = 0;
    [SerializeField] private TMP_Text overallText;

    private void Start()
    {
        Invoke("ShowCounter", 0.05f);
    }

    void ShowCounter()
    {
        for (int i = 4; i < 7; i++)
        {
            overall += GridData.Instance.data[i].Count;
        }
        overallText.text = overall.ToString();
    }

}
