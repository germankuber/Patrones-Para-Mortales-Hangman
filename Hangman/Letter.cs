using System;
using System.Text.RegularExpressions;

namespace Hangman
{
    internal class Letter
    {
        private bool _right = false;
        private char _letter;
        public Letter(char letter)
        {
            var match = Regex.Match(letter.ToString(), "[a-zA-Z]");
            if (!match.Success)
                throw new ArgumentException();
            _letter = letter;
        }
        public void SetRight()
        {
            _right = true;
        }
        public bool IsRight() => _right;
        public static bool operator ==(Letter letterA, Letter letterB) =>
            letterA._letter.ToString().ToLower() == letterB._letter.ToString().ToLower();
        public static bool operator !=(Letter letterA, Letter letterB) =>
            letterA._letter.ToString().ToLower() != letterB._letter.ToString().ToLower();

        public static implicit operator string(Letter d) => d._letter.ToString();
    }
}