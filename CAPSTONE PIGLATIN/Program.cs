using System;
using System.Linq;

namespace CAPSTONE_PIGLATIN
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Pig Latin Translator\n");  
            string continueEntry = null;
            bool wantToContinue = true;
            bool validUserInput = false;
            string userInput = null;

            while (wantToContinue)
            {
                wantToContinue = false;
                continueEntry = null;
                validUserInput = false;

                /* Get user input and if the user input is empty, ask again until user enters at least one character */
                while (!validUserInput)
                {
                    userInput = GetUserInput("Please enter a line in to the Translator:");
                    
                    if(String.IsNullOrEmpty(userInput))
                    {
                        Console.Write("Invalid Entry, ");
                        validUserInput = false;
                    }
                    else
                    {
                        validUserInput = true;
                    }
                }

                /* Split the user input string into an array of words */
                string[] words = userInput.Split(' ');

                /* Convert the user input sentense into string comprising of pig latin words */
                string result = PigLatin(words);

                /* display the pig latin string */
                Console.WriteLine(result + "\n");

                /* after converting the user input string into pig latin code, ask user if he/she wants to continue 
                 * with another string */
                while(continueEntry != "y" && continueEntry != "n")
                {
                    Console.WriteLine("Would you like to translate another line (y/n)?");
                    continueEntry = Console.ReadLine().ToLower();

                    /* check if user has entered correct input i.e. y or n; if not, ask user for another input */
                    if(continueEntry != "y" && continueEntry != "n")
                    {
                        Console.Write("Invalid entry, ");
                    }
                    else if (continueEntry == "y")
                    {
                        wantToContinue = true;
                    }
                    else if (continueEntry == "n")
                    {
                        wantToContinue = false;
                    }
                    else
                    {
                        Console.Write("Invalid entry, ");                       
                    }
                }

            }

            /* If user doesn't want to continue, exit code and say good bye */
            Console.WriteLine("Thank you for your time, Goodbye!");
            Console.ReadKey();
        }

        /* Custom method to get user's input */
        public static string GetUserInput(string message)
        {
            Console.WriteLine(message);
            string input = Console.ReadLine();
            return input;
        }

        /* custom method to validate digits and special characters from user input and using "WordToPigLatin" method */
        public static string PigLatin(string[] pig)
        {
             string[] newPig = new string[pig.Length];

            for (int i = 0; i < pig.Length; i++)
            {
                if(pig[i].All(Char.IsDigit) || HasSpecialChar(pig[i]))
                {
                    newPig[i] = pig[i];
                }
                else
                {
                    newPig[i] = WordToPigLatin(pig[i]);
                }
            }

            /* combine all the pig latin coded words together to make the complete string */
            string result = string.Join(' ', newPig);

            return result;
        }

        /* custom method to convert one word into pig latin coded word */
        public static string WordToPigLatin(string input)
        {
            const string vowels = "aeiouAEIOU";
            int index;            
            char first = input[0];

            /* If the first letter of word is a vowel, just add "way" at the end */
            if (vowels.Contains(first))
            {
                return input + "way";
            }

            /* find the index of first vowel in the string if the word starts with consonant */
            input = input.ToLower();
            for (index = 1; index < input.Length; index++)
            {
                if (vowels.Contains(input[index]))
                {
                    break;
                }
            }

            /* split the word into two parts from the first vowel and get last part */
            string lastpart = input.Substring(0, index) + "ay";
            string firstpart = "";

            /* get the first characters before the first vowel and store them into firstpart variable 
             Also maintain the case of the letter */
            if (char.IsUpper(first))
            {
                firstpart = char.ToUpper(input[index++]) + input.Substring(index, input.Length - index);
            }
            else
            {
                firstpart = input.Substring(index, input.Length - index);
            }

            /* return the complete word by combining first and last part */
            return firstpart + lastpart;
        }

        /* check if the input string has a special character */
        public static bool HasSpecialChar(string input)
        {
            string specialChar = @"\|!#$%&/()=?»«@£§€{}.-;<>_,";
            foreach (var item in specialChar)
            {
                if (input.Contains(item))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
