using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Timer
{
    private float remainingTime;   
    public Timer (float time)
    {
        this.remainingTime = time;
    }
    
    public void Tick(float deltaTime)
    {
        this.remainingTime -= 1*deltaTime;
        if(TimerUp())
        {
            ResetTimer();
        }
    }
    public float RemainingTime()
    {
        return remainingTime;
    }
    public void StartTimer(float time)
    {
        remainingTime = time;
    }
    private void ResetTimer()
    {
        remainingTime = 0;
    }
    public bool TimerUp()
    {
        if (remainingTime <= 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
