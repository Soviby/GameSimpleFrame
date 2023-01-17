using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class TimerHelper
{

    // ÑÓ³Ù  delay
    public static MyTask DelayFuc(Action callback, float delay)
    {
        return MyTask.Run(Delay( callback,  delay));
    }

    private static IEnumerator Delay(Action callback, float delay)
    {
        yield return delay * 1000;
        callback();
    }

    // Ñ­»·  duration
    public static MyTask LoopFuc(Action callback,float duration)
    {
        return MyTask.Run(Loop(callback, duration));
    }

    private static IEnumerator Loop(Action callback, float duration)
    {
        while (true)
        {
            yield return duration * 1000;
            callback();
        }
    }

    // Ñ­»·  duration
    public static MyTask LoopFrameFuc(Action callback)
    {
        return MyTask.Run(LoopFrame(callback));
    }

    private static IEnumerator LoopFrame(Action callback)
    {
        while (true)
        {
            yield return null;
            callback();
        }
    }
}
