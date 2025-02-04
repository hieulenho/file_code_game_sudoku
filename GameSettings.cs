using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class GameSettings : MonoBehaviour
{
    public enum EGameMode
    {
        NOT_SET,
        EASY,
        MEDIUM,
        HARD,
        VERY_HARD
    }
    public static GameSettings Instance;
    private void Awake()
    {
        _Paused = false;
        if (Instance == null)
        {
            DontDestroyOnLoad(this);
            Instance = this;
        }
        else
            Destroy(this);
    }
    private EGameMode _GameMode;
    private bool _Paused = false;

    public void SetPaused(bool paused) { _Paused = paused; }
    public bool GetPaused() { return _Paused; }

    void Start()
    {
        _GameMode = EGameMode.NOT_SET;
    }
    public void SetGameMode(EGameMode mode)
    {
        _GameMode = mode;
    }
    public void SetGameMode(string mode)
    {
        if (mode ==  "Easy")
            SetGameMode(EGameMode.EASY);
        else if (mode == "Medium")
            SetGameMode(EGameMode.MEDIUM);
        else if (mode == "Hard")
            SetGameMode(EGameMode.HARD);
        else if (mode == "VeryHard") 
            SetGameMode(EGameMode.VERY_HARD);
        else
            SetGameMode(EGameMode.NOT_SET);
    }
    public string GetGameMode()
    {
        switch (_GameMode)
        {
            case EGameMode.EASY: return "Easy";
            case EGameMode.MEDIUM: return "Medium";
            case EGameMode.HARD: return "Hard";
            case EGameMode.VERY_HARD: return "VeryHard";
        }
        Debug.LogError("ERROR: GameL Level is not set");
        return " ";
    }
}


