using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizGame.Models
{
    internal class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Score { get; set; }

        public User(string name, string email) 
        {
            Name = name;
            Email = email;
        }
    }
}
