
using System;

namespace HangmanCA
{
    class Program
    {
        public static void Main(string[] args)
        {
            Console.WriteLine("Starting Hangman Game...");
            Console.WriteLine("Welcome to Hangman!");

            /*
            1- declare variable, counter, to hold number of guesses wanted.
            2- select random word for the game from a hard coded array of strings words.
            3- display place holder for the letters to be guessed. use the symbol - for character place holder
            4- start the game by prompting the user to guess a letter for the hidden word. use a loop that ends at max counter allowed for the guesses.
            5- if the letter guessed is a part of the hidden word characters, replace the - placeholder at the correct position with the guessed letter
            6- otherwise place the wrongly guessed letter at the hang man drawing, abstracted as a set (no repititions) of max guesses.
            7- if the word is guessed correctly before the max guesses is reached, display a congratulations prompt
            8- otherwise display a losing message prompt
            9- give player the option to replay the game or exit. 
            */
            int counter = 9;
            char guessedLetter = '\0';
            string[] words = { "cat", "dog", "fish", "bird", "tree", "book", "game", "play", "work", "food" };
            Random rand = new Random();
            string randomWord = words[rand.Next(0, 10)];
            Console.WriteLine(randomWord);
            string displayedWord = "";

            string lettersPlaceHolders()
            {
                for (int i = 0; i < randomWord.Length; i++)
                {
                    displayedWord += "?";
                }
                Console.WriteLine();
                Console.WriteLine(displayedWord);
                return displayedWord;
            }

            string insertLetter()
            {
                for (int i = 0; i < randomWord.Length; i++)
                {
                    if (randomWord[i] == guessedLetter)
                    {
                        var tmp = displayedWord.ToCharArray();
                        tmp[i] = guessedLetter;
                        displayedWord = new string(tmp);
                    }
                }
                Console.WriteLine(displayedWord);
                return displayedWord;
            }

            lettersPlaceHolders();

            for (int i = 0; i < counter; i++)
            {
                Console.Write("what is you next guess? : ");
                var keyInfo = Console.ReadKey();
                guessedLetter = char.ToLowerInvariant(keyInfo.KeyChar);
                Console.WriteLine();

                if (randomWord.Contains(guessedLetter))
                {
                    insertLetter();
                    if (displayedWord == randomWord) break;
                }
            }

            Console.WriteLine("Press any key to exit.");
            Console.ReadKey();
            Console.WriteLine("Exiting Hangman Game...");
        }
    }
}

