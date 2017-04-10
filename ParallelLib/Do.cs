using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Intact.ParallelLib
{
    internal class Do : ParallelConstruct
    {
        private static WaitHandle[] waitHandles;        

        #region public Do()
       
        public Do()
        {
            ThreadPool.SetMaxThreads(processorCount, processorCount);
            waitHandles = new WaitHandle[processorCount];
        }

        #endregion

        public void DoExecution(Constraint<int> constraint, ConstraintAction constraintAction, Action action)
        {
            int counter = 0;
            do
            {
                constraintAction.Invoke(ref counter);
            } while (constraint.Invoke(counter));
            chunk = counter / processorCount;
            int start = 0;
            int end = chunk;

            for (int i = 1; i <= processorCount; i++)
            {
                int x = i - 1;
                waitHandles[x] = new ManualResetEvent(false);
                DoPart doPart = new DoPart();
                doPart.ExecutionPart = action;
                doPart.Constraint = constraint;
                doPart.Start = start;
                doPart.End = end;                
                DoSynchronisationContainer doSynchronisationContainer =
                    new DoSynchronisationContainer((ManualResetEvent)waitHandles[x], doPart);
                ThreadPool.QueueUserWorkItem(
                    delegate(object state)
                    {
                        DoSynchronisationContainer doSynchronisationContainerLocal = (DoSynchronisationContainer)state;
                        DoPart doPartLocal = doSynchronisationContainerLocal.DoPart_;
                        try
                        {
                            int intCounter = doPartLocal.Start;
                            do
                            {
                                doPartLocal.ExecutionPart.Invoke(ref intCounter);
                            }
                            while (doPartLocal.Constraint.Invoke(intCounter) && (intCounter <= doPartLocal.End && intCounter >= doPartLocal.Start));
                        }
                        finally
                        {
                            doSynchronisationContainerLocal.ManualResetEvent_.Set();
                        }
                    }
                    , doSynchronisationContainer);
                start = end + 1;
                end = end + chunk;
            }
            WaitHandle.WaitAll(waitHandles);
        }

        public void DoExecution<T>(Constraint<int> constraint, ConstraintAction constraintAction, Action<T> action, T value)
        {            
            int counter = 0;    
            do
            {
                constraintAction.Invoke(ref counter);
            } while (constraint.Invoke(counter));
            chunk = counter / processorCount;
            int start = 0;
            int end = chunk;

            for (int i = 1; i <= processorCount; i++)
            {
                int x = i - 1;
                waitHandles[x] = new ManualResetEvent(false);
                DoPart<T> doPart = new DoPart<T>();
                doPart.ExecutionPart = action;     
                doPart.Constraint = constraint;
                doPart.Start = start;
                doPart.End = end;
                doPart.Value = value;
                DoSynchronisationContainer<T> doSynchronisationContainer =
                    new DoSynchronisationContainer<T>((ManualResetEvent)waitHandles[x], doPart);
                ThreadPool.QueueUserWorkItem(
                    delegate(object state)
                    {
                        DoSynchronisationContainer<T> doSynchronisationContainerLocal = (DoSynchronisationContainer<T>)state;
                        DoPart<T> doPartLocal = doSynchronisationContainerLocal.DoPart_;
                        try
                        {
                            int intCounter = doPartLocal.Start;
                            do
                            {
                                doPartLocal.ExecutionPart.Invoke(ref intCounter, doPartLocal.Value);
                            }
                            while (doPartLocal.Constraint.Invoke(intCounter) && (intCounter <= doPartLocal.End && intCounter >= doPartLocal.Start));
                        }
                        finally
                        {
                            doSynchronisationContainerLocal.ManualResetEvent_.Set();
                        }
                    }
                    , doSynchronisationContainer);
                start = end + 1;
                end = end + chunk;
            }
            WaitHandle.WaitAll(waitHandles);
        }

        public void DoExecution<T1, T2>(Constraint<int> constraint, ConstraintAction constraintAction, Action<T1, T2> action, T1 value1, T2 value2)
        {            
            int counter = 0;
            do
            {
                constraintAction.Invoke(ref counter);
            } while (constraint.Invoke(counter));
            chunk = counter / processorCount;
            int start = 0;
            int end = chunk;

            for (int i = 1; i <= processorCount; i++)
            {
                int x = i - 1;
                waitHandles[x] = new ManualResetEvent(false);
                DoPart<T1, T2> doPart = new DoPart<T1, T2>();
                doPart.ExecutionPart = action;
                doPart.Constraint = constraint;
                doPart.Start = start;
                doPart.End = end;
                doPart.Value1 = value1;
                doPart.Value2 = value2;
                DoSynchronisationContainer<T1, T2> doSynchronisationContainer =
                    new DoSynchronisationContainer<T1, T2>((ManualResetEvent)waitHandles[x], doPart);
                ThreadPool.QueueUserWorkItem(
                    delegate(object state)
                    {
                        DoSynchronisationContainer<T1, T2> doSynchronisationContainerLocal = (DoSynchronisationContainer<T1, T2>)state;
                        DoPart<T1, T2> doPartLocal = doSynchronisationContainerLocal.DoPart_;
                        try
                        {
                            int intCounter = doPartLocal.Start;
                            do
                            {
                                doPartLocal.ExecutionPart.Invoke(ref intCounter, doPartLocal.Value1, doPartLocal.Value2);
                            }
                            while (doPartLocal.Constraint.Invoke(intCounter) && (intCounter <= doPartLocal.End && intCounter >= doPartLocal.Start));
                        }
                        finally
                        {
                            doSynchronisationContainerLocal.ManualResetEvent_.Set();
                        }
                    }
                    , doSynchronisationContainer);
                start = end + 1;
                end = end + chunk;
            }
            WaitHandle.WaitAll(waitHandles);
        }        
    }
}
