using System;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine;



public class timeStamp
{
    public int hours;
    public int minutes;

    private Action triggerAction;

    public timeStamp(int h, int m, Action t)
    {
        hours = h;
        minutes = m;
        triggerAction = t;
    }

    public void doEvent()
    {
        triggerAction();
        
    }


}

public class Scheduler : MonoBehaviour
{

    public Text ClockText;

    int hours;
    int minutes;
    float seconds;
    public float timeSpeed;

    List<timeStamp> timeSchedule;

    // Start is called before the first frame update
    void Start()
    {

        hours = 6;
        minutes = 0;
        seconds = 0;

        timeSchedule = new List<timeStamp>();

        timeSchedule.Add(new timeStamp(6, 30, KillPlayer));
    }


    private void Update()
    {
        seconds += timeSpeed * Time.deltaTime;
        if (seconds > 60)
        {
            minutes += 1;
            if (minutes > 60)
            {
                hours += 1;
                minutes = 0;
            }
            seconds = 0;
        }

        string clockHours = hours.ToString("00");
        string clockMinutes = minutes.ToString("00");

        ClockText.text = clockHours + " : " + clockMinutes;

        CheckSchedule();

        
    }

    void CheckSchedule()
    {
        foreach(timeStamp ts in timeSchedule)
        {
            if(hours == ts.hours && minutes == ts.minutes)
            {
                //do scheduled event
                ts.doEvent();
            }
        }
    }


    void KillPlayer()
    {
        SceneManager.LoadScene("TestLevel");
    }
}
