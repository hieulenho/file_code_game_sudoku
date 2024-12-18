using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;
using TMPro;
public class Clock : MonoBehaviour
{
    


    int hour_ = 0;
    int minute_ = 0;
    int seconds_ = 0;

    private TextMeshProUGUI textClock;
    //private Text textClock;
    private float delta_time;
    private bool stop_clock_ = false;
    public static Clock instance;
    private void Awake()
    {
        if (instance)
            Destroy(instance);
        instance = this;
        textClock = GetComponent<TextMeshProUGUI>();
        //textClock = GetComponent<Text>();
        delta_time = 0;
    }
    void Start()
    {
        stop_clock_ = false;
    }
    void Update()
    {
        if (GameSettings.Instance.GetPaused() == false && stop_clock_ == false)
        {
            delta_time += Time.deltaTime;
            TimeSpan span = TimeSpan.FromSeconds(delta_time);
            string hour = LeadingZero(span.Hours);
            string minute = LeadingZero(span.Minutes);
            string seconds = LeadingZero(span.Seconds);
            textClock.text = hour + ":" +minute + ":" +seconds;
        }
    }
    string LeadingZero(int n)
    {
        return n.ToString().PadLeft(2,'0');
    }
    public void OnGameOver()
    {
        stop_clock_ = true;
    }
    private void OnEnable()
    {
        GameEvents.OnGameOver += OnGameOver;
    }
    private void OnDisable()
    {
        GameEvents.OnGameOver -= OnGameOver;
    }
    public TextMeshProUGUI GetCurrentTimeText()
    {
        return textClock;
        
    }
    
}
