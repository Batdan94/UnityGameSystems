using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils{

	public static float Map(float value, float minIn, float maxIn, float minOut, float maxOut)
    {
        float slope = (maxOut - minOut) / (maxIn - minIn);
        return minOut + slope * (value - minIn);
    }
}
