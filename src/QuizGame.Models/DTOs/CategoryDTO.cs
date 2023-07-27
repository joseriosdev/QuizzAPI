using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizGame.Models.DTOs
{
    public class CategoryDTO
    {
        public string Id { get; set; }
        [Required(ErrorMessage = "You should provide a name value.")]
        public string Name { get; set; }

        public CategoryDTO()
        {
        }
        public CategoryDTO(string id, string name)
        {
            Id = id;
            Name = name;
        }

        public CategoryDTO(string name)
        {
            Id = Guid.NewGuid().ToString();
            Name = name;
        }
    }
}
