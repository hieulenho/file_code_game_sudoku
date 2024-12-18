using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameWon : MonoBehaviour
{
    public GameObject WinPopup;
    public TextMeshProUGUI ClockText;

    void Start()
    {
        WinPopup.SetActive(false);
        ClockText.text = Clock.instance.GetCurrentTimeText().text;
    }

    private void OnBoardCompleted()
    {
        WinPopup.SetActive(true);
        ClockText.text = Clock.instance.GetCurrentTimeText().text;
    }

    private void OnEnable()
    {
        GameEvents.OnBoardCompleted += OnBoardCompleted;

    }

    private void OnDisable()
    {
        GameEvents.OnBoardCompleted -= OnBoardCompleted;
    }
}
