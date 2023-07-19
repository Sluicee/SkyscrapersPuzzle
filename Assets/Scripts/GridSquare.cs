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

    [SerializeField] private GameObject notePrefab;
    [SerializeField] private Transform notes;
    private List<GameObject> notesNumber = new List<GameObject>();
    private bool noteActive;

    private GameObject grid;

    public void Start()
    {
        grid = GameObject.Find("Grid");
        noteActive = false;
        var colors = this.colors;
        colors.selectedColor = Themes.theme[0];
        colors.pressedColor = Themes.theme[0];
        this.colors = colors;
        InitNotes(grid.GetComponent<GameGrid>().getSize);
        SetNoteNumberValue(0);
    }

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
            numberText.fontStyle = FontStyles.Italic;
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
        Vibration.VibrateAndroid(10);
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
            if (noteActive)
            {
                SetNoteSingleNumberValue(number);
                guessedNumber = 0;
                DisplayText(0);
            }
            else
            {
                SetNoteNumberValue(0);
                guessedNumber = number;
                DisplayText(number);
                grid.GetComponent<GameGrid>().CheckIsFilled();
            }
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
            Vibration.VibrateAndroid(10);
            guessedNumber = 0;
            grid.GetComponent<GameGrid>().CheckIsFilled();
        }
    }

    public void InitNotes(int size)
    {
        for (int i = 0; i < size; i++)
        {
            var nt = Instantiate(notePrefab);
            nt.transform.SetParent(notes);
            nt.transform.localScale = new Vector3(1, 1, 1);
            nt.GetComponent<TMP_Text>().color = Themes.theme[1];
            notesNumber.Add(nt);
        }
    }

    public List<string> GetSquareNotes()
    {
        List<string> notes = new List<string>();

        foreach (var number in notesNumber)
        {
            notes.Add(number.GetComponent<TMP_Text>().text);
        }

        return notes;
    }

    public void SetClearEmptyNotes()
    {
        foreach ( var number in notesNumber)
        {
            if (number.GetComponent<TMP_Text>().text == "0")
            {
                number.GetComponent<TMP_Text>().text = " ";
            }
        }
    }

    private void SetNoteNumberValue(int value)
    {
        foreach( var number in notesNumber)
        {
            if (value <= 0)
            {
                number.GetComponent<TMP_Text>().text = " ";
            }
            else
            {
                number.GetComponent<TMP_Text>().text = value.ToString();
            }
        }
    }

    private void SetNoteSingleNumberValue(int value, bool forceUpdate = false)
    {
        if (!noteActive && !forceUpdate)
            return;

        if (value <= 0)
            notesNumber[value - 1].GetComponent<TMP_Text>().text = " ";
        else
        {
            if (notesNumber[value - 1].GetComponent<TMP_Text>().text == " " || forceUpdate)
                notesNumber[value - 1].GetComponent<TMP_Text>().text = value.ToString();
            else
                notesNumber[value - 1].GetComponent<TMP_Text>().text = " ";
        }
    }

    public void SetGridNotes(List<int> notes)
    {
        foreach( var note in notes)
        {
            SetNoteSingleNumberValue(note, true);
        }
    }

    public void NoteActive(bool active)
    {
        noteActive = active;
    }

    private void OnEnable()
    {
        GameEvents.OnUpdateSquareNumber += OnSetNumber;
        GameEvents.OnSquareSelected += OnSquareSelected;
        GameEvents.OnClearNumber += ClearNumber;
        GameEvents.OnNotesActive += NoteActive;
    }

    private void OnDisable()
    {
        GameEvents.OnUpdateSquareNumber -= OnSetNumber;
        GameEvents.OnSquareSelected -= OnSquareSelected;
        GameEvents.OnClearNumber -= ClearNumber;
        GameEvents.OnNotesActive -= NoteActive;
    }
}
