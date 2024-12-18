using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using TMPro;

public class PauseMenu : MonoBehaviour
{
    public TextMeshProUGUI time_text;

    public void Displaytime()
    {
        time_text.text = Clock.instance.GetCurrentTimeText().text;
    }
}
