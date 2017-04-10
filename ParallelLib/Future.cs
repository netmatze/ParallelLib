using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Intact.ParallelLib
{
    public class Future<T> : IAsyncResult, IDisposable
    {
        protected ManualResetEvent manualResetEvent = new ManualResetEvent(false);
        private Func<T> func;
        protected T value;

        public T Value
        {
            get 
            {
                manualResetEvent.WaitOne();
                return this.value; 
            }            
        }

        protected bool isCanceled;

        public bool IsCanceled
        {
            get { return isCanceled; }
        }

        protected bool isCompleted;

        public bool IsCompleted
        {
            get { return isCompleted; }
        }

        public void Wait()
        {
            manualResetEvent.WaitOne();
        }

        protected Future()
        {

        }

        protected Future(Func<T> func)
        {
            this.func = func;
        }

        public static Future<T> Create(Func<T> func)
        {
            Future<T> future = new Future<T>(func);
            future.Execute();
            return future;
        }

        protected virtual void Execute()
        {
            manualResetEvent.Reset();
            isCompleted = false;
            ThreadPool.QueueUserWorkItem(ExecuteWork, func);            
        }

        protected virtual void ExecuteWork(object state)
        {
            try
            {
                func = (Func<T>)state;
                value = func.Invoke();
                isCompleted = true;
            }
            finally
            {
                manualResetEvent.Set();
            }
        }

        #region IDisposable Member

        public void Dispose()
        {
            
        }

        #endregion

        #region IAsyncResult Member

        public object AsyncState
        {
            get { return null; }
        }

        public System.Threading.WaitHandle AsyncWaitHandle
        {
            get { return manualResetEvent; }
        }

        public bool CompletedSynchronously
        {
            get { return isCompleted; }
        }

        #endregion
    }

    public class Future<T, TResult> : Future<TResult>
    {
        private Func<T, TResult> func;

        protected Future(Func<T, TResult> func) : base()
        {
            this.func = func;
        }

        public static Future<T, TResult> Create(Func<T, TResult> func, T value)
        {
            Future<T, TResult> future = new Future<T, TResult>(func);
            future.Execute(value);
            return future;
        }

        protected virtual void Execute(T value)
        {
            manualResetEvent.Reset();
            isCompleted = false;
            ExecuteWorkObject executionWorkObject = new ExecuteWorkObject(func, value);
            ThreadPool.QueueUserWorkItem(ExecuteWork, executionWorkObject);
        }

        protected override void ExecuteWork(object state)
        {
            try
            {
                ExecuteWorkObject executionWorkObject = (ExecuteWorkObject)state;
                value = executionWorkObject.Func.Invoke(executionWorkObject.Value);
                isCompleted = true;
            }
            finally
            {
                manualResetEvent.Set();
            }
        }

        #region ExecuteWorkObject

        private class ExecuteWorkObject
        {
            private Func<T, TResult> func;

            public Func<T, TResult> Func
            {
                get { return func; }
                set { func = value; }
            }

            private T value;

            public T Value
            {
                get { return this.value; }
                set { this.value = value; }
            }

            #region public ExecuteWorkObject()
            
            public ExecuteWorkObject(Func<T, TResult> func, T value)
            {
                this.func = func;
                this.value = value;
            }

            #endregion
        }

        #endregion

    }

    public class Future<T1, T2, TResult> : Future<TResult>
    {
        private Func<T1, T2, TResult> func;

        protected Future(Func<T1, T2, TResult> func)
            : base()
        {
            this.func = func;
        }

        public static Future<T1, T2, TResult> Create(Func<T1, T2, TResult> func, T1 value1, T2 value2)
        {
            Future<T1, T2, TResult> future = new Future<T1, T2, TResult>(func);
            future.Execute(value1, value2);
            return future;
        }

        protected virtual void Execute(T1 value1, T2 value2)
        {
            manualResetEvent.Reset();
            isCompleted = false;
            ExecuteWorkObject executionWorkObject = new ExecuteWorkObject(func, value1, value2);
            ThreadPool.QueueUserWorkItem(ExecuteWork, executionWorkObject);
        }

        protected override void ExecuteWork(object state)
        {
            try
            {
                ExecuteWorkObject executionWorkObject = (ExecuteWorkObject)state;
                value = executionWorkObject.Func.Invoke(executionWorkObject.Value1, executionWorkObject.Value2);
                isCompleted = true;
            }
            finally
            {
                manualResetEvent.Set();
            }
        }

        #region ExecuteWorkObject

        private class ExecuteWorkObject
        {
            private Func<T1, T2, TResult> func;

            public Func<T1, T2, TResult> Func
            {
                get { return func; }
                set { func = value; }
            }

            private T1 value1;

            public T1 Value1
            {
                get { return this.value1; }
                set { this.value1 = value; }
            }

            private T2 value2;

            public T2 Value2
            {
                get { return this.value2; }
                set { this.value2 = value; }
            }

            #region public ExecuteWorkObject()

            public ExecuteWorkObject(Func<T1, T2, TResult> func, T1 value1, T2 value2)
            {
                this.func = func;
                this.value1 = value1;
                this.value2 = value2;
            }

            #endregion
        }

        #endregion

    }

    public class Future<T1, T2, T3, TResult> : Future<TResult>
    {
        private Func<T1, T2, T3, TResult> func;

        protected Future(Func<T1, T2, T3, TResult> func)
            : base()
        {
            this.func = func;
        }

        public static Future<T1, T2, T3, TResult> Create(Func<T1, T2, T3, TResult> func, T1 value1, T2 value2, T3 value3)
        {
            Future<T1, T2, T3, TResult> future = new Future<T1, T2, T3, TResult>(func);
            future.Execute(value1, value2, value3);
            return future;
        }

        protected virtual void Execute(T1 value1, T2 value2, T3 value3)
        {
            manualResetEvent.Reset();
            isCompleted = false;
            ExecuteWorkObject executionWorkObject = new ExecuteWorkObject(func, value1, value2, value3);
            ThreadPool.QueueUserWorkItem(ExecuteWork, executionWorkObject);
        }

        protected override void ExecuteWork(object state)
        {
            try
            {
                ExecuteWorkObject executionWorkObject = (ExecuteWorkObject)state;
                value = executionWorkObject.Func.Invoke(executionWorkObject.Value1, executionWorkObject.Value2, executionWorkObject.Value3);
                isCompleted = true;
            }
            finally
            {
                manualResetEvent.Set();
            }
        }

        #region ExecuteWorkObject

        private class ExecuteWorkObject
        {
            private Func<T1, T2, T3, TResult> func;

            public Func<T1, T2, T3, TResult> Func
            {
                get { return func; }
                set { func = value; }
            }

            private T1 value1;

            public T1 Value1
            {
                get { return this.value1; }
                set { this.value1 = value; }
            }

            private T2 value2;

            public T2 Value2
            {
                get { return this.value2; }
                set { this.value2 = value; }
            }

            private T3 value3;

            public T3 Value3
            {
                get { return this.value3; }
                set { this.value3 = value; }
            }

            #region public ExecuteWorkObject()

            public ExecuteWorkObject(Func<T1, T2, T3, TResult> func, T1 value1, T2 value2, T3 value3)
            {
                this.func = func;
                this.value1 = value1;
                this.value2 = value2;
                this.value3 = value3;
            }

            #endregion
        }

        #endregion

    }
}
