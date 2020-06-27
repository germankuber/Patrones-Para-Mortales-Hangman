using System;
using System.Collections.Generic;
using System.Linq;

namespace Hangman
{
    internal static class ListExtension
    {
        internal static List<Letter> AddIfNotExist(this List<Letter> list, Letter letter)
        {
            if (!list.Any(x => x == letter))
                list.Add(letter);
            return list;
        }
        internal static List<Letter> ExecuteIf(this List<Letter> list, Func<List<Letter>, bool> funcIf, Action act)
        {
            if (funcIf(list))
                act();
            return list;
        }
    }
}