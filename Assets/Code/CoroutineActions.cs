using System;
using System.Collections;
using UnityEngine;

public static class CoroutineActions
{
    public static IEnumerator Interpolate(float runTime, Action<float> interpolationAction)
    {
        var startTime = Time.time;
        var elapsed = 0f;

        while (elapsed < 1)
        {
            elapsed = Mathf.Clamp((Time.time - startTime) / runTime, 0, 1);
            interpolationAction(elapsed);
            yield return 0;
        }
    }
}