using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Intact.ParallelLib
{
    internal class For : ParallelConstruct
    {
        private WaitHandle[] waitHandles;                

        #region public For()
      
        public For()
        {
            ThreadPool.SetMaxThreads(processorCount, processorCount);
            waitHandles = new WaitHandle[processorCount];
        }

        #endregion      

        public void ForExecution(int from, int to, int step, Action action)
        {            
            chunk = (to - from) / processorCount;
            int start = from;
            int end = chunk;
            for (int i = 1; i <= processorCount; i++)
            {
                int x = i - 1;
                waitHandles[x] = new ManualResetEvent(false);
                ForPart forPart = new ForPart(start, end, step);
                forPart.ExecutionPart = action;
                ConstantForSynchronisationContainer forSynchronisationContainer =
                    new ConstantForSynchronisationContainer((ManualResetEvent)waitHandles[x], forPart);
                ThreadPool.QueueUserWorkItem( 
                    delegate(object state)
                    {
                        ConstantForSynchronisationContainer localforSynchronisationContainer = (ConstantForSynchronisationContainer)state;
                        ForPart localforPart = localforSynchronisationContainer.ForPart_;
                        try
                        {
                            for (int ii = localforPart.From; ii <= localforPart.To; ii = ii + localforPart.Step)
                            {
                                localforPart.ExecutionPart.Invoke(ref ii);
                            }
                        }
                        finally
                        {
                            localforSynchronisationContainer.ManualResetEvent_.Set();
                        }
                    }, forSynchronisationContainer);
                start = end + 1;
                end = end + chunk;
            }
            WaitHandle.WaitAll(waitHandles);
        }

        public void ForExecution(int from, int to, int step, Action action, ExecutionConstancyEnum executionConstancy)
        {
            if (executionConstancy == ExecutionConstancyEnum.Constant)
            {
                ForExecution(from, to, step, action);
            }
            else
            {                
                ParallelQueue<ForPart>[] parallelQueues = new ParallelQueue<ForPart>[processorCount];
                chunk = (to - from) / processorCount;
                int start = from;
                int end = chunk;

                for (int i = 1; i <= processorCount; i++)
                {
                    int x = i - 1;
                    waitHandles[x] = new ManualResetEvent(false);
                    ParallelQueueFiller parallelQueueFiller = new ParallelQueueFiller();
                    ParallelQueue<ForPart> parallelQueue = parallelQueueFiller.FillWithForPart(start, end, step, action);
                    parallelQueues[x] = parallelQueue;
                    UnconstantForSynchronisationContainer unconstantForSynchronisationContainer =
                        new UnconstantForSynchronisationContainer((ManualResetEvent)waitHandles[x], parallelQueue, parallelQueues);
                    ThreadPool.QueueUserWorkItem(
                        delegate(object state)
                        {
                            UnconstantForSynchronisationContainer localUnconstantForSynchronisationContainer = 
                                (UnconstantForSynchronisationContainer)state;
                            ParallelQueue<ForPart> localparallelQueue = localUnconstantForSynchronisationContainer.ParallelQueue;
                            ParallelQueue<ForPart>[] localparallelQueues = localUnconstantForSynchronisationContainer.ParallelQueues;
                            try
                            {
                                bool localQueueIsEmpty = false;                            
                                while (!localQueueIsEmpty)
                                {
                                    ForPart forPart = localparallelQueue.Dequeue();
                                    if (forPart != null)
                                    {
                                        int ii = forPart.From;
                                        forPart.ExecutionPart.Invoke(ref ii);
                                    }
                                    else
                                    {
                                        localQueueIsEmpty = true;
                                    }
                                }                            
                                foreach (ParallelQueue<ForPart> localForegnParallelQueue in localparallelQueues)
                                {
                                    if (localForegnParallelQueue != localparallelQueue)
                                    {
                                        bool localForegnQueueIsEmpty = false;
                                        while (!localForegnQueueIsEmpty)
                                        {
                                            ForPart forPart = localForegnParallelQueue.Steal();
                                            if (forPart != null)
                                            {
                                                int ii = forPart.From;
                                                forPart.ExecutionPart.Invoke(ref ii);
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
                                localUnconstantForSynchronisationContainer.ManualResetEvent_.Set();
                            }
                        }, unconstantForSynchronisationContainer);
                    start = end + 1;
                    end = end + chunk;
                }
                WaitHandle.WaitAll(waitHandles);
            }
        }

        public void ForExecution<TState, T>(int from, int to, int step,
            Func<T, TState> state, Action<T, ThreadLocalState<TState>> function, 
            InvokeAction<T, ThreadLocalState<TState>> aggregation, T value)
        {
            TState initialState = state(value);
            List<ForPartAggregation<TState, T>> forParts = new List<ForPartAggregation<TState, T>>();
            chunk = (to - from) / processorCount;
            int start = from;
            int end = chunk;

            for (int i = 1; i <= processorCount; i++)
            {
                int x = i - 1;
                waitHandles[x] = new ManualResetEvent(false);
                ForPartAggregation<TState, T> forPartAggregation = new ForPartAggregation<TState, T>(start, end, step);
                forPartAggregation.ExecutionPart = function;
                forPartAggregation.Statevalue = new ThreadLocalState<TState>(initialState);
                forPartAggregation.Value1 = value;
                forParts.Add(forPartAggregation);
                ConstantForAggregationSynchronisationContainer<TState, T> forSynchronisationContainer =
                   new ConstantForAggregationSynchronisationContainer<TState, T>((ManualResetEvent)waitHandles[x], forPartAggregation);
                ThreadPool.QueueUserWorkItem(
                    delegate(object localstate)
                    {
                        ConstantForAggregationSynchronisationContainer<TState, T> localforSynchronisationContainer = (ConstantForAggregationSynchronisationContainer<TState, T>)localstate;
                        ForPartAggregation<TState, T> localforPart = localforSynchronisationContainer.ForPart_;
                        try
                        {
                            for (int ii = localforPart.From; ii <= localforPart.To; ii = ii + localforPart.Step)
                            {
                                localforPart.ExecutionPart.Invoke(ref ii, localforPart.Value1, localforPart.Statevalue);
                            }
                        }
                        finally
                        {
                            localforSynchronisationContainer.ManualResetEvent_.Set();
                        }
                    }, forSynchronisationContainer);
                start = end + 1;
                end = end + chunk;
            }            
            WaitHandle.WaitAll(waitHandles);
            foreach (ForPartAggregation<TState, T> localforPart in forParts)
            {
                aggregation(localforPart.Value1, localforPart.Statevalue);
            }
        }

        public void ForExecution<TState>(int from, int to, int step,
            Func<TState> state, Action<ThreadLocalState<TState>> function, InvokeAction<ThreadLocalState<TState>> aggregation)
        {
            TState initialState = state();
            List<ForPartAggregation<TState>> forParts = new List<ForPartAggregation<TState>>();
            chunk = (to - from) / processorCount;
            int start = from;
            int end = chunk;

            for (int i = 1; i <= processorCount; i++)
            {
                int x = i - 1;
                waitHandles[x] = new ManualResetEvent(false);
                ForPartAggregation<TState> forPartAggregation = new ForPartAggregation<TState>(start, end, step);
                forPartAggregation.ExecutionPart = function;
                forPartAggregation.Value = new ThreadLocalState<TState>(initialState);
                forParts.Add(forPartAggregation);
                ConstantForAggregationSynchronisationContainer<TState> forSynchronisationContainer =
                    new ConstantForAggregationSynchronisationContainer<TState>((ManualResetEvent)waitHandles[x], forPartAggregation);
                ThreadPool.QueueUserWorkItem(
                    delegate(object localstate)
                    {
                        ConstantForAggregationSynchronisationContainer<TState> localforSynchronisationContainer = 
                            (ConstantForAggregationSynchronisationContainer<TState>)localstate;
                        ForPartAggregation<TState> localforPart = localforSynchronisationContainer.ForPart_;
                        try
                        {
                            for (int ii = localforPart.From; ii <= localforPart.To; ii = ii + localforPart.Step)
                            {
                                localforPart.ExecutionPart.Invoke(ref ii, localforPart.Value);
                            }
                        }
                        finally
                        {
                            localforSynchronisationContainer.ManualResetEvent_.Set();
                        }
                    }, forSynchronisationContainer);
                start = end + 1;
                end = end + chunk;
            }
            WaitHandle.WaitAll(waitHandles);
            foreach (ForPartAggregation<TState> localforPart in forParts)
            {
                aggregation(localforPart.Value);
            }
        }

        public void ForExecution<T>(int from, int to, int step, Action<T> action, T value)
        {            
            chunk = (to - from) / processorCount;
            int start = from;
            int end = chunk;

            for (int i = 1; i <= processorCount; i++)
            {
                int x = i - 1;
                waitHandles[x] = new ManualResetEvent(false);
                ForPart<T> forPart = new ForPart<T>(start, end, step);
                forPart.ExecutionPart = action;
                forPart.Value = value;
                ConstantForSynchronisationContainer forSynchronisationContainer =
                    new ConstantForSynchronisationContainer((ManualResetEvent)waitHandles[x], forPart);
                ThreadPool.QueueUserWorkItem(
                    delegate(object state)
                    {
                        ConstantForSynchronisationContainer<T> localforSynchronisationContainer = (ConstantForSynchronisationContainer<T>)state;
                        ForPart<T> localforPart = localforSynchronisationContainer.ForPart_;
                        try
                        {
                            for (int ii = localforPart.From; ii <= localforPart.To; ii = ii + localforPart.Step)
                            {
                                localforPart.ExecutionPart.Invoke(ref ii, localforPart.Value);
                            }
                        }
                        finally
                        {
                            localforSynchronisationContainer.ManualResetEvent_.Set();
                        }
                    }, forSynchronisationContainer);
                start = end + 1;
                end = end + chunk;
            }
            WaitHandle.WaitAll(waitHandles);
        }

        public void ForExecution<T>(int from, int to, int step, Action<T> action, ExecutionConstancyEnum executionConstancy, T value)
        {
            if (executionConstancy == ExecutionConstancyEnum.Constant)
            {
                ForExecution<T>(from, to, step, action, value);
            }
            else
            {                
                ParallelQueue<ForPart<T>>[] parallelQueues = new ParallelQueue<ForPart<T>>[processorCount];
                chunk = (to - from) / processorCount;
                int start = from;
                int end = chunk;

                for (int i = 1; i <= processorCount; i++)
                {
                    int x = i - 1;
                    waitHandles[x] = new ManualResetEvent(false);
                    ParallelQueueFiller parallelQueueFiller = new ParallelQueueFiller();
                    ParallelQueue<ForPart<T>> parallelQueue = parallelQueueFiller.FillWithForPart<T>(start, end, step, action, value);
                    parallelQueues[x] = parallelQueue;
                    UnconstantForSynchronisationContainer<T> unconstantForSynchronisationContainer =
                        new UnconstantForSynchronisationContainer<T>((ManualResetEvent)waitHandles[x], parallelQueue, parallelQueues);
                    ThreadPool.QueueUserWorkItem(
                    delegate(object state)
                    {
                        UnconstantForSynchronisationContainer<T> localUnconstantForSynchronisationContainer =
                            (UnconstantForSynchronisationContainer<T>)state;
                        ParallelQueue<ForPart<T>> localparallelQueue = localUnconstantForSynchronisationContainer.ParallelQueue;
                        ParallelQueue<ForPart<T>>[] localparallelQueues = localUnconstantForSynchronisationContainer.ParallelQueues;
                        try
                        {
                            bool localQueueIsEmpty = false;                            
                            while (!localQueueIsEmpty)
                            {
                                ForPart<T> forPart = localparallelQueue.Dequeue();
                                if (forPart != null)
                                {
                                    int ii = forPart.From;
                                    forPart.ExecutionPart.Invoke(ref ii, forPart.Value);
                                }
                                else
                                {
                                    localQueueIsEmpty = true;
                                }
                            }
                            foreach (ParallelQueue<ForPart<T>> localForegnParallelQueue in localparallelQueues)
                            {
                                if (localForegnParallelQueue != localparallelQueue)
                                {
                                    bool localForegnQueueIsEmpty = false;
                                    while (!localForegnQueueIsEmpty)
                                    {
                                        ForPart<T> forPart = localForegnParallelQueue.Steal();
                                        if (forPart != null)
                                        {
                                            int ii = forPart.From;
                                            forPart.ExecutionPart.Invoke(ref ii, forPart.Value);
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
                            localUnconstantForSynchronisationContainer.ManualResetEvent_.Set();
                        }
                    }, unconstantForSynchronisationContainer);
                    start = end + 1;
                    end = end + chunk;
                }
                WaitHandle.WaitAll(waitHandles);
            }
        }

        public void ForExecution<T1, T2>(int from, int to, int step, Action<T1, T2> action, T1 value1, T2 value2)
        {            
            chunk = (to - from) / processorCount;
            int start = from;
            int end = chunk;

            for (int i = 1; i <= processorCount; i++)
            {
                int x = i - 1;
                waitHandles[x] = new ManualResetEvent(false);
                ForPart<T1, T2> forPart = new ForPart<T1, T2>(start, end, step);
                forPart.ExecutionPart = action;
                forPart.Value1 = value1;
                forPart.Value2 = value2;
                ConstantForSynchronisationContainer forSynchronisationContainer =
                    new ConstantForSynchronisationContainer((ManualResetEvent)waitHandles[x], forPart);
                ThreadPool.QueueUserWorkItem(
                    delegate(object state)
                    {
                        ConstantForSynchronisationContainer<T1, T2> localforSynchronisationContainer = (ConstantForSynchronisationContainer<T1, T2>)state;
                        ForPart<T1, T2> localforPart = localforSynchronisationContainer.ForPart_;
                        try
                        {
                            for (int ii = localforPart.From; ii <= localforPart.To; ii = ii + localforPart.Step)
                            {
                                localforPart.ExecutionPart.Invoke(ref ii, localforPart.Value1, localforPart.Value2);
                            }
                        }
                        finally
                        {
                            localforSynchronisationContainer.ManualResetEvent_.Set();
                        }
                    }, forSynchronisationContainer);
                start = end + 1;
                end = end + chunk;
            }
            WaitHandle.WaitAll(waitHandles);
        }

        public void ForExecution<T1, T2>(int from, int to, int step, Action<T1, T2> action, ExecutionConstancyEnum executionConstancy, T1 value1, T2 value2)
        {
            if (executionConstancy == ExecutionConstancyEnum.Constant)
            {
                ForExecution<T1, T2>(from, to, step, action, value1, value2);
            }
            else
            {                
                ParallelQueue<ForPart<T1, T2>>[] parallelQueues = new ParallelQueue<ForPart<T1, T2>>[processorCount];
                chunk = (to - from) / processorCount;
                int start = from;
                int end = chunk;

                for (int i = 1; i <= processorCount; i++)
                {
                    int x = i - 1;
                    waitHandles[x] = new ManualResetEvent(false);
                    ParallelQueueFiller parallelQueueFiller = new ParallelQueueFiller();
                    ParallelQueue<ForPart<T1, T2>> parallelQueue = parallelQueueFiller.FillWithForPart<T1, T2>(start, end, step, action, value1, value2);
                    parallelQueues[x] = parallelQueue;
                    UnconstantForSynchronisationContainer<T1, T2> unconstantForSynchronisationContainer =
                        new UnconstantForSynchronisationContainer<T1, T2>((ManualResetEvent)waitHandles[x], parallelQueue, parallelQueues);
                    ThreadPool.QueueUserWorkItem(
                    delegate(object state)
                    {
                        UnconstantForSynchronisationContainer<T1, T2> localUnconstantForSynchronisationContainer =
                            (UnconstantForSynchronisationContainer<T1, T2>)state;
                        ParallelQueue<ForPart<T1, T2>> localparallelQueue = localUnconstantForSynchronisationContainer.ParallelQueue;
                        ParallelQueue<ForPart<T1, T2>>[] localparallelQueues = localUnconstantForSynchronisationContainer.ParallelQueues;
                        try
                        {
                            bool localQueueIsEmpty = false;
                            while (!localQueueIsEmpty)
                            {
                                ForPart<T1, T2> forPart = localparallelQueue.Dequeue();
                                if (forPart != null)
                                {
                                    int ii = forPart.From;
                                    forPart.ExecutionPart.Invoke(ref ii, forPart.Value1, forPart.Value2);
                                }
                                else
                                {
                                    localQueueIsEmpty = true;
                                }
                            }
                            foreach (ParallelQueue<ForPart<T1, T2>> localForegnParallelQueue in localparallelQueues)
                            {
                                if (localForegnParallelQueue != localparallelQueue)
                                {
                                    bool localForegnQueueIsEmpty = false;
                                    while (!localForegnQueueIsEmpty)
                                    {
                                        ForPart<T1, T2> forPart = localForegnParallelQueue.Steal();
                                        if (forPart != null)
                                        {
                                            int ii = forPart.From;
                                            forPart.ExecutionPart.Invoke(ref ii, forPart.Value1, forPart.Value2);
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
                            localUnconstantForSynchronisationContainer.ManualResetEvent_.Set();
                        }
                    }, unconstantForSynchronisationContainer);
                    start = end + 1;
                    end = end + chunk;
                }
                WaitHandle.WaitAll(waitHandles);
            }
        }    
    }
}
