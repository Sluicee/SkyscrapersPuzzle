using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameGrid : MonoBehaviour
{
    [Header ("Number Grid")]
    [Range(3, 9)]
    [SerializeField] private int columns = 0;
    private int rows;
    [SerializeField] private float squareOffset = 0.0f;
    [SerializeField] private float squareScale = 1.0f;
    [SerializeField] private GameObject gridSquare;

    [Header("Number Buttons")]
    [SerializeField] private GameObject numberButtonsGroup;
    [SerializeField] private GameObject numberButton;
    private List<GameObject> numberButtons = new List<GameObject>();

    private GameObject[,] gridSquares;

    void Start()
    {
        if (gridSquare.GetComponent<GridSquare>() == null)
            Debug.LogError("This Game Object need to have GridSquare script attached");

        rows = columns;
        gridSquares = new GameObject[rows, columns];
        SpawnGridSquares();
        SetGridNumber();
        SpawnGridNumberButtons();
    }

    void Update()
    {

    }

    private void SpawnGridSquares()
    {
        int squareIndex = 0;
        for (int row = 0; row < rows; ++row)
        {
            for (int column = 0; column < columns; ++column)
            {
                gridSquares[row, column] = Instantiate(gridSquare);
                gridSquares[row, column].GetComponent<GridSquare>().setSquareIndex(squareIndex);
                gridSquares[row, column].transform.parent = this.transform; //instantiate this square as a child of the script holder gameobject
                gridSquares[row, column].transform.localScale = new Vector3(squareScale, squareScale, squareScale);

                squareIndex++;
            }
        }

        var gridLayoutGroup = GetComponent<GridLayoutGroup>();
        gridLayoutGroup.cellSize *= squareScale;
        gridLayoutGroup.spacing = new Vector2(squareOffset, squareOffset);
        gridLayoutGroup.constraintCount = columns;
    }

    private void SetGridNumber()
    {
        int[][] board = new int[columns][];
        for (int i = 0; i < columns; i++)
        {
            board[i] = new int[columns];
        }
        board[0] = Utils.createOrderedArray(columns, 1);
        Utils.shuffle(board[0]);
        for (int x = 1; x < columns; x++)
        {
            board[x] = Utils.createOrderedArray(columns, 1);
            do
            {
                Utils.shuffle(board[x]);
            } while (!Utils.compare2DArray(board[x], board, 0, x));
        }

        for (int row = 0; row < rows; row++)
        {
            for (int column = 0; column < columns; column++)
            {
                gridSquares[row, column].GetComponent<GridSquare>().SetNumber(board[row][column]);
            }
        }
    }

    private void SpawnGridNumberButtons()
    {
        for(int i = 1; i <= columns; i++)
        {
            numberButtons.Add(Instantiate(numberButton) as GameObject);
            numberButtons[numberButtons.Count - 1].transform.parent = numberButtonsGroup.transform;
            numberButtons[numberButtons.Count - 1].GetComponent<NumberButton>().value = i;
            numberButtons[numberButtons.Count - 1].transform.localScale = new Vector3(squareScale, squareScale, squareScale);
        }
    }

}
