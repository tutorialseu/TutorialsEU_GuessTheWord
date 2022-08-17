using System.Linq;

class Program
{
    public static void Main(string[] args)
    {
        //Set the console to white and text to black
        Console.BackgroundColor = ConsoleColor.White;
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Black;
        string wordToGuess = null;

        Console.WriteLine("Welcome to 'Guess the word!' \nDo you want to type your own word to guess?");
        Console.WriteLine("Type 'Y' if you want to type your own word or 'N' if you want a randomly selected word");
        string randomOrNot = Console.ReadLine();
        randomOrNot = randomOrNot.ToUpper();
        bool isValidResponse = randomOrNot == "Y" || randomOrNot == "N";
        if (randomOrNot == "Y" || randomOrNot == "N")
        {
            isValidResponse = true;
        }
        else
        {
            isValidResponse = false;
        }
        while (isValidResponse == false)
        {
            Console.WriteLine("Please, only type 'Y' or 'N'");
            randomOrNot = Console.ReadLine();
            randomOrNot = randomOrNot.ToUpper();
            if (randomOrNot == "Y" || randomOrNot == "N")
            {
                isValidResponse = true;
            }
            else
            {
                isValidResponse = false;
            }
        }

        Console.Clear();

        if (randomOrNot == "Y")
        {
            Console.WriteLine("Host, please enter a word for the guest to guess: ");
            wordToGuess = Console.ReadLine();

            //Check if word is valid
            bool isOnlyLetters = wordToGuess.All(Char.IsLetter);
            while (isOnlyLetters == false || wordToGuess.Length == 0)
            {
                Console.WriteLine("Please enter only letters.  \nGive it another Try...");
                wordToGuess = Console.ReadLine();
                isOnlyLetters = wordToGuess.All(Char.IsLetter);
            }
            wordToGuess = wordToGuess.ToUpper();
        }
        else if (randomOrNot == "N")
        {
            // It wants to go to FileSystem\bin\Debug\net6.0\WordsToGuess.txt
            var path = Path.Combine(@"..\..\..\WordsToGuess.txt");
            string[] lines = File.ReadAllLines(path);
            Random random = new Random();
            int randomIndex = random.Next(lines.Length);
            wordToGuess = lines[randomIndex];
            wordToGuess = wordToGuess.ToUpper();
        }

        Console.Clear();

        int wordLength = wordToGuess.Length;
        char[] positionsToGuess = new char[wordLength];
        char[] wordToGuessChars = wordToGuess.ToCharArray();
        int lives = 5;
        List<char> lettersGuessed = new List<char>();
        bool gameWon = false;

        for (int i = 0; i < positionsToGuess.Length; i++)
        {
            positionsToGuess[i] = '-';
        }

        while (lives > 0)
        {
            string printProgress = String.Concat(positionsToGuess);
            bool letterFound = false;
            int multiples = 0;

            if (printProgress == wordToGuess)
            {
                gameWon = true;
                break;
            }

            Console.WriteLine("You have {0} lives!", lives);
            Console.WriteLine("Word to guess: " + printProgress);
            Console.WriteLine("\n\n\nGuess a letter: ");
            string playerGuess = Console.ReadLine();
            bool guessTest = playerGuess.All(Char.IsLetter);

            while (guessTest == false || playerGuess.Length != 1)
            {
                Console.WriteLine("Please enter only a single letter!");
                Console.Write("Guess a letter: ");
                playerGuess = Console.ReadLine();
                guessTest = playerGuess.All(Char.IsLetter);
            }

            playerGuess = playerGuess.ToUpper();
            char playerChar = Convert.ToChar(playerGuess);

            if (lettersGuessed.Contains(playerChar) == false)
            {

                lettersGuessed.Add(playerChar);

                for (int i = 0; i < wordToGuessChars.Length; i++)
                {
                    if (wordToGuessChars[i] == playerChar)
                    {
                        positionsToGuess[i] = playerChar;
                        letterFound = true;
                        multiples++;
                    }
                }

                if (letterFound)
                {
                    Console.Clear();
                    Console.WriteLine("Found {0} letter {1}!", multiples, playerChar);
                }
                else
                {
                    Console.Clear();
                    Console.WriteLine("No letter {0}!", playerChar);
                    lives--;
                }
            }
            else
            {
                Console.Clear();
                Console.WriteLine("You already guessed {0}!", playerChar);
            }
        }

        Console.Clear();
        if (gameWon)
        {
            Console.WriteLine("The word was: {0}", wordToGuess);
            Console.WriteLine("You Won!");
        }
        else
        {
            Console.WriteLine("The word was: {0}", wordToGuess);
            Console.WriteLine("You Lose...");
        }

    }
}