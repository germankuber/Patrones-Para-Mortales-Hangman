using System.Collections.Generic;
using System.Linq;

namespace Hangman
{
    internal class Word
    {
        private List<Letter> _word;

        public Word(string word) =>
            _word = word.Select(x => new Letter(x)).ToList();

        public Letter GuessLetter(Letter letter) =>
            _word.Where(x => x == letter)
                .IfIsNotEmpty(() => letter.SetRight())
                .Aggregate(letter, (a, result) =>
                 {
                     result.SetRight();
                     return a;
                 });

        public bool IsWordRight() => _word.All(x => x.IsRight());

        internal string GetClue() =>
            string.Concat(_word.Select(x => x.IsRight() ? x : "_"));
    }
}