using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEvents : MonoBehaviour
{
    public delegate void UpdateSquareNumber(int number);
    public static event UpdateSquareNumber OnUpdateSquareNumber;

    public static void UpdateSquareNumberMethod(int number)
    {
        if (OnUpdateSquareNumber != null)
        {
            OnUpdateSquareNumber(number);
        }
    }


    public delegate void SquareSelected(int squareIndex);
    public static event SquareSelected OnSquareSelected;

    public static void SquareSelectedMethod(int SquareIndex)
    {
        if (OnSquareSelected != null)
        {
            OnSquareSelected(SquareIndex);
        }
    }

    public delegate void GameOver();
    public static event GameOver OnGameOver;
    public static void GameOverMethod()
    {
        if (OnGameOver != null)
        {
            OnGameOver();
        }
    }

    public delegate void ButtonPressed();
    public static event ButtonPressed OnButtonPressed;
    public static void ButtonPressedMethod()
    {
        if (OnButtonPressed != null)
        {
            OnButtonPressed();
        }
    }

    public delegate void ClearNumber();
    public static event ClearNumber OnClearNumber;
    public static void ClearNumberMethod()
    {
        if (OnClearNumber != null)
            OnClearNumber();
    }

    public delegate void GameComplited(bool win);
    public static event GameComplited OnGameComplited;
    public static void GameComplitedMethod(bool win)
    {
        if (OnGameComplited != null)
            OnGameComplited(win);
    }

    public delegate void NotesActive(bool active);
    public static event NotesActive OnNotesActive;
    public static void OnNoteActiveMethod(bool active)
    {
        if (OnNotesActive != null)
        {
            OnNotesActive(active);
        }
    }
}
