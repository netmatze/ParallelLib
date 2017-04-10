using System;
using System.Collections.Generic;
using System.Text;

namespace Intact.ParallelLib
{
    internal abstract class Part
    {
        private int start;

        public int Start
        {
            get { return start; }
            set { start = value; }
        }

        private int end;

        public int End
        {
            get { return end; }
            set { end = value; }
        }
    }
}
