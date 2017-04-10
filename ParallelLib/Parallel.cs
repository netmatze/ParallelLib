using System;
using System.Collections.Generic;
using System.Text;

namespace Intact.ParallelLib
{
    /// <summary>
    /// Die Parallel Klasse teilt Aufgaben auf die einzelnen cores eines Multicore Prozessors auf.
    /// </summary>
    public static class Parallel
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="actions"></param>
        public static void Invoke(params InvokeAction[] actions)
        {
            Invoke invoke = new Invoke();
            invoke.InvokeExecution(actions);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="executionConstancy"></param>
        /// <param name="actions"></param>
        public static void Invoke(ExecutionConstancyEnum executionConstancy, params InvokeAction[] actions)
        {
            Invoke invoke = new Invoke();
            invoke.InvokeExecution(executionConstancy, actions);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="actions"></param>
        public static void Invoke<T>(T value, params InvokeAction<T>[] actions)
        {
            Invoke invoke = new Invoke();
            invoke.InvokeExecution<T>(value, actions);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="value"></param>
        /// <param name="executionConstancy"></param>
        /// <param name="actions"></param>
        public static void Invoke<T>(T value, ExecutionConstancyEnum executionConstancy, params InvokeAction<T>[] actions)
        {
            Invoke invoke = new Invoke();
            invoke.InvokeExecution<T>(value, executionConstancy, actions);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <param name="actions"></param>
        public static void Invoke<T1, T2>(T1 value1, T2 value2, params InvokeAction<T1, T2>[] actions)
        {
            Invoke invoke = new Invoke();
            invoke.InvokeExecution<T1, T2>(value1, value2, actions);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <param name="executionConstancy"></param>
        /// <param name="actions"></param>
        public static void Invoke<T1, T2>(T1 value1, T2 value2, ExecutionConstancyEnum executionConstancy, params InvokeAction<T1, T2>[] actions)
        {
            Invoke invoke = new Invoke();
            invoke.InvokeExecution<T1, T2>(value1, value2, executionConstancy, actions);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <param name="value3"></param>
        /// <param name="actions"></param>
        public static void Invoke<T1, T2, T3>(T1 value1, T2 value2, T3 value3, params InvokeAction<T1, T2, T3>[] actions)
        {
            //Invoke<T1, T2, T3> invoke = new Invoke<T1, T2, T3>();
            //invoke.InvokeExecution(actions);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <typeparam name="T3"></typeparam>
        /// <param name="value1"></param>
        /// <param name="value2"></param>
        /// <param name="value3"></param>
        /// <param name="executionConstancy"></param>
        /// <param name="actions"></param>
        public static void Invoke<T1, T2, T3>(T1 value1, T2 value2, T3 value3, ExecutionConstancyEnum executionConstancy, params InvokeAction<T1, T2, T3>[] actions)
        {
            //Invoke<T1, T2, T3> invoke = new Invoke<T1, T2, T3>();
            //invoke.InvokeExecution(actions);
        }

        /// <summary>
        /// Eine do - while schleife wird parallel auf mehreren Kernen ausgeführt.
        /// </summary>       
        /// <param name="constraint">Die bedingung für while.</param>
        /// <param name="constraintAction">Die änderung der while methode</param>
        /// <param name="action">Der Schleifenkörper der ausgeführt wird.
        /// (mit dem übergebenen Wert kann auf die Schleifenvariable zugegriffen werden)</param>        
        public static void Do(Constraint<int> constraint, ConstraintAction constraintAction, Action action)
        {
            Do doExecution = new Do();
            doExecution.DoExecution(constraint, constraintAction, action);
        }
 
        /// <summary>
        /// Eine do - while schleife wird parallel auf mehreren Kernen ausgeführt.
        /// </summary>
        /// <typeparam name="T">Type des value Elements.</typeparam>
        /// <param name="constraint">Die bedingung für while.</param>
        /// <param name="constraintAction">Die änderung der while methode</param>
        /// <param name="action">Der Schleifenkörper der ausgeführt wird.
        /// (mit dem übergebenen Wert kann auf die Schleifenvariable zugegriffen werden)</param>
        /// <param name="value">value Element dass an den Schleifenkörper mitgegeben wird.</param>
        public static void Do<T>(Constraint<int> constraint, ConstraintAction constraintAction, Action<T> action, T value)
        {
            Do doExecution = new Do();
            doExecution.DoExecution<T>(constraint, constraintAction, action, value);
        }

        /// <summary>
        /// Eine do - while schleife wird parallel auf mehreren Kernen ausgeführt.
        /// </summary>
        /// <typeparam name="T1">Type des ersten value Elements</typeparam>
        /// <typeparam name="T2">Type des zweiten value Elements</typeparam>
        /// <param name="constraint">Die bedingung für while.</param>
        /// <param name="constraintAction">Die änderung der while methode</param>
        /// <param name="action">Der Schleifenkörper der ausgeführt wird.
        /// (mit dem übergebenen Wert kann auf die Schleifenvariable zugegriffen werden)</param>
        /// <param name="value1">erstes value Element dass an den Schleifenkörper mitgegeben wird.</param>
        /// <param name="value2">zweites value Element dass an den Schleifenkörper mitgegeben wird.</param>
        public static void Do<T1, T2>(Constraint<int> constraint, ConstraintAction constraintAction, 
            Action<T1, T2> action, T1 value1, T2 value2)
        {
            Do doExecution = new Do();
            doExecution.DoExecution<T1,T2>(constraint, constraintAction, action, value1, value2);
        }

        /// <summary>
        /// Eine for schleife wird parallel auf mehreren Kernen ausgeführt.
        /// </summary>
        /// <param name="from">von welchem Wert an soll die Schleife laufen</param>
        /// <param name="to">bis zu welchen Wert soll die for Schleife laufen</param>
        /// <param name="action">Der Schleifenkörper der ausgeführt wird.
        /// (mit dem übergebenen Wert kann auf die Schleifenvariable zugegriffen werden)</param>
        public static void For(int from, int to, Action action)
        {
            For forExecution = new For();
            forExecution.ForExecution(from, to, 1, action);
        }

        /// <summary>
        /// Eine for schleife wird parallel auf mehreren Kernen ausgeführt. Zusätzlich wird eine Aggregation auf die einzelnen Ergebnisse 
        /// des Schleifenbodys angewendet.
        /// </summary>
        /// <typeparam name="TState">type des state Objekts</typeparam>
        /// <param name="from">von welchem Wert an soll die Schleife laufen</param>
        /// <param name="to">bis zu welchen Wert soll die for Schleife laufen</param>
        /// <param name="state">initalisierung des local States</param>
        /// <param name="action">Der Schleifenkörper der ausgeführt wird.
        /// (mit dem übergebenen Wert kann auf die Schleifenvariable zugegriffen werden)</param>
        /// <param name="aggregation">Die Aggregation die auf die Teilergebnisse der einzelnen Schleifenbodies ausgeführt werden</param>
        public static void For<TState>(int from, int to, Func<TState> state,
            Action<ThreadLocalState<TState>> action, InvokeAction<ThreadLocalState<TState>> aggregation)
        {
            For forExecution = new For();
            forExecution.ForExecution<TState>(from, to, 1, state, action, aggregation);
        }

        /// <summary>
        /// Eine for schleife wird parallel auf mehreren Kernen ausgeführt. Zusätzlich wird eine Aggregation auf die einzelnen Ergebnisse 
        /// des Schleifenbodys angewendet.
        /// </summary>
        /// <typeparam name="TState">type des state Objekts</typeparam>
        /// <typeparam name="T">type der mitübergebenen value</typeparam>
        /// <param name="from">von welchem Wert an soll die Schleife laufen</param>
        /// <param name="to">bis zu welchen Wert soll die for Schleife laufen</param>
        /// <param name="state">initalisierung des local States</param>
        /// <param name="action">Der Schleifenkörper der ausgeführt wird.
        /// (mit dem übergebenen Wert kann auf die Schleifenvariable zugegriffen werden)</param>
        /// <param name="aggregation">Die Aggregation die auf die Teilergebnisse der einzelnen Schleifenbodies ausgeführt werden</param>
        /// <param name="value">mitübergebene value</param>
        public static void For<T, TState>(int from, int to, Func<T, TState> state,
            Action<T, ThreadLocalState<TState>> action, InvokeAction<T, ThreadLocalState<TState>> aggregation, T value)
        {
            For forExecution = new For();
            forExecution.ForExecution<TState, T>(from, to, 1, state, action, aggregation, value);
        }

        /// <summary>
        /// Eine for schleife wird parallel auf mehreren Kernen ausgeführt.
        /// </summary>
        /// <param name="from">von welchem Wert an soll die Schleife laufen</param>
        /// <param name="to">bis zu welchen Wert soll die for Schleife laufen</param>
        /// <param name="action">Der Schleifenkörper der ausgeführt wird.
        /// (mit dem übergebenen Wert kann auf die Schleifenvariable zugegriffen werden)</param>
        /// <param name="executionConstancy">worksteeling verwenden (Nonconstant)</param>
        public static void For(int from, int to, Action action, ExecutionConstancyEnum executionConstancy)
        {
            For forExecution = new For();
            forExecution.ForExecution(from, to, 1, action, executionConstancy);
        }

        /// <summary>
        /// Eine for schleife wird parallel auf mehreren Kernen ausgeführt.
        /// </summary>
        /// <typeparam name="T">Type des value Elements.</typeparam>
        /// <param name="from">von welchem Wert an soll die Schleife laufen</param>
        /// <param name="to">bis zu welchen Wert soll die for Schleife laufen</param>
        /// <param name="action">Der Schleifenkörper der ausgeführt wird.
        /// (mit dem übergebenen Wert kann auf die Schleifenvariable zugegriffen werden)</param>
        /// <param name="value">value Element dass an den Schleifenkörper mitgegeben wird.</param>
        public static void For<T>(int from, int to, Action<T> action, T value)
        {
            For forExecution = new For();
            forExecution.ForExecution<T>(from, to, 1, action, value);
        }

        /// <summary>
        /// Eine for schleife wird parallel auf mehreren Kernen ausgeführt.
        /// </summary>
        /// <typeparam name="T">Type des value Elements.</typeparam>
        /// <param name="from">von welchem Wert an soll die Schleife laufen</param>
        /// <param name="to">bis zu welchen Wert soll die for Schleife laufen</param>
        /// <param name="action">Der Schleifenkörper der ausgeführt wird.
        /// (mit dem übergebenen Wert kann auf die Schleifenvariable zugegriffen werden)</param>
        /// <param name="executionConstancy">worksteeling verwenden (Nonconstant)</param>
        /// <param name="value">value Element dass an den Schleifenkörper mitgegeben wird.</param>
        public static void For<T>(int from, int to, Action<T> action, ExecutionConstancyEnum executionConstancy, T value)
        {
            For forExecution = new For();
            forExecution.ForExecution<T>(from, to, 1, action, executionConstancy, value);
        }

        /// <summary>
        /// Eine for schleife wird parallel auf mehreren Kernen ausgeführt.
        /// </summary>
        /// <typeparam name="T1">Type des ersten value Elements.</typeparam>
        /// <typeparam name="T2">Type des zweiten value Elements.</typeparam>
        /// <param name="from">von welchem Wert an soll die Schleife laufen</param>
        /// <param name="to">bis zu welchen Wert soll die for Schleife laufen</param>
        /// <param name="action">Der Schleifenkörper der ausgeführt wird.
        /// (mit dem übergebenen Wert kann auf die Schleifenvariable zugegriffen werden)</param>
        /// <param name="value">erstes value Element dass an den Schleifenkörper mitgegeben wird.</param>
        /// <param name="value">zweites value Element dass an den Schleifenkörper mitgegeben wird.</param>
        public static void For<T1, T2>(int from, int to, Action<T1, T2> action, T1 value1, T2 value2)
        {
            For forExecution = new For();
            forExecution.ForExecution<T1, T2>(from, to, 1, action, value1, value2);
        }

        /// <summary>
        /// Eine for schleife wird parallel auf mehreren Kernen ausgeführt.
        /// </summary>
        /// <typeparam name="T1">Type des ersten value Elements.</typeparam>
        /// <typeparam name="T2">Type des zweiten value Elements.</typeparam>
        /// <param name="from">von welchem Wert an soll die Schleife laufen</param>
        /// <param name="to">bis zu welchen Wert soll die for Schleife laufen</param>
        /// <param name="action">Der Schleifenkörper der ausgeführt wird.
        /// (mit dem übergebenen Wert kann auf die Schleifenvariable zugegriffen werden)</param>
        /// <param name="executionConstancy">worksteeling verwenden (Nonconstant)</param>
        /// <param name="value">erstes value Element dass an den Schleifenkörper mitgegeben wird.</param>
        /// <param name="value">zweites value Element dass an den Schleifenkörper mitgegeben wird.</param>
        public static void For<T1, T2>(int from, int to, Action<T1, T2> action, ExecutionConstancyEnum executionConstancy, T1 value1, T2 value2)
        {
            For forExecution = new For();
            forExecution.ForExecution<T1, T2>(from, to, 1, action, executionConstancy, value1, value2);
        }

        /// <summary>
        /// Eine for Schleife wird parallel auf mehreren Kernen ausgeführt.
        /// </summary>
        /// <param name="from">von welchem Wert an soll die Schleife laufen</param>
        /// <param name="to">bis zu welchen Wert soll die for Schleife laufen</param>
        /// <param name="step">um wieviel soll die Schleifenvaribale bei jedem Durchlauf erhöht werden</param>
        /// <param name="action">Der Schleifenkörper der ausgeführt wird.
        /// (mit dem übergebenen Wert kann auf die Schleifenvariable zugegriffen werden)</param>
        public static void For(int from, int to, int step, Action action)
        {
            For forExecution = new For();
            forExecution.ForExecution(from, to, step, action);
        }

        /// <summary>
        /// Eine for schleife wird parallel auf mehreren Kernen ausgeführt.
        /// </summary>
        /// <param name="from">von welchem Wert an soll die Schleife laufen</param>
        /// <param name="to">bis zu welchen Wert soll die for Schleife laufen</param>
        /// <param name="step">um wieviel soll die Schleifenvaribale bei jedem Durchlauf erhöht werden</param>
        /// <param name="action">Der Schleifenkörper der ausgeführt wird.
        /// (mit dem übergebenen Wert kann auf die Schleifenvariable zugegriffen werden)</param>
        /// <param name="executionConstancy">worksteeling verwenden (Nonconstant)</param>
        public static void For(int from, int to, int step, Action action, ExecutionConstancyEnum executionConstancy)
        {
            For forExecution = new For();
            forExecution.ForExecution(from, to, step, action, executionConstancy);
        }
        
        /// <summary>
        /// Eine for Schleife wird parallel auf mehreren Kernen ausgeführt.
        /// </summary>
        /// <typeparam name="T">Type des value Elements.</typeparam>
        /// <param name="from">von welchem Wert an soll die Schleife laufen.</param>
        /// <param name="to">bis zu welchen Wert soll die for Schleife laufen.</param>
        /// <param name="step">um wieviel soll die Schleifenvaribale bei jedem Durchlauf erhöht werden.</param>
        /// <param name="action">Der Schleifenkörper der ausgeführt wird.
        /// (mit dem übergebenen Wert kann auf die Schleifenvariable zugegriffen werden)</param>
        /// <param name="value1">mitübergebener wert</param>
        public static void For<T>(int from, int to, int step, Action<T> action, T value)
        {
            For forExecution = new For();
            forExecution.ForExecution<T>(from, to, step, action, value);
        }

        /// <summary>
        /// Eine for Schleife wird parallel auf mehreren Kernen ausgeführt.
        /// </summary>
        /// <typeparam name="T">Type des value Elements.</typeparam>
        /// <param name="from">von welchem Wert an soll die Schleife laufen.</param>
        /// <param name="to">bis zu welchen Wert soll die for Schleife laufen.</param>
        /// <param name="step">um wieviel soll die Schleifenvaribale bei jedem Durchlauf erhöht werden.</param>
        /// <param name="action">Der Schleifenkörper der ausgeführt wird.
        /// (mit dem übergebenen Wert kann auf die Schleifenvariable zugegriffen werden)</param>
        /// <param name="executionConstancy">worksteeling verwenden (Nonconstant)</param>
        /// <param name="value1">mitübergebener wert</param>
        public static void For<T>(int from, int to, int step, Action<T> action, ExecutionConstancyEnum executionConstancy, T value)
        {
            For forExecution = new For();
            forExecution.ForExecution<T>(from, to, step, action, executionConstancy, value);
        }

        /// <summary>
        /// Eine for Schleife wird parallel auf mehreren Kernen ausgeführt.
        /// </summary>
        /// <typeparam name="T1">Type des ersten value Elements.</typeparam>
        /// <typeparam name="T2">Type des zweiten value Elements.</typeparam>
        /// <param name="from">von welchem Wert an soll die Schleife laufen</param>
        /// <param name="to">bis zu welchen Wert soll die for Schleife laufen</param>
        /// <param name="step">um wieviel soll die Schleifenvaribale bei jedem Durchlauf erhöht werden</param>
        /// <param name="action">Der Schleifenkörper der ausgeführt wird. 
        /// (mit dem übergebenen Wert kann auf die Schleifenvariable zugegriffen werden)</param>
        /// <param name="value1">erster mitübergebener wert</param>
        /// <param name="value2">zweiter mitübergebener wert</param>
        public static void For<T1, T2>(int from, int to, int step, Action<T1, T2> action, T1 value1, T2 value2)
        {
            For forExecution = new For();
            forExecution.ForExecution<T1, T2>(from, to, step, action, value1, value2);
        }

        /// <summary>
        /// Eine for Schleife wird parallel auf mehreren Kernen ausgeführt.
        /// </summary>
        /// <typeparam name="T1">Type des ersten value Elements.</typeparam>
        /// <typeparam name="T2">Type des zweiten value Elements.</typeparam>
        /// <param name="from">von welchem Wert an soll die Schleife laufen</param>
        /// <param name="to">bis zu welchen Wert soll die for Schleife laufen</param>
        /// <param name="step">um wieviel soll die Schleifenvaribale bei jedem Durchlauf erhöht werden</param>
        /// <param name="action">Der Schleifenkörper der ausgeführt wird. 
        /// (mit dem übergebenen Wert kann auf die Schleifenvariable zugegriffen werden)</param>
        /// <param name="executionConstancy">worksteeling verwenden (Nonconstant)</param>
        /// <param name="value1">erster mitübergebener wert</param>
        /// <param name="value2">zweiter mitübergebener wert</param>
        public static void For<T1, T2>(int from, int to, int step, Action<T1, T2> action, ExecutionConstancyEnum executionConstancy, T1 value1, T2 value2)
        {
            For forExecution = new For();
            forExecution.ForExecution<T1, T2>(from, to, step, action, executionConstancy, value1, value2);
        }

        /// <summary>
        /// Eine foreach Schleife wird parallel auf mehreren Kernen ausgeührt.
        /// </summary>
        /// <typeparam name="T">Type der Collectionelemente über die iteriert wird.</typeparam>
        /// <param name="source">Die Collection über die iteriert wird.</param>
        /// <param name="action">Welcher Schleifenkörper soll ausgeführt werden. </param>        
        public static void Foreach<T>(IEnumerable<T> source, Action<T> action)
        {
            Foreach<T> foreachExecution = new Foreach<T>();
            foreachExecution.ForeachExecution(source, action);
        }

        /// <summary>
        /// Eine foreach Schleife wird parallel auf mehreren Kernen ausgeührt.
        /// </summary>
        /// <typeparam name="T">Type der Collectionelemente über die iteriert wird.</typeparam>
        /// <param name="source">Die Collection über die iteriert wird.</param>
        /// <param name="action">Welcher Schleifenkörper soll ausgeführt werden. </param>        
        /// <param name="executionConstancy">worksteeling verwenden (Nonconstant)</param>
        public static void Foreach<T>(IEnumerable<T> source, Action<T> action, ExecutionConstancyEnum executionConstancy)
        {
            Foreach<T> foreachExecution = new Foreach<T>();
            foreachExecution.ForeachExecution(source, action, executionConstancy);
        }

        /// <summary>
        /// Eine foreach Schleife wird parallel auf mehreren Kernen ausgeührt.
        /// </summary>
        /// <typeparam name="T">Type der Collectionelemente über die iteriert wird.</typeparam>
        /// <typeparam name="T1">Type des ersten value Elements.</typeparam>
        /// <param name="source">Die Collection über die iteriert wird.</param>
        /// <param name="action">Der Schleifenkörper der ausgeführt wird.</param>
        /// <param name="value1">value Element dass an den Schleifenkörper mitgegeben wird.</param>
        public static void Foreach<T, T1>(IEnumerable<T> source, Action<T, T1> action, T1 value1)
        {
            Foreach<T> foreachExecution = new Foreach<T>();
            foreachExecution.ForeachExecution<T1>(source, action, value1);
        }

        /// <summary>
        /// Eine foreach Schleife wird parallel auf mehreren Kernen ausgeührt.
        /// </summary>
        /// <typeparam name="T">Type der Collectionelemente über die iteriert wird.</typeparam>
        /// <typeparam name="T1">Type des ersten value Elements.</typeparam>
        /// <param name="source">Die Collection über die iteriert wird.</param>
        /// <param name="action">Der Schleifenkörper der ausgeführt wird.</param>
        /// <param name="executionConstancy">worksteeling verwenden (Nonconstant)</param>
        /// <param name="value1">value Element dass an den Schleifenkörper mitgegeben wird.</param>
        public static void Foreach<T, T1>(IEnumerable<T> source, Action<T, T1> action, ExecutionConstancyEnum executionConstancy, T1 value1)
        {
            Foreach<T> foreachExecution = new Foreach<T>();
            foreachExecution.ForeachExecution<T1>(source, action, executionConstancy, value1);
        }

        /// <summary>
        /// Eine foreach Schleife wird parallel auf mehreren Kernen ausgeührt.
        /// </summary>
        /// <typeparam name="T">Type der Collectionelemente über die iteriert wird.</typeparam>
        /// <typeparam name="T1">Type des ersten value Elements.</typeparam>
        /// <typeparam name="T2">Type des zweiten value Elements.</typeparam>
        /// <param name="source">Die Collection über die iteriert wird.</param>
        /// <param name="action">Der Schleifenkörper der ausgeführt wird.</param>
        /// <param name="value1">erstes value Element dass an den Schleifenkörper mitgegeben wird.</param>
        /// <param name="value2">zweites value Element dass an den Schleifenkörper mitgegeben wird.</param>
        public static void Foreach<T, T1, T2>(IEnumerable<T> source, Action<T, T1, T2> action, T1 value1, T2 value2)
        {
            Foreach<T> foreachExecution = new Foreach<T>();
            foreachExecution.ForeachExecution<T1, T2>(source, action, value1, value2);
        }

        /// <summary>
        /// Eine foreach Schleife wird parallel auf mehreren Kernen ausgeührt.
        /// </summary>
        /// <typeparam name="T">Type der Collectionelemente über die iteriert wird.</typeparam>
        /// <typeparam name="T1">Type des ersten value Elements.</typeparam>
        /// <typeparam name="T2">Type des zweiten value Elements.</typeparam>
        /// <param name="source">Die Collection über die iteriert wird.</param>
        /// <param name="action">Der Schleifenkörper der ausgeführt wird.</param>
        /// <param name="executionConstancy">worksteeling verwenden (Nonconstant)</param>
        /// <param name="value1">erstes value Element dass an den Schleifenkörper mitgegeben wird.</param>
        /// <param name="value2">zweites value Element dass an den Schleifenkörper mitgegeben wird.</param>
        public static void Foreach<T, T1, T2>(IEnumerable<T> source, Action<T, T1, T2> action, ExecutionConstancyEnum executionConstancy, T1 value1, T2 value2)
        {
            Foreach<T> foreachExecution = new Foreach<T>();
            foreachExecution.ForeachExecution<T1, T2>(source, action, executionConstancy, value1, value2);
        }        
    }
}
