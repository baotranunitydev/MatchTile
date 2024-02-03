using Cysharp.Threading.Tasks.Internal;
using System;
using System.Threading;

namespace Cysharp.Threading.Tasks.Linq
{
    public static partial class UniTaskAsyncEnumerable
    {
        public static UnitaskVoid ForEachAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Action<TSource> action, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(action, nameof(action));

            return Cysharp.Threading.Tasks.Linq.ForEach.ForEachAsync(source, action, cancellationToken);
        }

        public static UnitaskVoid ForEachAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Action<TSource, Int32> action, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(action, nameof(action));

            return Cysharp.Threading.Tasks.Linq.ForEach.ForEachAsync(source, action, cancellationToken);
        }

        /// <summary>Obsolete(Error), Use Use ForEachAwaitAsync instead.</summary>
        [Obsolete("Use ForEachAwaitAsync instead.", true)]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public static UnitaskVoid ForEachAsync<T>(this IUniTaskAsyncEnumerable<T> source, Func<T, UnitaskVoid> action, CancellationToken cancellationToken = default)
        {
            throw new NotSupportedException("Use ForEachAwaitAsync instead.");
        }

        /// <summary>Obsolete(Error), Use Use ForEachAwaitAsync instead.</summary>
        [Obsolete("Use ForEachAwaitAsync instead.", true)]
        [System.ComponentModel.EditorBrowsable(System.ComponentModel.EditorBrowsableState.Never)]
        public static UnitaskVoid ForEachAsync<T>(this IUniTaskAsyncEnumerable<T> source, Func<T, int, UnitaskVoid> action, CancellationToken cancellationToken = default)
        {
            throw new NotSupportedException("Use ForEachAwaitAsync instead.");
        }

        public static UnitaskVoid ForEachAwaitAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UnitaskVoid> action, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(action, nameof(action));

            return Cysharp.Threading.Tasks.Linq.ForEach.ForEachAwaitAsync(source, action, cancellationToken);
        }

        public static UnitaskVoid ForEachAwaitAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Int32, UnitaskVoid> action, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(action, nameof(action));

            return Cysharp.Threading.Tasks.Linq.ForEach.ForEachAwaitAsync(source, action, cancellationToken);
        }

        public static UnitaskVoid ForEachAwaitWithCancellationAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UnitaskVoid> action, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(action, nameof(action));

            return Cysharp.Threading.Tasks.Linq.ForEach.ForEachAwaitWithCancellationAsync(source, action, cancellationToken);
        }

        public static UnitaskVoid ForEachAwaitWithCancellationAsync<TSource>(this IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Int32, CancellationToken, UnitaskVoid> action, CancellationToken cancellationToken = default)
        {
            Error.ThrowArgumentNullException(source, nameof(source));
            Error.ThrowArgumentNullException(action, nameof(action));

            return Cysharp.Threading.Tasks.Linq.ForEach.ForEachAwaitWithCancellationAsync(source, action, cancellationToken);
        }
    }

    internal static class ForEach
    {
        public static async UnitaskVoid ForEachAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Action<TSource> action, CancellationToken cancellationToken)
        {
            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    action(e.Current);
                }
            }
            finally
            {
                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }
        }

        public static async UnitaskVoid ForEachAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Action<TSource, Int32> action, CancellationToken cancellationToken)
        {
            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                int index = 0;
                while (await e.MoveNextAsync())
                {
                    action(e.Current, checked(index++));
                }
            }
            finally
            {
                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }
        }

        public static async UnitaskVoid ForEachAwaitAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, UnitaskVoid> action, CancellationToken cancellationToken)
        {
            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    await action(e.Current);
                }
            }
            finally
            {
                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }
        }

        public static async UnitaskVoid ForEachAwaitAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Int32, UnitaskVoid> action, CancellationToken cancellationToken)
        {
            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                int index = 0;
                while (await e.MoveNextAsync())
                {
                    await action(e.Current, checked(index++));
                }
            }
            finally
            {
                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }
        }

        public static async UnitaskVoid ForEachAwaitWithCancellationAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, CancellationToken, UnitaskVoid> action, CancellationToken cancellationToken)
        {
            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                while (await e.MoveNextAsync())
                {
                    await action(e.Current, cancellationToken);
                }
            }
            finally
            {
                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }
        }

        public static async UnitaskVoid ForEachAwaitWithCancellationAsync<TSource>(IUniTaskAsyncEnumerable<TSource> source, Func<TSource, Int32, CancellationToken, UnitaskVoid> action, CancellationToken cancellationToken)
        {
            var e = source.GetAsyncEnumerator(cancellationToken);
            try
            {
                int index = 0;
                while (await e.MoveNextAsync())
                {
                    await action(e.Current, checked(index++), cancellationToken);
                }
            }
            finally
            {
                if (e != null)
                {
                    await e.DisposeAsync();
                }
            }
        }
    }
}