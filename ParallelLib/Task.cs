using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Intact.ParallelLib
{
    public class Task
    {
        protected static int processorCount;
        protected static int chunk;        
        //protected ManualResetEvent manualResetEvent = new ManualResetEvent(false);
        protected WaitHandle[] waitHandles;
        protected bool[] isCompletedAll;
        private Func func;

        protected bool isCanceled;

        public bool IsCanceled
        {
            get { return isCanceled; }
        }        

        public bool IsCompleted
        {
            get 
            {
                foreach (bool isCompleted in isCompletedAll)
                {
                    if (!isCompleted)
                    {
                        return false;
                    }
                }
                return true; 
            }
        }

        public void Wait()
        {            
            WaitHandle.WaitAll(waitHandles);
        }

        protected Task()
        {
            processorCount = Environment.ProcessorCount;        
        }

        protected Task(Func func)
        {
            this.func = func;   
        }

        public static Task Create(Func func)
        {
            Task task = new Task(func);            
            task.Execute();
            return task;
        }

        protected virtual void Execute()
        {
            //manualResetEvent.Reset();
            //isCompleted = false;
            CodeAnalyser codeAnalyser = new CodeAnalyser();
            codeAnalyser.Analyse(this.func);
            ThreadPool.QueueUserWorkItem(ExecuteWork, func);            
        }

        protected virtual void ExecuteWork(object state)
        {
            //try
            //{
            //    func = (Func<T>)state;
            //    value = func.Invoke();
            //    isCompleted = true;
            //}
            //finally
            //{
            //    manualResetEvent.Set();
            //}
        }

    }
}
