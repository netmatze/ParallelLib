using System;
using System.Collections.Generic;
using System.Text;

namespace Intact.ParallelLib
{
    internal class DoPart : Part
    {
        private Constraint<int> constraint;

        public Constraint<int> Constraint
        {
            get { return constraint; }
            set { constraint = value; }
        }

        private Action executionPart;

        public virtual Action ExecutionPart
        {
            get { return executionPart; }
            set { executionPart = value; }
        }
    }

    internal class DoPart<T> : DoPart
    {
        private T value;

        public T Value
        {
            get { return this.value; }
            set { this.value = value; }
        }        

        private Action<T> executionPart;

        public new Action<T> ExecutionPart
        {
            get { return executionPart; }
            set { executionPart = value; }
        }
    }

    internal class DoPart<T1, T2> : DoPart
    {
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
            get { return executionPart; }
            set { executionPart = value; }
        }
    }
}
