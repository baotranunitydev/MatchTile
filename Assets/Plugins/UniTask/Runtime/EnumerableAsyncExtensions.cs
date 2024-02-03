﻿#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member

using System;
using System.Collections.Generic;

namespace Cysharp.Threading.Tasks
{
    public static class EnumerableAsyncExtensions
    {
        // overload resolver - .Select(async x => { }) : IEnumerable<UniTask<T>>

        public static IEnumerable<UnitaskVoid> Select<T>(this IEnumerable<T> source, Func<T, UnitaskVoid> selector)
        {
            return System.Linq.Enumerable.Select(source, selector);
        }

        public static IEnumerable<UniTask<TR>> Select<T, TR>(this IEnumerable<T> source, Func<T, UniTask<TR>> selector)
        {
            return System.Linq.Enumerable.Select(source, selector);
        }

        public static IEnumerable<UnitaskVoid> Select<T>(this IEnumerable<T> source, Func<T, int, UnitaskVoid> selector)
        {
            return System.Linq.Enumerable.Select(source, selector);
        }

        public static IEnumerable<UniTask<TR>> Select<T, TR>(this IEnumerable<T> source, Func<T, int, UniTask<TR>> selector)
        {
            return System.Linq.Enumerable.Select(source, selector);
        }
    }
}


