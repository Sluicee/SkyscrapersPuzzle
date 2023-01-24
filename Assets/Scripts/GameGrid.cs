using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameGrid : MonoBehaviour
{
    [SerializeField] private int columns = 0;
    private int rows;
    [SerializeField] private float squareOffset = 0.0f;
    [SerializeField] private GameObject gridSquare;
    //[SerializeField] private Vector2 startPosition;
    [SerializeField] private float squareScale = 1.0f;

    private List<GameObject> gridSquares = new List<GameObject>();

    void Start()
    {
        if (gridSquare.GetComponent<GridSquare>() == null)
            Debug.LogError("This Game Object need to have GridSquare script attached");

        rows = columns;
        SpawnGridSquares();
        SetGridNumber();
    }

    void Update()
    {

    }

    private void SpawnGridSquares()
    {
        int squares = columns * rows;
        for (int i = 0; i < squares; i++)
        {
            gridSquares.Add(Instantiate(gridSquare) as GameObject);
            gridSquares[gridSquares.Count - 1].transform.parent = this.transform; //instantiate this square as a child of the script holder gameobject
            gridSquares[gridSquares.Count - 1].transform.localScale = new Vector3(squareScale, squareScale, squareScale);
        }

        var gridLayoutGroup = GetComponent<GridLayoutGroup>();
        gridLayoutGroup.cellSize *= squareScale;
        gridLayoutGroup.spacing = new Vector2(squareOffset, squareOffset);
    }

    private void SetGridNumber()
    {
        foreach(var square in gridSquares)
        {
            square.GetComponent<GridSquare>().SetNumber(Random.Range(1, columns));
        }
    }
}
