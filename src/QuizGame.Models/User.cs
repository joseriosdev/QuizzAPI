using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizGame.Models
{
    public class User
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int Score { get; set; } = 0;
        public string Password { get; set; }

        public User(Guid id, string name, string email, string pass)
        {
            Name = name;
            Email = email;
            Password = pass;
            Id = id;
        }
    }
}
