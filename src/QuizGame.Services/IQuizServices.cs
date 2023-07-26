using QuizGame.Models;
using QuizGame.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizGame.Services
{
    public interface IQuizServices
    {
        List<Question> GenerateQuestions();
        List<Category> GenerateCategories();
        List<Quiz> GenerateQuizzes();
        public Question GetQuestion(Guid id);
        public Question AddQuestion(Question question);
        public bool RemoveQuestion(Guid id);

        public Quiz GetQuiz(Guid id);
        public Quiz AddQuiz(QuizDTO quiz);
        public bool RemoveQuiz(Guid id);

        public Quiz UpdateQuiz(Guid id, QuizDTO quiz);
    }
}
