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
}
