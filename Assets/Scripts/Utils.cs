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

    public static float NextGaussian()
    {
        float U, u, v, S;

        do
        {
            u = 2.0f * Random.value - 1.0f;
            v = 2.0f * Random.value - 1.0f;
            S = u * u + v * v;
        }
        while (S >= 1.0);

        float fac = Mathf.Sqrt(-2.0f * Mathf.Log(S) / S);
        return u * fac;
    }
}

public class Timer
{
    public float startTime;
    public float timeToTrigger;
    public float timeLeft
    {
        get { return timeToTrigger - (Time.time - startTime); }
    }

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


