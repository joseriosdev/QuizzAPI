using QuizGame.DB;
using QuizGame.Models;
using QuizGame.Models.DTOs;

namespace QuizGame.Services
{
    public class QuizServices : IQuizServices
    {
        public DB_Fake _db = new DB_Fake();

        public List<Category> GenerateCategories()
        {
            return _db._categories;
        }

        //QUESTIONS

        public List<Question> GenerateQuestions()
        {
            return _db._questions;
        }

        public Question GetQuestion(Guid id)
        {
            return _db._questions.Where(questions => questions.Id == id).FirstOrDefault();
        }

        public Question AddQuestion(Question question)
        {
            _db._questions.Add(question);
            return question;
        }
        
        public bool RemoveQuestion(Guid id) 
        {  
            Question question = GetQuestion(id);
            _db._questions.Remove(question); 
            return true;
        }

        //QUIZZES

        public List<Quiz> GenerateQuizzes()
        {
            return _db._quizzes;
        }

        public Quiz GetQuiz(Guid id)
        {
            return _db._quizzes.Where(quizzes => quizzes.Id == id).FirstOrDefault();
        }

        public Quiz AddQuiz(QuizDTO quiz)
        {
            Quiz quizToAdd = new Quiz(Guid.Parse(quiz.Id),quiz.QuizName,quiz.Description,quiz.Categories,quiz.questions);
            _db._quizzes.Add(quizToAdd);
            return quizToAdd;
        }

        public bool RemoveQuiz(Guid id)
        {
            Quiz quiz = GetQuiz(id);
            _db._quizzes.Remove(quiz);
            return true;
        }

        public Quiz UpdateQuiz(Guid id, QuizDTO quiz)
        {
            RemoveQuiz(id);
            Quiz quizToAdd = new Quiz(Guid.Parse(quiz.Id), quiz.QuizName, quiz.Description, quiz.Categories, quiz.questions);
            _db._quizzes.Add(quizToAdd);
            return quizToAdd;
        }
    }
    
}