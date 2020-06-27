using System;
using System.Collections.Generic;
using System.Linq;

namespace Hangman
{
    static class EnumerableExtensions
    {
        public static IEnumerable<T> IfIsNotEmpty<T>(this IEnumerable<T> list, Action act)
        {
            if (list.Any())
                act();
            return list;
        }
    }
}