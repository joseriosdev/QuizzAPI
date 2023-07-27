using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizGame.Models
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public Category(Guid id, string name)
        {
            Id = id;
            Name = name;
        }

        public Category(string name)
        {
            Id = Guid.NewGuid();
            Name = name;
        }

        public Category() { }
    }
}
