using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Intact.ParallelLib
{
    internal class UnconstantForSynchronisationContainer : SynchronisationContainer
    {
        private ParallelQueue<ForPart> parallelQueue;

        public ParallelQueue<ForPart> ParallelQueue
        {
            get { return parallelQueue; }
            set { parallelQueue = value; }
        }
        private ParallelQueue<ForPart>[] parallelQueues;

        public ParallelQueue<ForPart>[] ParallelQueues
        {
            get { return parallelQueues; }
            set { parallelQueues = value; }
        }

        internal UnconstantForSynchronisationContainer(ManualResetEvent manualResetEvent, 
            ParallelQueue<ForPart> parallelQueue, ParallelQueue<ForPart>[] parallelQueues) 
            : base(manualResetEvent)
        {
            this.parallelQueue = parallelQueue;
            this.parallelQueues = parallelQueues;
        }

        internal UnconstantForSynchronisationContainer(ManualResetEvent manualResetEvent)
            : base(manualResetEvent)
        {
        }
    }

    internal class UnconstantForSynchronisationContainer<T> : UnconstantForSynchronisationContainer
    {
        private ParallelQueue<ForPart<T>> parallelQueue;

        public new ParallelQueue<ForPart<T>> ParallelQueue
        {
            get { return parallelQueue; }
            set { parallelQueue = value; }
        }

        private ParallelQueue<ForPart<T>>[] parallelQueues;

        public new ParallelQueue<ForPart<T>>[] ParallelQueues
        {
            get { return parallelQueues; }
            set { parallelQueues = value; }
        }

        internal UnconstantForSynchronisationContainer(ManualResetEvent manualResetEvent,
            ParallelQueue<ForPart<T>> parallelQueue, ParallelQueue<ForPart<T>>[] parallelQueues)
            : base(manualResetEvent)
        {           
            this.parallelQueue = parallelQueue;
            this.parallelQueues = parallelQueues;
        }        
    }

    internal class UnconstantForSynchronisationContainer<T, T1> : UnconstantForSynchronisationContainer
    {
        private ParallelQueue<ForPart<T, T1>> parallelQueue;

        public new ParallelQueue<ForPart<T, T1>> ParallelQueue
        {
            get { return parallelQueue; }
            set { parallelQueue = value; }
        }

        private ParallelQueue<ForPart<T, T1>>[] parallelQueues;

        public new ParallelQueue<ForPart<T, T1>>[] ParallelQueues
        {
            get { return parallelQueues; }
            set { parallelQueues = value; }
        }

        internal UnconstantForSynchronisationContainer(ManualResetEvent manualResetEvent,
            ParallelQueue<ForPart<T, T1>> parallelQueue, ParallelQueue<ForPart<T, T1>>[] parallelQueues)
            : base(manualResetEvent)
        {
            this.parallelQueue = parallelQueue;
            this.parallelQueues = parallelQueues;
        }
    }
}
