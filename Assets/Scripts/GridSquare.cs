using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GridSquare : Selectable
{
    [SerializeField] private TMP_Text numberText;
    private int number;

    void Start()
    {
        
    }


    void Update()
    {
        
    }

    public void DisplayText()
    {
        if (number <= 0)
        {
            numberText.SetText(" ");
        }
        else
        {
            numberText.SetText(number.ToString());
        }
    }

    public void SetNumber(int number)
    {
        this.number = number;
        DisplayText();
    }
}
