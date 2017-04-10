using System;
using System.Collections.Generic;
using System.Text;

namespace Intact.ParallelLib
{
    internal class ForPart : Part
    {
        public ForPart(int from, int to)
        {
            this.from = from;
            this.to = to;
        }

        public ForPart(int from, int to, int step) : this(from, to)
        {
            this.step = step;
        }

        private int from;

        public int From
        {
            get { return from; }
            set { from = value; }
        }
        private int to;

        public int To
        {
            get { return to; }
            set { to = value; }
        }

        private int step;

        public int Step
        {
            get { return step; }
            set { step = value; }
        }

        private Action executionPart;

        public virtual Action ExecutionPart
        {
            get { return executionPart; }
            set { executionPart = value; }
        }
    }

    internal class ForPart<T> : ForPart
    {
        public ForPart(int from, int to) : base(from, to)
        {            
        }

        public ForPart(int from, int to, int step)
            : base(from, to, step)
        {            
        }

        private T value;

        public T Value
        {
            get { return this.value; }
            set { this.value = value; }
        }        

        private Action<T> executionPart;

        public new Action<T> ExecutionPart
        {
            get
            {
                return executionPart;
            }
            set
            {
                executionPart = value;
            }
        }
    }

    internal class ForPartAggregation<T> : ForPart
    {
        public ForPartAggregation(int from, int to)
            : base(from, to)
        {
        }

        public ForPartAggregation(int from, int to, int step)
            : base(from, to, step)
        {
        }

        private ThreadLocalState<T> value;

        public ThreadLocalState<T> Value
        {
            get { return this.value; }
            set { this.value = value; }
        }

        private Action<ThreadLocalState<T>> executionPart;

        public new Action<ThreadLocalState<T>> ExecutionPart
        {
            get
            {
                return executionPart;
            }
            set
            {
                executionPart = value;
            }
        }
    }

    internal class ForPartAggregation<T, T1> : ForPart
    {
        public ForPartAggregation(int from, int to)
            : base(from, to)
        {
        }

        public ForPartAggregation(int from, int to, int step)
            : base(from, to, step)
        {
        }

        private ThreadLocalState<T> statevalue;

        public ThreadLocalState<T> Statevalue
        {
            get { return this.statevalue; }
            set { this.statevalue = value; }
        }

        private T1 value1;

        public T1 Value1
        {
            get { return value1; }
            set { value1 = value; }
        }

        private Action<T1, ThreadLocalState<T>> executionPart;

        public new Action<T1, ThreadLocalState<T>> ExecutionPart
        {
            get
            {
                return executionPart;
            }
            set
            {
                executionPart = value;
            }
        }
    }

    internal class ForPart<T1, T2> : ForPart
    {
        public ForPart(int from, int to)
            : base(from, to)
        {
        }

        public ForPart(int from, int to, int step)
            : base(from, to, step)
        {
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

        private Action<T1, T2> executionPart;

        public new Action<T1, T2> ExecutionPart
        {
            get
            {
                return executionPart;
            }
            set
            {
                executionPart = value;
            }
        }
    }
}
