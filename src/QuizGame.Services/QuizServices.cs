using QuizGame.DB;
using QuizGame.Models;
using QuizGame.Models.DTOs;
using QuizGame.Models.Utils;

namespace QuizGame.Services
{
    public class QuizServices : IQuizServices
    {
        public DB_Fake _db = new DB_Fake();
        public Parser parser = new Parser();
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

        public Question UpdateQuestion(Guid id, Question question)
        {
            throw new NotImplementedException();
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
            Quiz quizToAdd = parser.ParseToQuiz(quiz);
            _db._quizzes.Add(quizToAdd);
            return quizToAdd;
        }

        public bool RemoveQuiz(Guid id)
        {
            Quiz quiz = GetQuiz(id);
            _db._quizzes.Remove(quiz);
            List<Question> questions = GenerateQuestions().Where(question => question.QuizAssigned == id).ToList();
            if (questions.Count > 0)
            {
                foreach (Question question in questions)
                {
                    question.QuizAssigned = null;
                    RemoveQuestion(question.Id);
                    AddQuestion(question);
                }
            }
            return true;
        }

        public Quiz UpdateQuiz(Guid id, QuizDTO quiz)
        {
            RemoveQuiz(id);
            Quiz quizToAdd = parser.ParseToQuiz(quiz);
            _db._quizzes.Add(quizToAdd);
            return quizToAdd;
        }

        //CATEGORIES

        public Category GetCategory(Guid id)
        {
            return _db._categories.Where(categories => categories.Id == id).FirstOrDefault();
        }

        public Category AddCategory(CategoryDTO category)
        {
            Category categoryToAdd = parser.ParseToCategory(category);
            _db._categories.Add(categoryToAdd);
            return categoryToAdd;
        }

        public bool RemoveCategory(Guid id)
        {
            Category category = GetCategory(id);
            _db._categories.Remove(category);
            List<Quiz> quizzes = GenerateQuizzes().Where(quiz => quiz.Categories.Any(categoryToUpdate => categoryToUpdate.Id == id)).ToList();
            if (quizzes.Count > 0)
            {
                foreach (Quiz quiz in quizzes)
                {
                    quiz.Categories.RemoveAll(obj => obj.Id == id);
                    QuizDTO quizToUpdate = parser.ParseToQuizDTO(quiz);
                    UpdateQuiz(quiz.Id, quizToUpdate);
                }
            }
            return true;
        }

        public Category UpdateCategory(Guid id, CategoryDTO category)
        {
            Category currentCategory = GetCategory(id);
            _db._categories.Remove(currentCategory);
            Category categoryToUpdate = parser.ParseToCategory(category);
            _db._categories.Add(categoryToUpdate);
            List<Quiz> quizzes = GenerateQuizzes().Where(quiz => quiz.Categories.Any(categoryToUpdate => categoryToUpdate.Id == id)).ToList();
            if (quizzes.Count > 0)
            {
                foreach (Quiz quiz in quizzes)
                {
                    quiz.Categories.RemoveAll(obj => obj.Id == id);
                    List<Category> newCategories = new List<Category>(quiz.Categories);
                    newCategories.Add(categoryToUpdate);
                    quiz.Categories = newCategories;
                    QuizDTO quizToUpdate = parser.ParseToQuizDTO(quiz);
                    UpdateQuiz(quiz.Id, quizToUpdate);
                }
            }
            return categoryToUpdate;
        }

    }
    
}