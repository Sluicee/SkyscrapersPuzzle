using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

public class GridSquare : Selectable, IPointerClickHandler, ISubmitHandler, IPointerUpHandler, IPointerExitHandler
{
    [SerializeField] private TMP_Text numberText;
    [SerializeField] private int number;
    private bool selected = false;
    public bool isSelected() { return selected; }

    private int squareIndex = -1;
    public void setSquareIndex(int index) {squareIndex = index; }

    void Start()
    {
        selected = false;
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

    public int GetNumber()
    {
        return number;
    }

    public void GuessNumber(int number)
    {
        numberText.SetText(number.ToString());
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

    private void OnEnable()
    {
        GameEvents.OnUpdateSquareNumber += OnSetNumber;
        GameEvents.OnSquareSelected += OnSquareSelected;
    }

    private void OnDisable()
    {
        GameEvents.OnUpdateSquareNumber -= OnSetNumber;
        GameEvents.OnSquareSelected -= OnSquareSelected;
    }

    public void OnSetNumber(int number)
    {
        if (selected)
        {
            GuessNumber(number);
        }
    }

    public void OnSquareSelected(int SquareIndex)
    {
        if (squareIndex != SquareIndex)
        {
            selected = false;
        }
    }
}
