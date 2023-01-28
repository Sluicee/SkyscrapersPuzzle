using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class Highlighler : MonoBehaviour
{
    [SerializeField] private GameGrid grid;
    [SerializeField] private Color highlightColor;
    private Color defaultColor;
    private GameObject[,] squares;

    public static Highlighler Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(Instance);
    }

    /*private void Start()
    {
        lineData = grid.getGridSquares;
        foreach (GameObject i in lineData)
        {
            if (i.GetComponent<GridSquare>().getSquareIndex() == 30)
            {
                GetSquarePosition(i);
            }
        }
    }

    public void GetSquarePosition(GameObject square)
    {
        Debug.Log(Utils.findIndex(lineData, square));
    }

    public int[] GetColumn(int squareIndex)
    {
        int[] column = new int[grid.getSize];
        return column;
    }

    public int[] GetRow(int squareIndex)
    {
        int[] row = new int[grid.getSize];
        return row;
    }*/

    private void Start()
    {
        squares = grid.getGridSquares;
        defaultColor = squares[0, 0].GetComponent<GridSquare>().colors.normalColor;
    }

    //Highlight row and column of selected square
    public void Highlight(int squareIndex)
    {
        int[] selected = { -1, -1 }; //position of square
        
        for (int i = 0; i < squares.GetLength(0); i++)
        {
            for (int j = 0; j < squares.GetLength(1); j++)
            {
                if (squares[i,j].GetComponent<GridSquare>().getSquareIndex() == squareIndex) //find index of selected square
                {
                    selected[0] = i;
                    selected[1] = j;
                }
            }
        }

        for (int i = 0; i < squares.GetLength(0); i++)
        {
            for (int j = 0; j < squares.GetLength(1); j++)
            {

                if (i == selected[0] && j == selected[1])
                    continue;

                if (i == selected[0] || j == selected[1])
                    squares[i, j].GetComponent<GridSquare>().SetSquareColor(highlightColor);
                else
                    squares[i, j].GetComponent<GridSquare>().SetSquareColor(defaultColor);
            }
        }
    }

    public void deHighlight()
    {
        for (int i = 0; i < squares.GetLength(0); i++)
        {
            for (int j = 0; j < squares.GetLength(1); j++)
            {
                squares[i, j].GetComponent<GridSquare>().SetSquareColor(defaultColor);
            }
        }
    }

    private void OnEnable()
    {
        GameEvents.OnButtonPressed += deHighlight;
    }

    private void OnDisable()
    {
        GameEvents.OnButtonPressed -= deHighlight;
    }
}
