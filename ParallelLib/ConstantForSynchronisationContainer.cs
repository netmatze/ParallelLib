using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Intact.ParallelLib
{
    internal class ConstantForSynchronisationContainer : SynchronisationContainer
    {
        private ForPart forPart_;

        public ForPart ForPart_
        {
            get { return forPart_; }
            set { forPart_ = value; }
        }

        #region public ForSynchronisationContainer()

        public ConstantForSynchronisationContainer(ManualResetEvent manualResetEvent)
            : base(manualResetEvent)
        {            
        }

        public ConstantForSynchronisationContainer(ManualResetEvent manualResetEvent, ForPart forPart) : base(manualResetEvent)
        {
            this.forPart_ = forPart;
        }

        #endregion
    }

    internal class ConstantForSynchronisationContainer<T> : ConstantForSynchronisationContainer
    {
        private ForPart<T> forPart_;

        public new ForPart<T> ForPart_
        {
            get { return forPart_; }
            set { forPart_ = value; }
        }

        #region public ForSynchronisationContainer<T>()

        public ConstantForSynchronisationContainer(ManualResetEvent manualResetEvent, ForPart<T> forPart)
            : base(manualResetEvent)
        {
            this.forPart_ = forPart;
        }

        #endregion
    }

    internal class ConstantForAggregationSynchronisationContainer<T> : ConstantForSynchronisationContainer
    {
        private ForPartAggregation<T> forPart_;

        public new ForPartAggregation<T> ForPart_
        {
            get { return forPart_; }
            set { forPart_ = value; }
        }

        #region public ConstantForAggregationSynchronisationContainer<T>()

        public ConstantForAggregationSynchronisationContainer(ManualResetEvent manualResetEvent, ForPartAggregation<T> forPart)
            : base(manualResetEvent)
        {
            this.forPart_ = forPart;
        }

        #endregion
    }

    internal class ConstantForAggregationSynchronisationContainer<T, T1> : ConstantForSynchronisationContainer
    {
        private ForPartAggregation<T, T1> forPart_;

        public new ForPartAggregation<T, T1> ForPart_
        {
            get { return forPart_; }
            set { forPart_ = value; }
        }

        #region public ConstantForAggregationSynchronisationContainer<T, T1>()

        public ConstantForAggregationSynchronisationContainer(ManualResetEvent manualResetEvent, ForPartAggregation<T, T1> forPart)
            : base(manualResetEvent)
        {
            this.forPart_ = forPart;
        }

        #endregion
    }

    internal class ConstantForSynchronisationContainer<T1, T2> : ConstantForSynchronisationContainer
    {
        private ForPart<T1, T2> forPart_;

        public new ForPart<T1, T2> ForPart_
        {
            get { return forPart_; }
            set { forPart_ = value; }
        }

        #region public ForSynchronisationContainer<T>()

        public ConstantForSynchronisationContainer(ManualResetEvent manualResetEvent, ForPart<T1, T2> forPart)
            : base(manualResetEvent)
        {
            this.forPart_ = forPart;
        }

        #endregion
    }
}
