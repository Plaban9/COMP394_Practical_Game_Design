using System.Collections;
using System.Collections.Generic;

using UnityEngine;

public class Timer
{
    public float TIME_REMAINING
    {
        get; private set;
    }
    public float TIMER_TOTAL
    {
        get; private set;
    }

    public bool HAS_ENDED
    {
        get; private set;
    }
    public bool IS_PAUSED
    {
        get; private set;
    }

    public Timer(float totalTimer)
    {
        IS_PAUSED = true;
        TIMER_TOTAL = TIME_REMAINING = totalTimer;
    }

    public delegate void TimerEnded();
    public TimerEnded OnTimerEnd;

    //void Update()
    //{
    //    UpdateTimer();
    //}

    public void UpdateTimer()
    {
        if (!HAS_ENDED && !IS_PAUSED)
        {
            if (TIME_REMAINING > 0)
            {
                TIME_REMAINING -= Time.deltaTime;
            }
            else
            {
                TIME_REMAINING = 0f;
                HAS_ENDED = true;
                OnTimerEnd?.Invoke();
            }
        }
    }

    public void PauseTimer()
    {
        IS_PAUSED = true;
    }

    public void ResumeTimer()
    {
        IS_PAUSED = false;
    }

    public void StartTimer()
    {
        IS_PAUSED = false;
        HAS_ENDED = false;
    }

    public void StopTimer(bool resetTimer = false)
    {
        if (resetTimer)
        {
            ResetTimer();
        }
        else
        {
            HAS_ENDED = true;
            OnTimerEnd?.Invoke();
        }
    }

    public void ResetTimer()
    {
        IS_PAUSED = true;
        HAS_ENDED = false;
        TIME_REMAINING = TIMER_TOTAL;
    }

    public string GetDisplayTimer()
    {
        float minutes = Mathf.FloorToInt(TIME_REMAINING / 60);
        float seconds = Mathf.FloorToInt(TIME_REMAINING % 60);

        var formattedMinutes = minutes < 10 ? "0" + Mathf.Clamp(minutes, 0, 60) : Mathf.Clamp(minutes, 0, 60).ToString();
        var formattedSeconds = seconds < 10 ? "0" + Mathf.Clamp(seconds, 0, 60) : Mathf.Clamp(seconds, 0, 60).ToString();


        return $"{formattedMinutes}:{formattedSeconds}";
    }
}
