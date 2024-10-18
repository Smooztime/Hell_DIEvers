using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Timer : MonoBehaviour
{
    [SerializeField] TMP_Text timerText;
    [SerializeField] TMP_Text secondsText;
    public bool timerRunning = true;
    float seconds;
    int minutes;
    int hours;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Awake()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        if (timerRunning)
        {
            seconds += Time.deltaTime;
        }

        if(seconds >= 60)
        {
            seconds = 0;
            minutes++;
        }
        if(minutes >= 60)
        {
            minutes = 0;
            hours++;
        }
        if(minutes < 10)
        {
            timerText.text = (hours + ":0" + minutes + ":").ToString();
        }
        else
        {
            timerText.text = (hours + ":" + minutes + ":").ToString();
        }
        if (seconds < 10)
        {
            secondsText.text = ("0" + Mathf.Round(seconds * 100) / 100).ToString();
        }
        else
        {
            secondsText.text = (Mathf.Round(seconds * 100) / 100).ToString();
        }
        
    }
}
