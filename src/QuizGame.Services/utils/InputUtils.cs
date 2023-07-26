using QuizGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace QuizGame.Services.utils
{
    public class InputUtils
    {
        public static string RequestValue()
        {
            string? input;
            do
            {
                input = Console.ReadLine();
                if (string.IsNullOrEmpty(input))
                {
                    Console.WriteLine("Enter a value");
                }
            } while (string.IsNullOrEmpty(input));

            return input;
        }

        public static int Selection(string input)
        {
            int selection;
            if (!int.TryParse(input, out selection))
            {
                throw new ArgumentException("Selection no Valid", nameof(selection));
            }
            return selection;
        }
        public static string VerifyAnswerMultipleChoice(QuestionMultipleChoice questionMultipleChoice, string input)
        {
            var availableOptions = new string[] { "a", "b", "c", "d" };

            do
            {
                if (availableOptions.Contains(input))
                    return questionMultipleChoice.MultipleAnswersMap[input];
                else
                {
                    Console.ForegroundColor = ConsoleColor.DarkYellow;
                    Console.WriteLine("Please enter a valid value [a, b, c, or d]");
                    Console.ResetColor();
                    input = RequestValue();
                }
            } while (true);
        }
    }
}
