using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Intact.ParallelLib
{
internal static class Semaphore
{
    private static int maxSemaphoreCount = 2;
    private static int currentSemaphoreCount = 0;        
    private static object monitorObject = new object();

    public static int SemaphoreCount
    {
        get
        {
            return maxSemaphoreCount;
        }
        set
        {
            maxSemaphoreCount = value;
        }
    }

    public static void Enter()
    {            
        lock (monitorObject)
        {
            if (maxSemaphoreCount > currentSemaphoreCount)
            {
                Interlocked.Increment(ref currentSemaphoreCount);
            }
            else
            {
                if (maxSemaphoreCount == currentSemaphoreCount + 1)
                {                        
                    Interlocked.Increment(ref currentSemaphoreCount);
                    Monitor.Wait(monitorObject);
                }
                else
                {
                    if (maxSemaphoreCount == currentSemaphoreCount)
                    {
                        Monitor.Wait(monitorObject);
                        Interlocked.Increment(ref currentSemaphoreCount);
                    }
                }
            }
        }
    }

    public static void Exit()
    {
        lock (monitorObject)
        {
            if (currentSemaphoreCount > 0)
            {
                Interlocked.Decrement(ref currentSemaphoreCount);
            }
            Monitor.Pulse(monitorObject);
        }
    }
}
}
