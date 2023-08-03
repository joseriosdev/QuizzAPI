using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Security;
using System.Text;
using System.Threading.Tasks;

namespace QuizGame.Models
{
    public class Quiz
    {
        private static readonly Random random = new Random();
        public Guid Id { get; set; }
        public string QuizName { get; set; }
        public string Description { get; set; }
        public List<Category> Categories { get; set; } = new List<Category>();
        public List<Question> questions { get; set; } = new List<Question>();

        public Quiz()
        {
            string[] quizNames = new string[] {"Abra kadabra", "Sesamo", "Bookshell", "Aww!", "QuizzApp" };
            string quizName = quizNames[RandomInt(quizNames.Length-1)];

            Id = Guid.NewGuid();
            QuizName = quizName;
            Description = quizNames[RandomInt(quizNames.Length - 1)] + " is the description part." + quizNames[RandomInt(quizNames.Length - 1)];
            Categories = new List<Category>();
            questions = new List<Question>();
        }

        public Quiz(string quizName, string description, List<Category> categories, List<Question> questionsList) 
        {
            Id = Guid.NewGuid();
            QuizName = quizName;
            Description = description;
            Categories = categories;
            questions = questionsList;
        }

        public Quiz(Guid id, string quizName, string description, List<Category> categories, List<Question> questionsList)
        {
            Id = id;
            QuizName = quizName;
            Description = description;
            Categories = categories;
            questions = questionsList;
        }

        public override bool Equals(object? obj)
        {
            return obj is Quiz quiz &&
                   Id.Equals(quiz.Id) &&
                   QuizName == quiz.QuizName &&
                   Description == quiz.Description &&
                   EqualityComparer<List<Category>>.Default.Equals(Categories, quiz.Categories) &&
                   EqualityComparer<List<Question>>.Default.Equals(questions, quiz.questions);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, QuizName, Description, Categories, questions);
        }

        private int RandomInt(int max)
        {
            int range = max + 1;
            int randomNumber = random.Next(range);
            return randomNumber;
        }
    }
}
