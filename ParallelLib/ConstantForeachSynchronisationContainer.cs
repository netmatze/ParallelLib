using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Intact.ParallelLib
{
    internal class ConstantForeachSynchronisationContainer<T> : SynchronisationContainer
    {
        private ForeachPart<T> forEeachPart_;

        public ForeachPart<T> ForEachPart_
        {
            get { return forEeachPart_; }
            set { forEeachPart_ = value; }
        }

        #region public ForSynchronisationContainer()

        public ConstantForeachSynchronisationContainer(ManualResetEvent manualResetEvent)
            : base(manualResetEvent)
        {            
        }

        public ConstantForeachSynchronisationContainer(ManualResetEvent manualResetEvent, ForeachPart<T> forEeachPart)
            : base(manualResetEvent)
        {
            this.forEeachPart_ = forEeachPart;
        }

        #endregion
    }

    internal class ConstantForeachSynchronisationContainer<T, T1> : ConstantForeachSynchronisationContainer<T>
    {
        private ForeachPart<T, T1> forEeachPart_;

        public new ForeachPart<T, T1> ForEachPart_
        {
            get { return forEeachPart_; }
            set { forEeachPart_ = value; }
        }

        #region public ForSynchronisationContainer()

        public ConstantForeachSynchronisationContainer(ManualResetEvent manualResetEvent, ForeachPart<T, T1> forEeachPart)
            : base(manualResetEvent)
        {
            this.forEeachPart_ = forEeachPart;
        }

        #endregion
    }

    internal class ConstantForeachSynchronisationContainer<T, T1, T2> : ConstantForeachSynchronisationContainer<T>
    {
        private ForeachPart<T, T1, T2> forEeachPart_;

        public new ForeachPart<T, T1, T2> ForEachPart_
        {
            get { return forEeachPart_; }
            set { forEeachPart_ = value; }
        }

        #region public ForSynchronisationContainer()

        public ConstantForeachSynchronisationContainer(ManualResetEvent manualResetEvent, ForeachPart<T, T1, T2> forEeachPart)
            : base(manualResetEvent)
        {
            this.forEeachPart_ = forEeachPart;
        }

        #endregion
    }    
}
