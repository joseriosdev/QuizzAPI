using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizGame.Models.DTOs
{
    public class QuizDTO
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "You should provide a name value.")]
        [MaxLength(50)]
        public string QuizName { get; set; }
        [Required(ErrorMessage = "You should provide a description value.")]
        [MaxLength(100)]
        public string Description { get; set; }
        public List<Category> Categories { get; set; } = new List<Category>();
        [MinLength(1, ErrorMessage = "You should provide at least 1 question")]
        public List<Question> questions { get; set; } = new List<Question>();

        public QuizDTO(string quizName, string description, List<Category> categories, List<Question> questionsList)
        {
            Id = Guid.NewGuid().ToString();
            QuizName = quizName;
            Description = description;
            Categories = categories;
            questions = questionsList;
        }

        public QuizDTO(string id, string quizName, string description, List<Category> categories, List<Question> questionsList)
        {
            Id = id;
            QuizName = quizName;
            Description = description;
            Categories = categories;
            questions = questionsList;
        }
        public QuizDTO()
        {
        }
    }
}
