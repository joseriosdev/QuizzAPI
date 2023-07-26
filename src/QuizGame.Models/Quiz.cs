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
        public Guid Id { get; set; }
        [Required(ErrorMessage = "You should provide a name value.")]
        [MaxLength(50)]
        public string QuizName { get; set; }
        [Required(ErrorMessage = "You should provide a description value.")]
        [MaxLength(100)]
        public string Description { get; set; }
        public List<Category> Categories { get; set; } = new List<Category>();
        [MinLength(1, ErrorMessage = "You should provide at least 1 question")]
        public List<Question> questions { get; set; } = new List<Question>();

        private static int currentID = 0;

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

    }
}
