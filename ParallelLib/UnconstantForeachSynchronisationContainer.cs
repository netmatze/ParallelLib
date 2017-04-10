using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Intact.ParallelLib
{
    internal class UnconstantForeachSynchronisationContainer<T> : SynchronisationContainer
    {
        private ParallelQueue<ForeachPart<T>> parallelQueue;

        public ParallelQueue<ForeachPart<T>> ParallelQueue
        {
            get { return parallelQueue; }
            set { parallelQueue = value; }
        }
        private ParallelQueue<ForeachPart<T>>[] parallelQueues;

        public ParallelQueue<ForeachPart<T>>[] ParallelQueues
        {
            get { return parallelQueues; }
            set { parallelQueues = value; }
        }

        internal UnconstantForeachSynchronisationContainer(ManualResetEvent manualResetEvent,
            ParallelQueue<ForeachPart<T>> parallelQueue, ParallelQueue<ForeachPart<T>>[] parallelQueues) 
            : base(manualResetEvent)
        {
            this.parallelQueue = parallelQueue;
            this.parallelQueues = parallelQueues;
        }

        internal UnconstantForeachSynchronisationContainer(ManualResetEvent manualResetEvent)
            : base(manualResetEvent)
        {
        }
    }

    internal class UnconstantForeachSynchronisationContainer<T, T1> : UnconstantForeachSynchronisationContainer<T>
    {
        private ParallelQueue<ForeachPart<T, T1>> parallelQueue;

        public new ParallelQueue<ForeachPart<T, T1>> ParallelQueue
        {
            get { return parallelQueue; }
            set { parallelQueue = value; }
        }
        private ParallelQueue<ForeachPart<T, T1>>[] parallelQueues;

        public new ParallelQueue<ForeachPart<T, T1>>[] ParallelQueues
        {
            get { return parallelQueues; }
            set { parallelQueues = value; }
        }

        internal UnconstantForeachSynchronisationContainer(ManualResetEvent manualResetEvent,
            ParallelQueue<ForeachPart<T, T1>> parallelQueue, ParallelQueue<ForeachPart<T, T1>>[] parallelQueues)
            : base(manualResetEvent)
        {
            this.parallelQueue = parallelQueue;
            this.parallelQueues = parallelQueues;
        }

        internal UnconstantForeachSynchronisationContainer(ManualResetEvent manualResetEvent)
            : base(manualResetEvent)
        {
        }
    }

    internal class UnconstantForeachSynchronisationContainer<T, T1, T2> : UnconstantForeachSynchronisationContainer<T>
    {
        private ParallelQueue<ForeachPart<T, T1, T2>> parallelQueue;

        public new ParallelQueue<ForeachPart<T, T1, T2>> ParallelQueue
        {
            get { return parallelQueue; }
            set { parallelQueue = value; }
        }
        private ParallelQueue<ForeachPart<T, T1, T2>>[] parallelQueues;

        public new ParallelQueue<ForeachPart<T, T1, T2>>[] ParallelQueues
        {
            get { return parallelQueues; }
            set { parallelQueues = value; }
        }

        internal UnconstantForeachSynchronisationContainer(ManualResetEvent manualResetEvent,
            ParallelQueue<ForeachPart<T, T1, T2>> parallelQueue, ParallelQueue<ForeachPart<T, T1, T2>>[] parallelQueues)
            : base(manualResetEvent)
        {
            this.parallelQueue = parallelQueue;
            this.parallelQueues = parallelQueues;
        }

        internal UnconstantForeachSynchronisationContainer(ManualResetEvent manualResetEvent)
            : base(manualResetEvent)
        {
        }
    }
}
