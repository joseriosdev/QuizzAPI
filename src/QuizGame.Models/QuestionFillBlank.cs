using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizGame.Models
{
    public class QuestionFillBlank : Question
    {
        [Range(0, int.MaxValue, ErrorMessage = "Please enter valid integer Number")]
        public int BlankPosition { get; set; }
        public QuestionFillBlank(Guid id, string text, string correctAnswer, int score, int blankPosition) : base(id, text, correctAnswer, score)
        { 
            Id = id;
            Text = text;
            CorrectAnswer = correctAnswer;
            Score = score;
            BlankPosition = blankPosition;
        }

        public QuestionFillBlank(Guid id, string text, string correctAnswer, int score, int blankPosition, Guid quizAssigned) : base(id, text, correctAnswer, score, quizAssigned)
        {
            Id = id;
            Text = text;
            CorrectAnswer = correctAnswer;
            Score = score;
            BlankPosition = blankPosition;
            QuizAssigned = quizAssigned;
        }

        public void PrintQuestion()
        {
            Console.WriteLine("---------------------");
            Console.WriteLine("# "+Text);
            Console.WriteLine("Type your Answer");
        }
    }
}
