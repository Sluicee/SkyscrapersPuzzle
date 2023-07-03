using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class GridSquare : Selectable, IPointerClickHandler, ISubmitHandler, IPointerUpHandler, IPointerExitHandler, IDeselectHandler
{
    [SerializeField] private TMP_Text numberText;
    [SerializeField] private int number;

    private int guessedNumber;
    public void setGuessedNumber(int number) { guessedNumber = number; }
    public int getGuessedNumber() { return guessedNumber; }

    private bool selected = false;
    public bool isSelected() { return selected; }

    private int squareIndex = -1;
    public void setSquareIndex(int index) {squareIndex = index; }
    public int getSquareIndex() { return squareIndex; }

    private bool hasDefault = false;
    public void setHasDefault(bool value) { hasDefault = value;}
    public bool isHasDefault() { return hasDefault;}

    public bool IsCorrect()
    {
        return guessedNumber == number;
    }

    //DEBUG
    public void SetCorrectNumber()
    {
        guessedNumber = number;
        DisplayText(number);
    }

    public void DisplayText(int number)
    {
        if (number > 0)
            numberText.SetText(number.ToString());
        else
            numberText.SetText("");        
    }

    public void SetNumber(int number)
    {
        this.number = number;
        if(Random.Range(0, 100) >= 80)
        {
            DisplayText(number);
            hasDefault = true;
            this.guessedNumber = number;
        }
        else
        {
            DisplayText(0);
        }
    }

    public int GetNumber()
    {
        return number;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        selected = true;
        GameEvents.SquareSelectedMethod(squareIndex);
    }

    public void OnSubmit(BaseEventData eventData)
    {
        throw new System.NotImplementedException();
    }

    public void OnSetNumber(int number)
    {
        if (selected && !hasDefault)
        {
            guessedNumber = number;
            DisplayText(number);
        }
    }

    public void OnSquareSelected(int SquareIndex)
    {
        if (squareIndex != SquareIndex)
        {
            selected = false;
            SetSquareColor(colors.normalColor);
        }
        else
        {
            SetSquareColor(colors.pressedColor);
        }
    }

    public void SetSquareColor(Color color)
    {
        var colors = this.colors;
        colors.normalColor = color;
        this.colors = colors;
    }

    public void ClearNumber()
    {
        if (selected && !hasDefault)
        {
            numberText.SetText("");
            guessedNumber = 0;
        }
    }

    private void OnEnable()
    {
        GameEvents.OnUpdateSquareNumber += OnSetNumber;
        GameEvents.OnSquareSelected += OnSquareSelected;
        GameEvents.OnClearNumber += ClearNumber;
    }

    private void OnDisable()
    {
        GameEvents.OnUpdateSquareNumber -= OnSetNumber;
        GameEvents.OnSquareSelected -= OnSquareSelected;
        GameEvents.OnClearNumber -= ClearNumber;
    }
}
