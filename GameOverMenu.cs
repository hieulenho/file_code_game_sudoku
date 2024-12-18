using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class GameOverMenu : MonoBehaviour
{
    public TextMeshProUGUI textClock;
    void Start()
    {
        textClock.text = Clock.instance.GetCurrentTimeText().text;
    }
}



