using System.Collections.Generic;
using System.Linq;

namespace Hangman
{
    internal class Game
    {
        public Word Word { get; }
        private List<Letter> _letters = new List<Letter>();
        private readonly VerifyEndOfGame _verifyEndOfGame;

        public Game(Word word, VerifyEndOfGame verifyEndOfGame)
        {
            Word = word;
            _verifyEndOfGame = verifyEndOfGame;
        }

        internal bool GuessLetter(Letter letter) =>
            _verifyEndOfGame.Verify(_letters,
                                    Word,
                                    Word.GuessLetter(letter))
                            .IsRight();

        internal string GetClue() =>
             Word.GetClue();

        internal List<Letter> GetInvalidLetters() =>
            _letters.Where(x => !x.IsRight()).ToList();

    }
}