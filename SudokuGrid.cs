using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class SudokuGrid : MonoBehaviour
{
    public int columns = 0;
    public int rows = 0;
    public float square_offset = 0.0f;
    public GameObject grid_square;
    public Vector2 start_position = new Vector2(0.0f, 0.0f);
    public float sqaure_scale = 1.0f;
    public float square_gap = 0.1f;
    public Color line_hightlight_color = Color.red;
    private List<GameObject> grid_sqaures_ = new List<GameObject>();
    private int selected_grid_data = -1;
    void Start()
    {
        if (grid_square.GetComponent<GridSquare>() == null)
            Debug.LogError("This Game Object need to have GridSquare Scripts attached !");

        CreateGrid();
        SetGridNumber(GameSettings.Instance.GetGameMode());

    }

    void Update()
    {

    }

    private void CreateGrid()
    {
        SpawnGridSquares();
        SetSquaresPosition();
        CreateBoldLines();
    }

    private void SpawnGridSquares()
    {
        int square_index = 0;
        for (int row = 0; row < rows; row++)
        {
            for (int column = 0; column < columns; column++)
            {
                grid_sqaures_.Add(Instantiate(grid_square) as GameObject);
                grid_sqaures_[grid_sqaures_.Count - 1].GetComponent<GridSquare>().SetSquareIndex(square_index);
                grid_sqaures_[grid_sqaures_.Count - 1].transform.parent = this.transform;
                grid_sqaures_[grid_sqaures_.Count - 1].transform.localScale = new Vector3(sqaure_scale, sqaure_scale, sqaure_scale);
                
                square_index++;
            }
        }
    }

    private void SetSquaresPosition()
    {
        var sqaure_rect = grid_sqaures_[0].GetComponent<RectTransform>();
        Vector2 offset = new Vector2();
        Vector2 sqaure_gap_number = new Vector2(0.0f, 0.0f);
        bool row_moved = false;

        offset.x = sqaure_rect.rect.width * sqaure_rect.transform.localScale.x + square_offset;
        offset.y = sqaure_rect.rect.height * sqaure_rect.transform.localScale.y + square_offset;

        int column_number = 0;
        int row_number = 0;

        foreach (GameObject sqaure in grid_sqaures_)
        {
            if (column_number + 1 > columns)
            {
                row_number++;
                column_number = 0;
                sqaure_gap_number.x = 0;
                row_moved = false;
            }

            var pos_x_offset = offset.x * column_number + (sqaure_gap_number.x * square_gap);
            var pos_y_offset = offset.y * row_number + (sqaure_gap_number.y * square_gap);

            if(column_number > 0 && column_number % 3 == 0)
            {
                sqaure_gap_number.x++;
                pos_x_offset += square_gap;
            }
            if(row_number > 0 &&  row_number % 3 == 0 && row_moved == false)
            {
                row_moved = true;
                sqaure_gap_number.y++;
                pos_y_offset += square_gap;
            }
            sqaure.GetComponent<RectTransform>().anchoredPosition = new Vector2(start_position.x + pos_x_offset, start_position.y - pos_y_offset);
            column_number++;

        }

    }

    private void CreateBoldLines()
    {

    }
    private void SetGridNumber(string level)
    {
        selected_grid_data = Random.Range(0, SudokuData.Instance.sudoku_game[level].Count);
        var data = SudokuData.Instance.sudoku_game[level][selected_grid_data];

        setGridSqaureData(data);
        //foreach (var square in grid_sqaures_)
        //{
        //    var gridSquare = square.GetComponent<GridSquare>();
        //    if (gridSquare != null)
        //    {
        //        gridSquare.SetNumber(Random.Range(1, 10));
        //    }
        //    else
        //    {
        //        Debug.LogError("GridSquare component is missing on one of the grid squares");
        //    }
        //}
    }

    private void setGridSqaureData(SudokuData.SudokuBoardData data) {
        for(int index = 0; index < grid_sqaures_.Count; index++)
        {
            grid_sqaures_[index].GetComponent<GridSquare>().SetNumber(data.unsolved_data[index]);
            grid_sqaures_[index].GetComponent<GridSquare>().SetCorrectNumber(data.solved_data[index]);
            grid_sqaures_[index].GetComponent<GridSquare>().SetHasDefaultValue(data.unsolved_data[index] != 0 && data.unsolved_data[index] == data.solved_data[index]);
        }
    }
    private void OnEnable()
    {
        GameEvents.OnSquareSelected += OnSquareSelected;
        GameEvents.OnUpdataSquareNumber += CheckBoardCompleted;
    }

    private void OnDisable()
    {
        GameEvents.OnSquareSelected -= OnSquareSelected;
        GameEvents.OnUpdataSquareNumber -= CheckBoardCompleted;

    }
    private void SetSquaresColor(int[] data, Color col)
    {
        foreach (var index in data)
        {
            var comp = grid_sqaures_[index].GetComponent<GridSquare>();
            if ( comp.HasWrongValue() == false && comp.IsSelected() == false)
            {
                comp.SetSqaureColour(col);
            }
        }
    }
    public void OnSquareSelected(int square_index)
    {
        var horizontal_line = LineIndicator.instance.GetHorizontalLine(square_index);
        var vertical_line = LineIndicator.instance.GetVerticalLine(square_index);
        var square = LineIndicator.instance.GetSquare(square_index);
        SetSquaresColor(LineIndicator.instance.GetAllSqauresIndexes(), Color.white);
        SetSquaresColor(horizontal_line, line_hightlight_color);
        SetSquaresColor(vertical_line, line_hightlight_color);
        SetSquaresColor(square, line_hightlight_color);

    }

    private void CheckBoardCompleted(int number)
    {
        foreach(var square in grid_sqaures_)
        {
            var comp = square.GetComponent<GridSquare>();
            if(comp.IsCorrectNumberSet() == false)
            {
                return;
            }
        }

        GameEvents.OnBoardCompletedMethod();
    }

    public void SolveSudoku()
    {
        foreach(var square in grid_sqaures_)
        {
            var comp = square.GetComponent<GridSquare>();
            comp.SetCorrectNumber();
        }

        CheckBoardCompleted(number : 0);
    }
}