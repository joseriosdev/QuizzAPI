using QuizGame.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizGame.DB
{
    public class DB_Fake
    {

        public static Guid guid1 = Guid.NewGuid();
        public static Guid guid2 = Guid.NewGuid();
        public static Guid guid3 = Guid.NewGuid();
        public static Guid guid4 = Guid.NewGuid();
        public static Guid guid5 = Guid.NewGuid();
        public static Guid guid6 = Guid.NewGuid();
        public static Guid guid7 = Guid.NewGuid();
        public static Guid guid8 = Guid.NewGuid();
        public static Guid guid9 = Guid.NewGuid();
        public static Guid guid10 = Guid.NewGuid();

        public List<Question> _questions = new List<Question>
        {
            new QuestionFillBlank(guid3, "The Pyramids of Giza are located in _____.", "egypt", 100 , 38, guid1),
            new QuestionMultipleChoice(guid4, "What is the color of the 2nd 'G' in Google?", "blue", 100, new List<string> {"Red","Green","Blue","Yellow" }, guid1),
            new QuestionFillBlank(guid5, "In the city of ______ MA, United States. Basketball were invented.", "springfield", 100, 16, guid2),
            new QuestionMultipleChoice(guid6, "Which country won the Fifa World Cup in 2014?", "germany", 100, new List<string> {"Argentina","France","Italy","Germany" }, guid2),
        };
        public List<Category> _categories = new List<Category>
        {
            new Category {Id= guid7, Name = "History" },
            new Category {Id= guid8, Name = "Science" },
            new Category {Id= guid9, Name = "Sports" },
            new Category {Id= guid10, Name = "Entertainment" }
        };

        public List<Quiz> _quizzes = new List<Quiz>
            {
            new Quiz(
                guid1,
                "World War II Quizz",
                "Some Description about WWII",
                new List<Category>
                {
                    new Category {Id= guid9, Name = "Sports" },
                    new Category {Id= guid10, Name = "Entertainment" }
                },
                new List<Question>
                {
                   new QuestionFillBlank(guid3, "The Pyramids of Giza are located in _____.", "egypt", 100 , 38, guid1),
                   new QuestionMultipleChoice(guid4, "What is the color of the 2nd 'G' in Google?", "blue", 100, new List<string> {"Red","Green","Blue","Yellow" }, guid1),
                })
                ,
            new Quiz(
                guid2,
                "Sports Basics Quizz",
                "Introduction to sports",
                new List<Category>
                {
                    new Category {Id= guid7, Name = "History" },
                    new Category {Id= guid8, Name = "Science" },
                },
                new List<Question>
                    {
                        new QuestionFillBlank(guid5, "In the city of ______ MA, United States. Basketball were invented.", "springfield", 100, 16, guid2),
                        new QuestionMultipleChoice(guid6, "Which country won the Fifa World Cup in 2014?", "germany", 100, new List<string> {"Argentina","France","Italy","Germany" }, guid2),
                    }
                )
            };
    }
}
