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
        public string QuizName { get; set; }
        public string Description { get; set; }
        public List<Category> Categories { get; set; } = new List<Category>();
        public List<Question> questions { get; set; } = new List<Question>();

        private static int currentID = 0;

        public QuizDTO()
        {
        }
    }
}
