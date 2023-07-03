using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class GameGrid : MonoBehaviour
{
    [Header ("Number Grid")]
    [Range(3, 9)]
    [SerializeField] private int columns = 0;
    public int getSize { get { return columns; } }
    private int rows;
    [SerializeField] private float squareOffset = 0.0f;
    [SerializeField] private float squareScale = 1.0f;
    [SerializeField] private GameObject gridSquare;

    [Header("Skyscrapers Count")]
    [SerializeField] private GameObject skyScrapersCount;
    [SerializeField] private GameObject skyScrapersCountGroup;
    Dictionary<string, GameObject> skyScrapers; //-y(row) - vertical left, +y(row) vertical right from up to down; -x(column) - horizontal bottom, +x(column) horizontal top from left to right

    [Header("Number Buttons")]
    [SerializeField] private GameObject numberButtonsGroup;
    [SerializeField] private GameObject numberButton;
    private List<GameObject> numberButtons = new List<GameObject>();

    private GameObject[,] gridSquares;

    public GameObject[,] getGridSquares
    {
        get { return gridSquares; }
        private set { gridSquares = value; }
    }

    void Start()
    {
        if (gridSquare.GetComponent<GridSquare>() == null)
            Debug.LogError("This Game Object need to have GridSquare script attached");

        rows = columns;
        gridSquares = new GameObject[rows, columns];
        skyScrapers = new Dictionary<string, GameObject>(columns*4);
        SpawnGridSquares();
        SetGridNumber();
        SpawnSkyscrapersCount();
        SpawnGridNumberButtons();
    }

    void Update()
    {

    }

    /*
   Creates empty gameobjects (no numbers), with a GridLayout component attached to the parent that arranges these objects in a grid, and whose parameters are changed in this method.
    */
    private void SpawnGridSquares()
    {
        int squareIndex = 0;
        for (int row = 0; row < rows; ++row)
        {
            for (int column = 0; column < columns; ++column)
            {
                gridSquares[row, column] = Instantiate(gridSquare);
                gridSquares[row, column].GetComponent<GridSquare>().setSquareIndex(squareIndex);
                gridSquares[row, column].transform.SetParent(this.transform); //instantiate this square as a child of the script holder gameobject
                gridSquares[row, column].transform.localScale = new Vector3(squareScale, squareScale, squareScale);

                squareIndex++;
            }
        }

        var gridLayoutGroup = GetComponent<GridLayoutGroup>();
        gridLayoutGroup.cellSize *= squareScale;
        gridLayoutGroup.spacing = new Vector2(squareOffset, squareOffset);
        gridLayoutGroup.constraintCount = columns;

        LayoutRebuilder.ForceRebuildLayoutImmediate(this.GetComponent<RectTransform>());
    }

    /*
    Creates a two-dimensional array of numbers. 
    The nested array is row. This all creates a field in which the row and column cannot repeat numbers. 
    I have no idea how this works, it's just copy-pasted.
    */
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

    /*
    Counts the number of visible skyscrapers in a column or row.
    Each axis has a skyscraper count and the highest number at the moment of the enumeration. 
    The search starts with the second number in the row/column and compares it to the previous number; if it is higher, the counter is incremented. 
    During one cycle of the line/column search, each side of the playing field is compared at the same time. 
    The variables row, columnLeft, columnRight are conditional and have nothing to do with row/column, as you might guess from the name, they are just for counting.
    */
    private void SpawnSkyscrapersCount()
    {

        int counter1 = 1; //left
        int counter2 = 1; //right
        int counter3 = 1; //top
        int counter4 = 1; //bottom
        int highest1 = 1; //left
        int highest2 = 1; //right
        int highest3 = 1; //top
        int highest4 = 1; //bottom
        
        for (int row = 0; row < rows; row++)
        {
            counter1 = 1;
            counter2 = 1;
            counter3 = 1;
            counter4 = 1;
            highest1 = gridSquares[row, 0].GetComponent<GridSquare>().GetNumber();
            highest2 = gridSquares[row, columns - 1].GetComponent<GridSquare>().GetNumber();
            highest3 = gridSquares[0, row].GetComponent<GridSquare>().GetNumber();
            highest4 = gridSquares[columns - 1, row].GetComponent<GridSquare>().GetNumber();

            for (int columnLeft = 1, columnRight = columns - 2; columnLeft < columns && columnRight >= 0; columnLeft++, columnRight--)
            {
                //left right
                if (gridSquares[row, columnLeft].GetComponent<GridSquare>().GetNumber() > highest1)
                {
                    highest1 = gridSquares[row, columnLeft].GetComponent<GridSquare>().GetNumber();
                    counter1++;
                }
                if (gridSquares[row, columnRight].GetComponent<GridSquare>().GetNumber() > highest2)
                {
                    highest2 = gridSquares[row, columnRight].GetComponent<GridSquare>().GetNumber();
                    counter2++;
                }
                //top bottom
                if (gridSquares[columnLeft, row].GetComponent<GridSquare>().GetNumber() > highest3)
                {
                    highest3 = gridSquares[columnLeft, row].GetComponent<GridSquare>().GetNumber();
                    counter3++;
                }
                if (gridSquares[columnRight, row].GetComponent<GridSquare>().GetNumber() > highest4)
                {
                    highest4 = gridSquares[columnRight, row].GetComponent<GridSquare>().GetNumber();
                    counter4++;
                }
            }

            SetSkyScrapersCount(counter1, "-y", row);
            SetSkyScrapersCount(counter2, "+y", row);
            SetSkyScrapersCount(counter3, "+x", row);
            SetSkyScrapersCount(counter4, "-x", row);
        }
    }

    //spawn number on screen
    private void SpawnGridNumberButtons()
    {
        for(int i = 1; i <= columns; i++)
        {
            numberButtons.Add(Instantiate(numberButton) as GameObject);
            numberButtons[numberButtons.Count - 1].transform.SetParent(numberButtonsGroup.transform);
            numberButtons[numberButtons.Count - 1].GetComponent<NumberButton>().value = i;
            numberButtons[numberButtons.Count - 1].transform.localScale = new Vector3(squareScale, squareScale, squareScale);
        }
    }

    //places numbers with the number of visible skyscrapers next to the row or column they belong to 
    private void SetSkyScrapersCount(int counter, string key, int row)
    {
        float gap = Vector3.Distance(gridSquares[0, 0].transform.position, gridSquares[0, 1].transform.position);
        skyScrapers[key] = Instantiate(skyScrapersCount);
        skyScrapers[key].transform.SetParent(skyScrapersCountGroup.transform);
        skyScrapers[key].transform.localScale = new Vector3(squareScale, squareScale, squareScale);
        skyScrapers[key].GetComponent<TMP_Text>().SetText(counter.ToString());
        Vector3 referenceElementPosition;
        switch (key)
        {
            case "-y":
                referenceElementPosition = gridSquares[row, 0].transform.position;
                skyScrapers[key].transform.position = new Vector3(referenceElementPosition.x - gap, referenceElementPosition.y, 0);
                break;
            case "+y":
                referenceElementPosition = gridSquares[row, columns - 1].transform.position;
                skyScrapers[key].transform.position = new Vector3(referenceElementPosition.x + gap, referenceElementPosition.y, 0);
                break;
            case "+x":
                referenceElementPosition = gridSquares[0, row].transform.position;
                skyScrapers[key].transform.position = new Vector3(referenceElementPosition.x, referenceElementPosition.y + gap, 0);
                break;
            case "-x":
                referenceElementPosition = gridSquares[columns - 1, row].transform.position;
                skyScrapers[key].transform.position = new Vector3(referenceElementPosition.x, referenceElementPosition.y - gap, 0);
                break;
        }
    }

    public void CheckBoardCompleted()
    {
        bool result = false;
        foreach (var square in gridSquares)
        {
            var comp = square.GetComponent<GridSquare>();
            if (!comp.IsCorrect())
            {
                Debug.Log("Error");
                result = false;
                break;
            }
            else
            {
                Debug.Log("Correct");
                result = true;
            }
        }
        GameEvents.GameComplitedMethod(result);
    }

    public void OnSquareSelected(int squareIndex)
    {
        Highlighler.Instance.Highlight(squareIndex);
    }

    private void OnEnable()
    {
        GameEvents.OnSquareSelected += OnSquareSelected;
    }

    private void OnDisable()
    {
        GameEvents.OnSquareSelected -= OnSquareSelected;
    }

    //DEBUG
    [ContextMenu("Solve")]
    public void Solve()
    {
        foreach(var square in gridSquares)
        {
            var comp = square.GetComponent<GridSquare>();
            comp.SetCorrectNumber();
        }
        CheckBoardCompleted();
    }

}
