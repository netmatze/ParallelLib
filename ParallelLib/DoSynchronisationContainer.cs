using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Intact.ParallelLib
{
    internal class DoSynchronisationContainer : SynchronisationContainer
    {
        private DoPart doPart_;

        public DoPart DoPart_
        {
            get { return doPart_; }
            set { doPart_ = value; }
        }

        public DoSynchronisationContainer(ManualResetEvent manualResetEvent)
            : base(manualResetEvent)
        {

        }

        public DoSynchronisationContainer(ManualResetEvent manualResetEvent, DoPart doPart)
            : base(manualResetEvent)
        {
            this.doPart_ = doPart;
        }
    }

    internal class DoSynchronisationContainer<T> : DoSynchronisationContainer
    {
        private DoPart<T> doPart_;

        public new DoPart<T> DoPart_
        {
            get { return doPart_; }
            set { doPart_ = value; }
        }

        public DoSynchronisationContainer(ManualResetEvent manualResetEvent, DoPart<T> doPart)
            : base(manualResetEvent)
        {
            this.doPart_ = doPart;
        }
    }

    internal class DoSynchronisationContainer<T1, T2> : DoSynchronisationContainer
    {
        private DoPart<T1, T2> doPart_;

        public new DoPart<T1, T2> DoPart_
        {
            get { return doPart_; }
            set { doPart_ = value; }
        }

        public DoSynchronisationContainer(ManualResetEvent manualResetEvent, DoPart<T1, T2> doPart)
            : base(manualResetEvent)
        {
            this.doPart_ = doPart;
        }
    }
}
