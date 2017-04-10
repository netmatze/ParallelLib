using System;
using System.Collections.Generic;
using System.Text;

namespace Intact.ParallelLib
{
    public delegate void Func();
    //public delegate TResult Func<TResult>();
    //public delegate TResult Func<T, TResult>(T value);
    //public delegate TResult Func<T1, T2, TResult>(T1 value1, T2 value2);
    //public delegate TResult Func<T1, T2, T3, TResult>(T1 value1, T2 value2, T3 value3);

    public delegate void Action(ref int source);    
    public delegate void Action<T>(ref int source, T value);
    public delegate void Action<T1, T2>(ref int source, T1 value1, T2 value2);
    public delegate void Action<T1, T2, T3>(ref int source, T1 value1, T2 value2, T3 value3);
    public delegate void Action<T1, T2, T3, T4>(ref int source, T1 value1, T2 value2, T4 value4);
    public delegate bool Constraint<T>(T constraint);    
    public delegate void ConstraintAction(ref int constraintAction);

    public delegate void InvokeAction();
    public delegate void InvokeAction<T>(T value);
    public delegate void InvokeAction<T1, T2>(T1 value1, T2 value2);
    public delegate void InvokeAction<T1, T2, T3>(T1 value1, T2 value2, T3 value3);


    internal abstract class ParallelConstruct
    {
        protected static int processorCount;
        protected static int chunk;

        #region public ParallelConstruct()

        static ParallelConstruct()
        {
            processorCount = Environment.ProcessorCount;            
        }

        #endregion        
    }
}
