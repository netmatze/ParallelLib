using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Intact.ParallelLib
{
    internal abstract class SynchronisationContainer
    {
        protected ManualResetEvent manualResetEvent_;

        public ManualResetEvent ManualResetEvent_
        {
            get { return manualResetEvent_; }
            set { manualResetEvent_ = value; }
        }

        #region public SynchronisationContainer()

        public SynchronisationContainer(ManualResetEvent manualResetEvent)
        {            
            this.manualResetEvent_ = manualResetEvent;                
        }

        #endregion
    }
}
