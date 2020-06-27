using System;

using Xunit;

namespace Hangman
{
    public class WordShould
    {
        [Theory]
        [InlineData("%")]
        [InlineData("&")]
        [InlineData("9")]
        public void Initilize_with_word_with_wrong_character(string word)
        {
            Assert.Throws<ArgumentException>(() => new Word(word));
        }
    }
}
