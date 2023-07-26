using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizGame.Models
{
    public class Collection
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public List<Quiz> Quizzes { get; set; }

        public Collection() { }
        public Collection(string name, string description, List<Quiz> quizzes) 
        { 
            Name = name;
            Description = description; 
            Quizzes = quizzes;
        }

    }
}
