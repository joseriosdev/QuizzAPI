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
        [Required(ErrorMessage = "You should provide a name value.")]
        public string Name { get; set; }
    }
}
