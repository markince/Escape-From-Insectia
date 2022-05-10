using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CountUpTimer : MonoBehaviour
{
    public Text timerText;
    private float secondsCount;
    private int minuteCount;
    private int hourCount;
    void Update()
    {
        UpdateTimerUI();
    }
    //call this on update
    public void UpdateTimerUI()
    {
        //set timer UI
        secondsCount += Time.deltaTime;
        timerText.text = minuteCount.ToString("0") + ":" + ((int)secondsCount).ToString("00");
        if (secondsCount >= 60)
        {
            minuteCount++;
            secondsCount = 0;
        }
        else if (minuteCount >= 60)
        {
            hourCount++;
            minuteCount = 0;
        }
    }
}
