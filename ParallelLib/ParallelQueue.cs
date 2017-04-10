using System;
using System.Collections.Generic;
using System.Text;
using System.Collections;

namespace Intact.ParallelLib
{
    public class ParallelQueue<T> : CollectionBase, IEnumerable<T>, IEnumerable
    {
        private volatile object synchronisationObject = new object();

        #region IEnumerable<T> Member

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return (IEnumerator<T>) base.InnerList.GetEnumerator();
        }

        #endregion

        #region IEnumerable Member

        IEnumerator IEnumerable.GetEnumerator()
        {
            return base.InnerList.GetEnumerator();          
        }

        #endregion

        public void Enqueue(T item)
        {
            lock (synchronisationObject)
            {
                base.InnerList.Add(item);
            }
        }

        public T Dequeue()
        {
            lock (synchronisationObject)
            {
                if (base.InnerList.Count == 0)
                {
                    return default(T);
                }
                T item = (T)base.InnerList[base.InnerList.Count - 1];
                base.InnerList.RemoveAt(base.InnerList.Count - 1);
                return item;
            }
        }

        public T Steal()
        {
            lock (synchronisationObject)
            {
                if (base.InnerList.Count == 0)
                {
                    return default(T);
                }
                T item = (T)base.InnerList[0];
                base.InnerList.RemoveAt(0);
                return item;
            }
        }
        
    }
}
