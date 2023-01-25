using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class GridSquare : Selectable
{
    [SerializeField] private TMP_Text numberText;
    public int number;

    void Start()
    {
        
    }


    void Update()
    {
        
    }

    public void DisplayText()
    {
        if (Random.Range(0, 10) > 8)
            numberText.SetText(number.ToString());
        else
            numberText.SetText(" ");
    }

    public void SetNumber(int number)
    {
        this.number = number;
        DisplayText();
    }
}
