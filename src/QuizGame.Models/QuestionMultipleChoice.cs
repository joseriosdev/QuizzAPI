using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizGame.Models
{
    public class QuestionMultipleChoice : Question
    {
        public List<string> Choices { get; set; }
        public readonly Dictionary<string, string> MultipleAnswersMap = new Dictionary<string, string>();
        [MinLength(2, ErrorMessage = "You should provide at least 2 options")]
        private Dictionary<int, string> Options = new Dictionary<int, string>
        {
            {0, "a) " },
            {1, "b) " },
            {2, "c) " },
            {3, "d) " }
        };

        public QuestionMultipleChoice(Guid id, string text, string correctAnswer, int score, List<string> choices) : base(id, text, correctAnswer, score)
        {
            Id = id;
            Text = text;
            CorrectAnswer = correctAnswer;
            Score = score;
            Choices = choices;
            for (int i = 0; i < choices.Count; i++)
                MultipleAnswersMap.Add(Options[i][0].ToString().ToLower(), choices[i].ToLower());
        }

        public QuestionMultipleChoice(Guid id, string text, string correctAnswer, int score, List<string> choices, Guid quizAssigned) : base(id, text, correctAnswer, score, quizAssigned)
        {
            Id = id;
            Text = text;
            CorrectAnswer = correctAnswer;
            Score = score;
            Choices = choices;
            for (int i = 0; i < choices.Count; i++)
                MultipleAnswersMap.Add(Options[i][0].ToString().ToLower(), choices[i].ToLower());
            QuizAssigned = quizAssigned;
        }

        public void PrintQuestion()
        {
            Console.WriteLine("---------------------");
            Console.WriteLine("# " + Text);
            for (var i = 0; i < Choices.Count(); i++)
                Console.WriteLine($"{Options[i]} {Choices[i]}");
            Console.WriteLine("Type your Answer");
            Console.WriteLine("a b c d");
        }
    }
}
