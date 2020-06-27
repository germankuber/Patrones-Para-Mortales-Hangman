using FluentAssertions;

using System.Linq;

using Xunit;

namespace Hangman
{
    public class GameShould
    {
        private readonly Word _word = new Word("Test");
        private Game game;
        public GameShould()
        {
            game = new Game(_word, new VerifyEndOfGame(x => { }));
        }

        [Fact]
        public void Has_The_Word()
        {
            game.Word.Should().Be(_word);
        }

        [Fact]
        public void Choose_The_Right_Letter()
        {
            game.GuessLetter(new Letter('T')).Should().BeTrue();
        }

        [Fact]
        public void Choose_The_Wrong_Letter()
        {
            game.GuessLetter(new Letter('g')).Should().BeFalse();
        }

        [Theory]
        [InlineData('t')]
        [InlineData('S')]
        public void Choose_The_Right_Letter_Ignore_Casing(char letter)
        {
            game.GuessLetter(new Letter(letter)).Should().BeTrue();
        }

        [Fact]
        public void Choose_The_Wrong_Letter_Keep_Invalid_Letters()
        {
            game.GuessLetter(new Letter('g'));

            game.GetInvalidLetters().Any(x => x == new Letter('g')).Should().BeTrue();
        }

        [Fact]
        public void Choose_The_Wrong_Letter_Keep_Right_Letters()
        {
            game.GuessLetter(new Letter('t'));
            game.GetClue().Contains('t').Should().BeTrue();
        }

        [Fact]
        public void Return_Clue()
        {
            game.GetClue().Should().Be("____");

        }

        [Theory]
        [InlineData('s', "__s_")]
        [InlineData('t', "T__t")]
        [InlineData('e', "_e__")]
        public void Choose_The_Right_Letter_Return_Letter_In_Clue(char letter, string clue)
        {
            game.GuessLetter(new Letter(letter));
            game.GetClue().Should().Be(clue);
        }

        [Fact]
        public void Choose_The_Second_Letter_Return_Letter_In_Clue()
        {
            game.GuessLetter(new Letter('e'));
            game.GuessLetter(new Letter('s'));
            game.GetClue().Should().Be("_es_");
        }


        [Fact]
        public void Lost_The_Game()
        {
            var winGame = true;
            var game = new Game(_word, new VerifyEndOfGame((w) =>
            {
                winGame = w;
            }));
            game.GuessLetter(new Letter('a'));
            game.GuessLetter(new Letter('b'));
            game.GuessLetter(new Letter('c'));
            game.GuessLetter(new Letter('d'));
            game.GuessLetter(new Letter('p'));
            game.GuessLetter(new Letter('f'));
            game.GuessLetter(new Letter('z'));
            winGame.Should().BeFalse();
        }

        [Fact]
        public void Win_The_Game()
        {
            var winGame = false;
            var game = new Game(_word, new VerifyEndOfGame((w) =>
            {
                winGame = w;
            }));
            game.GuessLetter(new Letter('t'));
            game.GuessLetter(new Letter('e'));
            game.GuessLetter(new Letter('s'));
            winGame.Should().BeTrue();
        }

        [Fact]
        public void Not_Repeat_Wrong_Letter()
        {
            game.GuessLetter(new Letter('h'));
            game.GuessLetter(new Letter('h'));
            game.GetInvalidLetters().Select(x => x == new Letter('t')).Count().Should().Be(1);
        }
    }
}
