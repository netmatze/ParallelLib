using System;
using System.Collections.Generic;
using System.Text;

namespace Intact.ParallelLib
{
    internal class ParallelQueueFiller
    {
        internal ParallelQueue<InvokePart> FillWithInvoke(InvokeAction[] actions)
        {
            ParallelQueue<InvokePart> parallelQueue = new ParallelQueue<InvokePart>();            
            foreach (InvokeAction invokeAction in actions)
            {
                InvokePart invokePart = new InvokePart();
                invokePart.InvokationParts.Add(invokeAction);
                parallelQueue.Enqueue(invokePart);
            }            
            return parallelQueue;
        }

        internal ParallelQueue<InvokePart<T>> FillWithInvoke<T>(InvokeAction<T>[] actions, T value)
        {
            ParallelQueue<InvokePart<T>> parallelQueue = new ParallelQueue<InvokePart<T>>();
            foreach (InvokeAction<T> invokeAction in actions)
            {
                InvokePart<T> invokePart = new InvokePart<T>();
                invokePart.InvokationParts.Add(invokeAction);
                invokePart.Value = value;
                parallelQueue.Enqueue(invokePart);
            }
            return parallelQueue;
        }

        internal ParallelQueue<InvokePart<T1, T2>> FillWithInvoke<T1, T2>(InvokeAction<T1, T2>[] actions, T1 value1, T2 value2)
        {
            ParallelQueue<InvokePart<T1, T2>> parallelQueue = new ParallelQueue<InvokePart<T1, T2>>();
            foreach (InvokeAction<T1, T2> invokeAction in actions)
            {
                InvokePart<T1, T2> invokePart = new InvokePart<T1, T2>();
                invokePart.InvokationParts.Add(invokeAction);
                invokePart.Value1 = value1;
                invokePart.Value2 = value2;
                parallelQueue.Enqueue(invokePart);
            }
            return parallelQueue;
        }

        internal ParallelQueue<ForPart> FillWithForPart(int from, int to, int step, Action action)        
        {            
            ParallelQueue<ForPart> parallelQueue = new ParallelQueue<ForPart>();
            for (int i = from; i < to; i = i + step)
            {
                ForPart forPart = new ForPart(i,i);
                forPart.ExecutionPart = action;
                parallelQueue.Enqueue(forPart);
            }
            return parallelQueue;                    
        }        

        internal ParallelQueue<ForPart<T>> FillWithForPart<T>(int from, int to, int step, Action<T> action, T value)
        {
            ParallelQueue<ForPart<T>> parallelQueue = new ParallelQueue<ForPart<T>>();
            for (int i = from; i < to; i = i + step)
            {
                ForPart<T> forPart = new ForPart<T>(i, i);
                forPart.Value = value;
                forPart.ExecutionPart = action;
                parallelQueue.Enqueue(forPart);
            }
            return parallelQueue;
        }

        internal ParallelQueue<ForPart<T1, T2>> FillWithForPart<T1, T2>(int from, int to, int step, Action<T1, T2> action, T1 value1, T2 value2)
        {
            ParallelQueue<ForPart<T1, T2>> parallelQueue = new ParallelQueue<ForPart<T1, T2>>();
            for (int i = from; i < to; i = i + step)
            {
                ForPart<T1, T2> forPart = new ForPart<T1, T2>(i, i);
                forPart.Value1 = value1;
                forPart.Value2 = value2;
                forPart.ExecutionPart = action;
                parallelQueue.Enqueue(forPart);
            }
            return parallelQueue;
        }

        internal ParallelQueue<ForPart<T,T1>> FillWithForPart<T, T1>(int from, int to, int step, Action<T, T1> action)
        {
            ParallelQueue<ForPart<T, T1>> parallelQueue = new ParallelQueue<ForPart<T, T1>>();
            for (int i = from; i < to; i = i + step)
            {
                ForPart<T, T1> forPart = new ForPart<T, T1>(i, i);
                forPart.ExecutionPart = action;
                parallelQueue.Enqueue(forPart);
            }
            return parallelQueue;
        }

        internal ParallelQueue<ForeachPart<T>> FillWithForeachPart<T>(int from, int to, Action<T> action, IEnumerable<T> source)
        {
            ParallelQueue<ForeachPart<T>> parallelQueue = new ParallelQueue<ForeachPart<T>>();
            int innercounter = 0;
            foreach (T sourceLocal in source)
            {
                if (innercounter >= from && innercounter <= to)
                {
                    ForeachPart<T> foreachPart = new ForeachPart<T>();
                    foreachPart.Start = innercounter;
                    foreachPart.End = innercounter;
                    foreachPart.ExecutionPart = action;
                    foreachPart.SingleSource = sourceLocal;
                    parallelQueue.Enqueue(foreachPart);
                }
                else if (innercounter > to)
                    break;
                innercounter++;
            }            
            return parallelQueue;
        }

        internal ParallelQueue<ForeachPart<T, T1>> FillWithForeachPart<T, T1>(int from, int to, Action<T, T1> action, IEnumerable<T> source, T1 value1)
        {
            ParallelQueue<ForeachPart<T, T1>> parallelQueue = new ParallelQueue<ForeachPart<T, T1>>();
            int innercounter = 0;
            foreach (T sourceLocal in source)
            {
                if (innercounter >= from && innercounter <= to)
                {
                    ForeachPart<T, T1> foreachPart = new ForeachPart<T, T1>();
                    foreachPart.Start = innercounter;
                    foreachPart.End = innercounter;
                    foreachPart.ExecutionPart = action;
                    foreachPart.SingleSource = sourceLocal;
                    foreachPart.Value1 = value1;
                    parallelQueue.Enqueue(foreachPart);
                }
                else if (innercounter > to)
                    break;
                innercounter++;
            }
            return parallelQueue;
        }

        internal ParallelQueue<ForeachPart<T, T1, T2>> FillWithForeachPart<T, T1, T2>(int from, int to, Action<T, T1, T2> action, IEnumerable<T> source, T1 value1, T2 value2)
        {
            ParallelQueue<ForeachPart<T, T1, T2>> parallelQueue = new ParallelQueue<ForeachPart<T, T1, T2>>();
            int innercounter = 0;
            foreach (T sourceLocal in source)
            {
                if (innercounter >= from && innercounter <= to)
                {
                    ForeachPart<T, T1, T2> foreachPart = new ForeachPart<T, T1, T2>();
                    foreachPart.Start = innercounter;
                    foreachPart.End = innercounter;
                    foreachPart.ExecutionPart = action;
                    foreachPart.SingleSource = sourceLocal;
                    foreachPart.Value1 = value1;
                    foreachPart.Value2 = value2;
                    parallelQueue.Enqueue(foreachPart);
                }
                else if (innercounter > to)
                    break;
                innercounter++;
            }
            return parallelQueue;
        }
    }
}
