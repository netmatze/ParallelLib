using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Intact.ParallelLib
{
    internal class Foreach<T> : ParallelConstruct
    {
        private WaitHandle[] waitHandles;

        public Foreach()
        {
            ThreadPool.SetMaxThreads(processorCount, processorCount);
            waitHandles = new WaitHandle[processorCount];
        }

        public void ForeachExecution(IEnumerable<T> source, Action<T> action)
        {            
            int counter = 0;
            while (source.GetEnumerator().MoveNext())
                counter++;
            chunk = counter / processorCount;
            int start = 0;                    
            int end = chunk;

            for (int i = 1; i <= processorCount; i++)
            {
                int x = i - 1;
                waitHandles[x] = new ManualResetEvent(false);
                ForeachPart<T> forEachPart = new ForeachPart<T>();
                forEachPart.ExecutionPart = action;
                forEachPart.Source = source;
                forEachPart.Start = start;
                forEachPart.End = end;
                ConstantForeachSynchronisationContainer<T> foreachSynchronisationContainer =
                    new ConstantForeachSynchronisationContainer<T>((ManualResetEvent)waitHandles[x], forEachPart);
                ThreadPool.QueueUserWorkItem(
                    delegate(object state)
                    {
                        ConstantForeachSynchronisationContainer<T> forSynchronisationContainer = (ConstantForeachSynchronisationContainer<T>)state;
                        ForeachPart<T> foreachPart = forSynchronisationContainer.ForEachPart_;
                        try
                        {
                            int innercounter = 0;
                            foreach (T sourceLocal in foreachPart.Source)
                            {
                                if (innercounter >= foreachPart.Start && innercounter <= foreachPart.End)
                                    foreachPart.ExecutionPart.Invoke(ref innercounter, sourceLocal);
                                else if (innercounter > foreachPart.End)
                                    break;
                                innercounter++;
                            }
                        }
                        finally
                        {
                            forSynchronisationContainer.ManualResetEvent_.Set();
                        }
                    }
                    , foreachSynchronisationContainer);
                start = end + 1;
                end = end + chunk;
            }
            WaitHandle.WaitAll(waitHandles);
        }

        public void ForeachExecution(IEnumerable<T> source, Action<T> action, ExecutionConstancyEnum executionConstancy)
        {
            if (executionConstancy == ExecutionConstancyEnum.Constant)
            {
                ForeachExecution(source, action);
            }
            else
            {
                ParallelQueue<ForeachPart<T>>[] parallelQueues = new ParallelQueue<ForeachPart<T>>[processorCount];
                int counter = 0;
                while (source.GetEnumerator().MoveNext())
                    counter++;
                chunk = counter / processorCount;
                int start = 0;
                int end = chunk;

                for (int i = 1; i <= processorCount; i++)
                {
                    int x = i - 1;
                    waitHandles[x] = new ManualResetEvent(false);                    
                    ParallelQueueFiller parallelQueueFiller = new ParallelQueueFiller();
                    ParallelQueue<ForeachPart<T>> parallelQueue = parallelQueueFiller.FillWithForeachPart<T>(start, end, action, source);
                    parallelQueues[x] = parallelQueue;
                    UnconstantForeachSynchronisationContainer<T> unconstantForeachSynchronisationContainer =
                        new UnconstantForeachSynchronisationContainer<T>((ManualResetEvent)waitHandles[x], parallelQueue, parallelQueues);
                    ThreadPool.QueueUserWorkItem(
                        delegate(object state)
                        {
                            UnconstantForeachSynchronisationContainer<T> localUnconstantForeachSynchronisationContainer =
                                (UnconstantForeachSynchronisationContainer<T>)state;
                            ParallelQueue<ForeachPart<T>> localparallelQueue = localUnconstantForeachSynchronisationContainer.ParallelQueue;
                            ParallelQueue<ForeachPart<T>>[] localparallelQueues = localUnconstantForeachSynchronisationContainer.ParallelQueues;
                            try
                            {
                                bool localQueueIsEmpty = false;
                                while (!localQueueIsEmpty)
                                {
                                    ForeachPart<T> foreachPart = localparallelQueue.Dequeue();
                                    if (foreachPart != null)
                                    {
                                        int ii = foreachPart.Start;
                                        foreachPart.ExecutionPart.Invoke(ref ii, foreachPart.SingleSource);
                                    }
                                    else
                                    {
                                        localQueueIsEmpty = true;
                                    }
                                }
                                foreach (ParallelQueue<ForeachPart<T>> localForegnParallelQueue in localparallelQueues)
                                {
                                    if (localForegnParallelQueue != localparallelQueue)
                                    {
                                        bool localForegnQueueIsEmpty = false;
                                        while (!localForegnQueueIsEmpty)
                                        {
                                            ForeachPart<T> foreachPart = localForegnParallelQueue.Steal();
                                            if (foreachPart != null)
                                            {
                                                int ii = foreachPart.Start;
                                                foreachPart.ExecutionPart.Invoke(ref ii, foreachPart.SingleSource);
                                            }
                                            else
                                            {
                                                localForegnQueueIsEmpty = true;
                                            }
                                        }
                                    }
                                }
                            }
                            finally
                            {
                                localUnconstantForeachSynchronisationContainer.ManualResetEvent_.Set();
                            }
                        }, unconstantForeachSynchronisationContainer);
                    start = end + 1;
                    end = end + chunk;
                }
                WaitHandle.WaitAll(waitHandles);
            }            
        }

        public void ForeachExecution<T1>(IEnumerable<T> source, Action<T, T1> action, T1 value1)
        {            
            int counter = 0;
            while (source.GetEnumerator().MoveNext())
                counter++;
            chunk = counter / processorCount;
            int start = 0;
            int end = chunk;

            for (int i = 1; i <= processorCount; i++)
            {
                int x = i - 1;
                waitHandles[x] = new ManualResetEvent(false);
                ForeachPart<T, T1> forEachPart = new ForeachPart<T, T1>();
                forEachPart.ExecutionPart = action;
                forEachPart.Source = source;
                forEachPart.Start = start;
                forEachPart.End = end;
                forEachPart.Value1 = value1;
                ConstantForeachSynchronisationContainer<T, T1> foreachSynchronisationContainer =
                    new ConstantForeachSynchronisationContainer<T, T1>((ManualResetEvent)waitHandles[x], forEachPart);
                ThreadPool.QueueUserWorkItem(
                    delegate(object state)
                    {
                        ConstantForeachSynchronisationContainer<T, T1> forSynchronisationContainer = (ConstantForeachSynchronisationContainer<T, T1>)state;
                        ForeachPart<T, T1> foreachPart = forSynchronisationContainer.ForEachPart_;
                        try
                        {
                            int innercounter = 0;
                            foreach (T sourceLocal in foreachPart.Source)
                            {
                                if (innercounter >= foreachPart.Start && innercounter <= foreachPart.End)
                                    foreachPart.ExecutionPart.Invoke(ref innercounter, sourceLocal, foreachPart.Value1);
                                else if (innercounter > foreachPart.End)
                                    break;
                                innercounter++;
                            }
                        }
                        finally
                        {
                            forSynchronisationContainer.ManualResetEvent_.Set();
                        }
                    }
                    , foreachSynchronisationContainer);
                start = end + 1;
                end = end + chunk;
            }
            WaitHandle.WaitAll(waitHandles);
        }

        public void ForeachExecution<T1>(IEnumerable<T> source, Action<T, T1> action, 
            ExecutionConstancyEnum executionConstancy, T1 value1)
        {
            if (executionConstancy == ExecutionConstancyEnum.Constant)
            {
                ForeachExecution<T1>(source, action, value1);
            }
            else
            {
                ParallelQueue<ForeachPart<T, T1>>[] parallelQueues = new ParallelQueue<ForeachPart<T, T1>>[processorCount];
                int counter = 0;
                while (source.GetEnumerator().MoveNext())
                    counter++;
                chunk = counter / processorCount;
                int start = 0;
                int end = chunk;

                for (int i = 1; i <= processorCount; i++)
                {
                    int x = i - 1;
                    waitHandles[x] = new ManualResetEvent(false);
                    ParallelQueueFiller parallelQueueFiller = new ParallelQueueFiller();
                    ParallelQueue<ForeachPart<T, T1>> parallelQueue = parallelQueueFiller.FillWithForeachPart<T, T1>(start, end, action, source, value1);
                    parallelQueues[x] = parallelQueue;
                    UnconstantForeachSynchronisationContainer<T, T1> unconstantForeachSynchronisationContainer =
                        new UnconstantForeachSynchronisationContainer<T, T1>((ManualResetEvent)waitHandles[x], parallelQueue, parallelQueues);
                    ThreadPool.QueueUserWorkItem(
                        delegate(object state)
                        {
                            UnconstantForeachSynchronisationContainer<T, T1> localUnconstantForeachSynchronisationContainer =
                                (UnconstantForeachSynchronisationContainer<T, T1>)state;
                            ParallelQueue<ForeachPart<T, T1>> localparallelQueue = localUnconstantForeachSynchronisationContainer.ParallelQueue;
                            ParallelQueue<ForeachPart<T, T1>>[] localparallelQueues = localUnconstantForeachSynchronisationContainer.ParallelQueues;
                            try
                            {
                                bool localQueueIsEmpty = false;
                                while (!localQueueIsEmpty)
                                {
                                    ForeachPart<T, T1> foreachPart = localparallelQueue.Dequeue();
                                    if (foreachPart != null)
                                    {
                                        int ii = foreachPart.Start;
                                        foreachPart.ExecutionPart.Invoke(ref ii, foreachPart.SingleSource, foreachPart.Value1);
                                    }
                                    else
                                    {
                                        localQueueIsEmpty = true;
                                    }
                                }
                                foreach (ParallelQueue<ForeachPart<T, T1>> localForegnParallelQueue in localparallelQueues)
                                {
                                    if (localForegnParallelQueue != localparallelQueue)
                                    {
                                        bool localForegnQueueIsEmpty = false;
                                        while (!localForegnQueueIsEmpty)
                                        {
                                            ForeachPart<T, T1> foreachPart = localForegnParallelQueue.Steal();
                                            if (foreachPart != null)
                                            {
                                                int ii = foreachPart.Start;
                                                foreachPart.ExecutionPart.Invoke(ref ii, foreachPart.SingleSource, foreachPart.Value1);
                                            }
                                            else
                                            {
                                                localForegnQueueIsEmpty = true;
                                            }
                                        }
                                    }
                                }
                            }
                            finally
                            {
                                localUnconstantForeachSynchronisationContainer.ManualResetEvent_.Set();
                            }
                        }, unconstantForeachSynchronisationContainer);
                    start = end + 1;
                    end = end + chunk;
                }
                WaitHandle.WaitAll(waitHandles);
            }
        }

        public void ForeachExecution<T1, T2>(IEnumerable<T> source, Action<T, T1, T2> action, T1 value1, T2 value2)
        {            
            int counter = 0;
            while (source.GetEnumerator().MoveNext())
                counter++;
            chunk = counter / processorCount;
            int start = 0;
            int end = chunk;

            for (int i = 1; i <= processorCount; i++)
            {
                int x = i - 1;
                waitHandles[x] = new ManualResetEvent(false);
                ForeachPart<T, T1, T2> forEachPart = new ForeachPart<T, T1, T2>();
                forEachPart.ExecutionPart = action;
                forEachPart.Source = source;
                forEachPart.Start = start;
                forEachPart.End = end;
                forEachPart.Value1 = value1;
                forEachPart.Value2 = value2;
                ConstantForeachSynchronisationContainer<T, T1, T2> foreachSynchronisationContainer =
                    new ConstantForeachSynchronisationContainer<T, T1, T2>((ManualResetEvent)waitHandles[x], forEachPart);
                ThreadPool.QueueUserWorkItem(
                    delegate(object state)
                    {
                        ConstantForeachSynchronisationContainer<T, T1, T2> forSynchronisationContainer = (ConstantForeachSynchronisationContainer<T, T1, T2>)state;
                        ForeachPart<T, T1, T2> foreachPart = forSynchronisationContainer.ForEachPart_;
                        try
                        {
                            int innercounter = 0;
                            foreach (T sourceLocal in foreachPart.Source)
                            {
                                if (innercounter >= foreachPart.Start && innercounter <= foreachPart.End)
                                    foreachPart.ExecutionPart.Invoke(ref innercounter, sourceLocal, foreachPart.Value1, foreachPart.Value2);
                                else if (innercounter > foreachPart.End)
                                    break;
                                innercounter++;
                            }
                        }
                        finally
                        {
                            forSynchronisationContainer.ManualResetEvent_.Set();
                        }
                    }, foreachSynchronisationContainer);
                start = end + 1;
                end = end + chunk;
            }
            WaitHandle.WaitAll(waitHandles);
        }

        public void ForeachExecution<T1, T2>(IEnumerable<T> source, Action<T, T1, T2> action, 
            ExecutionConstancyEnum executionConstancy, T1 value1, T2 value2)
        {
            if (executionConstancy == ExecutionConstancyEnum.Constant)
            {
                ForeachExecution<T1, T2>(source, action, value1, value2);
            }
            else
            {
                ParallelQueue<ForeachPart<T, T1, T2>>[] parallelQueues = new ParallelQueue<ForeachPart<T, T1, T2>>[processorCount];
                int counter = 0;
                while (source.GetEnumerator().MoveNext())
                    counter++;
                chunk = counter / processorCount;
                int start = 0;
                int end = chunk;

                for (int i = 1; i <= processorCount; i++)
                {
                    int x = i - 1;
                    waitHandles[x] = new ManualResetEvent(false);
                    ParallelQueueFiller parallelQueueFiller = new ParallelQueueFiller();
                    ParallelQueue<ForeachPart<T, T1, T2>> parallelQueue = parallelQueueFiller.FillWithForeachPart<T, T1, T2>(start, end, action, source, value1, value2);
                    parallelQueues[x] = parallelQueue;
                    UnconstantForeachSynchronisationContainer<T, T1, T2> unconstantForeachSynchronisationContainer =
                        new UnconstantForeachSynchronisationContainer<T, T1, T2>((ManualResetEvent)waitHandles[x], parallelQueue, parallelQueues);
                    ThreadPool.QueueUserWorkItem(
                        delegate(object state)
                        {
                            UnconstantForeachSynchronisationContainer<T, T1, T2> localUnconstantForeachSynchronisationContainer =
                                (UnconstantForeachSynchronisationContainer<T, T1, T2>)state;
                            ParallelQueue<ForeachPart<T, T1, T2>> localparallelQueue = localUnconstantForeachSynchronisationContainer.ParallelQueue;
                            ParallelQueue<ForeachPart<T, T1, T2>>[] localparallelQueues = localUnconstantForeachSynchronisationContainer.ParallelQueues;
                            try
                            {
                                bool localQueueIsEmpty = false;
                                while (!localQueueIsEmpty)
                                {
                                    ForeachPart<T, T1, T2> foreachPart = localparallelQueue.Dequeue();
                                    if (foreachPart != null)
                                    {
                                        int ii = foreachPart.Start;
                                        foreachPart.ExecutionPart.Invoke(ref ii, foreachPart.SingleSource, foreachPart.Value1, foreachPart.Value2);
                                    }
                                    else
                                    {
                                        localQueueIsEmpty = true;
                                    }
                                }
                                foreach (ParallelQueue<ForeachPart<T, T1, T2>> localForegnParallelQueue in localparallelQueues)
                                {
                                    if (localForegnParallelQueue != localparallelQueue)
                                    {
                                        bool localForegnQueueIsEmpty = false;
                                        while (!localForegnQueueIsEmpty)
                                        {
                                            ForeachPart<T, T1, T2> foreachPart = localForegnParallelQueue.Steal();
                                            if (foreachPart != null)
                                            {
                                                int ii = foreachPart.Start;
                                                foreachPart.ExecutionPart.Invoke(ref ii, foreachPart.SingleSource, foreachPart.Value1, foreachPart.Value2);
                                            }
                                            else
                                            {
                                                localForegnQueueIsEmpty = true;
                                            }
                                        }
                                    }
                                }
                            }
                            finally
                            {
                                localUnconstantForeachSynchronisationContainer.ManualResetEvent_.Set();
                            }
                        }, unconstantForeachSynchronisationContainer);
                    start = end + 1;
                    end = end + chunk;
                }
                WaitHandle.WaitAll(waitHandles);
            }
        }
    }
}
