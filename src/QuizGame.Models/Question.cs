using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizGame.Models
{
    public class Question
    {
        public Guid Id { get; set; }
        [Required(ErrorMessage = "You should provide a text value.")]
        public string Text { get; set; }
        [Required(ErrorMessage = "You should provide a correct answer value.")]
        public string CorrectAnswer { get; set; }
        [Range(0, int.MaxValue, ErrorMessage = "Please enter a value bigger than {0}")]
        public int Score { get; set; }
        public Guid QuizAssigned { get; set; }

        public Question(Guid id, string text, string correctAnswer, int score)
        {
            Id = id;
            Text = text;
            CorrectAnswer = correctAnswer;
            Score = score;
        }

        public Question(Guid id, string text, string correctAnswer, int score, Guid quizAssigned)
        {
            Id = id;
            Text = text;
            CorrectAnswer = correctAnswer;
            Score = score;
            QuizAssigned = quizAssigned;
        }
        public Question()
        {
        }

        public override string ToString()
        {
            StringBuilder sb = new();
            sb.AppendLine("Text: " + Text);
            sb.AppendLine("Score: " + Score);
            sb.AppendLine("CorrectAnswer: " + CorrectAnswer);
            return sb.ToString();
        }
    }
}
