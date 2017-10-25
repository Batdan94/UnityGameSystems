using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils
{
	public static float Map(float value, float minIn, float maxIn, float minOut, float maxOut)
    {
        float slope = (maxOut - minOut) / (maxIn - minIn);
        return minOut + slope * (value - minIn);
    }
}

public class Timer
{
    public float startTime;
    public float timeToTrigger;

    public Timer(float _timeToTrigger)
    {
        startTime = Time.time;
        timeToTrigger = _timeToTrigger;
    }

    public bool Trigger()
    {
        if (Time.time - startTime > timeToTrigger)
        {
            startTime = Time.time;
            return true;
        }
        return false;
    }
}
