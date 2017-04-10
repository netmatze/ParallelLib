using System;
using System.Collections.Generic;
using System.Text;

namespace Intact.ParallelLib
{    
    internal class ForeachPart<T> : Part
    {
        protected IEnumerable<T> source;

        public IEnumerable<T> Source
        {
            get { return source; }
            set { source = value; }
        }

        private Action<T> executionPart;

        public Action<T> ExecutionPart
        {
            get { return executionPart; }
            set { executionPart = value; }
        }

        protected T singleSource;

        public T SingleSource
        {
            get { return singleSource; }
            set { singleSource = value; }
        }
    }

    internal class ForeachPart<T, T1> : ForeachPart<T>
    {        
        private Action<T, T1> executionPart;

        public new Action<T, T1> ExecutionPart
        {
            get { return executionPart; }
            set { executionPart = value; }
        }

        private T1 value1;

        public T1 Value1
        {
            get { return value1; }
            set { value1 = value; }
        }
    }

    internal class ForeachPart<T, T1, T2> : ForeachPart<T>
    {        
        private Action<T, T1, T2> executionPart;

        public new Action<T, T1,T2> ExecutionPart
        {
            get { return executionPart; }
            set { executionPart = value; }
        }

        private T1 value1;

        public T1 Value1
        {
            get { return value1; }
            set { value1 = value; }
        }

        private T2 value2;

        public T2 Value2
        {
            get { return value2; }
            set { value2 = value; }
        }
    }
}
