using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.CompilerServices;
using System.Threading.Tasks.Sources;

public class GameEvents : MonoBehaviour 
{
    public delegate void UpdateSquareNumber(int nummber);
    public static event UpdateSquareNumber OnUpdataSquareNumber;

    public static void UpdateSquareNumberMethod(int number)
    {
        if (OnUpdataSquareNumber != null)
            OnUpdataSquareNumber(number);
    }

    public delegate void SquareSelected(int square_index);
    public static event SquareSelected OnSquareSelected;
    public static void SquareSelectedMethod(int square_index)
    {
        if (OnSquareSelected != null)
            OnSquareSelected(square_index);
    }

    public delegate void WrongNumber();
    public static event WrongNumber OnWrongNumber;
    public static void OnWrongNumberMethod()
    {
        if (OnWrongNumber != null)
            OnWrongNumber();
    }

    public delegate void GameOver();
    public static event GameOver OnGameOver;

    public static void OnGameOverMethod()
    {
        if(OnGameOver != null)
        {
            OnGameOver();
        }
            
    }

    public delegate void NotesActive(bool active);
    public static event NotesActive OnNotesActive;
    public static void OnNotesActiveMethod(bool active)
    {
        if (OnNotesActive != null)
            OnNotesActive(active);
    }

    public delegate void ClearNumber();
    public static event ClearNumber OnClearNumber;
    public static void OnClearNumberMethod()
    {
        if (OnClearNumber != null) 
            OnClearNumber();
    }

    public delegate void BoardCompleted();
    public static event BoardCompleted OnBoardCompleted;

    public static void OnBoardCompletedMethod() {
    
        if(OnBoardCompleted != null)
            { OnBoardCompleted(); }
    }
}

