using System;
using System.Collections.Generic;
using System.Linq;

namespace Hangman
{
    internal class VerifyEndOfGame
    {
        private Action<bool> _notfyEndGame;

        const int MAX_ATTEMPT = 6;

        public VerifyEndOfGame(Action<bool> notfyEndGame) =>
            _notfyEndGame = notfyEndGame;
        
        public Letter Verify(List<Letter> letters, Word word, Letter letter)
        {
            letters.AddIfNotExist(letter)
                .ExecuteIf((l) => l.Count(x => !x.IsRight()) > MAX_ATTEMPT,
                           () => _notfyEndGame(false));
            if (word.IsWordRight())
                _notfyEndGame(true);
            return letter;
        }
    }
}