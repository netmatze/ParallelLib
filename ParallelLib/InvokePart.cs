using System;
using System.Collections.Generic;
using System.Text;

namespace Intact.ParallelLib
{
    internal class InvokePart : Part
    {
        private List<InvokeAction> invokationParts;

        public virtual List<InvokeAction> InvokationParts
        {
            get { return invokationParts; }
            set { invokationParts = value; }
        }
    }

    internal class InvokePart<T> : InvokePart
    {
        private T value;

        public T Value
        {
            get { return this.value; }
            set { this.value = value; }
        }

        private List<InvokeAction<T>> invokationParts;

        public new List<InvokeAction<T>> InvokationParts
        {
            get
            {
                return invokationParts;
            }
            set
            {
                invokationParts = value;
            }
        }
    }

    internal class InvokePart<T1, T2> : InvokePart
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

        private List<InvokeAction<T1, T2>> invokationParts;

        public new List<InvokeAction<T1, T2>> InvokationParts
        {
            get
            {
                return invokationParts;
            }
            set
            {
                invokationParts = value;
            }
        }
    }
}
