using System;
using System.Collections.Generic;
using System.Text;

namespace Intact.ParallelLib
{   
    public class ThreadLocalState<T>
    {
        public ThreadLocalState(T value)
        {
            this.value = value;
        }

        private T value;

        public T Value
        {
            get { return this.value; }
            set { this.value = value; }
        }
    }    
}
