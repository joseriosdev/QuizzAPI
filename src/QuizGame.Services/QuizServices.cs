using QuizGame.DB;
using QuizGame.Models;
using QuizGame.Models.DTOs;
using QuizGame.Models.Utils;
using System.Linq;
using System.Xml.Linq;

namespace QuizGame.Services
{
    public class QuizServices : IQuizServices
    {
        public DB_Fake _db = new DB_Fake();
        public Parser parser = new Parser();
        public async Task<IEnumerable<Category>> GenerateCategoriesAsync()
        {
            return _db._categories;
        }

        //QUESTIONS

        public List<Question> GenerateQuestionsAsync()
        {
            return _db._questions;
        }

        public async Task<Question> GetQuestionAsync(Guid id)
        {
            await Task.Delay(10);
            var question = _db._questions.Where(questions => questions.Id == id).FirstOrDefault();
            return question;
        }

        public async Task<Question> AddQuestionAsync(Question question)
        {
            _db._questions.Add(question);
            await Task.Delay(10);
            return question;
        }
        
        public async Task<bool> RemoveQuestionAsync(Guid id) 
        {
            Question question = await GetQuestionAsync(id);
            _db._questions.Remove(question);
            List<Quiz> quizzes = GenerateQuizzesAsync().Where(quiz => quiz.questions.Any(question => question.Id == id)).ToList();
            if (quizzes.Count > 0)
            {
                foreach (Quiz quiz in quizzes)
                {
                    quiz.questions.RemoveAll(obj => obj.Id == id);
                    QuizDTO quizToUpdate = parser.ParseToQuizDTO(quiz);
                    await UpdateQuizAsync(quiz.Id, quizToUpdate);
                }
            }
            await Task.Delay(10);
            return true;
        }

        public async Task<Question> UpdateQuestionAsync(Guid id, Question question)
        {
            Question currentQuestion = await GetQuestionAsync(id);
            _db._questions.Remove(currentQuestion);
            _db._questions.Add(question);
            List<Quiz> quizzes = GenerateQuizzesAsync().Where(quiz => quiz.questions.Any(questionToUpdate => questionToUpdate.Id == id)).ToList();
            if (quizzes.Count > 0)
            {
                foreach (Quiz quiz in quizzes)
                {
                    quiz.questions.RemoveAll(obj => obj.Id == id);
                    List<Question> newQuestions = new List<Question>(quiz.questions);
                    newQuestions.Add(question);
                    quiz.questions = newQuestions;
                    QuizDTO quizToUpdate = parser.ParseToQuizDTO(quiz);
                    await UpdateQuizAsync(quiz.Id, quizToUpdate);
                }
            }
            await Task.Delay(10);
            return question;
        }

        //QUIZZES

        public List<Quiz> GenerateQuizzesAsync()
        {
            return _db._quizzes;
        }

        public async Task<Quiz> GetQuizAsync(Guid id)
        {
            await Task.Delay(10);
            return _db._quizzes.Where(quizzes => quizzes.Id == id).FirstOrDefault();
        }

        public async Task<Quiz> AddQuizAsync(QuizDTO quiz)
        {
            Quiz quizToAdd = parser.ParseToQuiz(quiz);
            _db._quizzes.Add(quizToAdd);
            await Task.Delay(10);
            return quizToAdd;
        }

        public async Task<bool> RemoveQuizAsync(Guid id)
        {
            Quiz quiz = await GetQuizAsync(id);
            _db._quizzes.Remove(quiz);
            List<Question> questions = GenerateQuestionsAsync().Where(question => question.QuizAssigned == id).ToList();
            if (questions.Count > 0)
            {
                foreach (Question question in questions)
                {
                    question.QuizAssigned = null;
                    await RemoveQuestionAsync(question.Id);
                    await AddQuestionAsync(question);
                }
            }
            return true;
        }

        public async Task<Quiz> UpdateQuizAsync(Guid id, QuizDTO quiz)
        {
            await RemoveQuizAsync(id);
            Quiz quizToAdd = parser.ParseToQuiz(quiz);
            _db._quizzes.Add(quizToAdd);
            await Task.Delay(10);
            return quizToAdd;
        }

        public async Task<IEnumerable<Quiz>> HandlePaginationAsync(int page, int pageSize)
        {
            await Task.Delay(10);
            int currentQuizCountInDB = _db._quizzes.Count;
            int maxNumberOfPages = (int)Math.Ceiling((decimal)currentQuizCountInDB / (decimal)pageSize);
            
            if(maxNumberOfPages == 1)
            {
                return _db._quizzes;
            }

            IEnumerable<Quiz> itemsInPage = _db._quizzes
                .Skip((page - 1) * pageSize)
                .Take(pageSize);

            if (itemsInPage.Count() == 0)
                throw new Exception("Not Found");

            return itemsInPage;
        }

        public async Task<IEnumerable<Quiz>> QuizSearcherByNameAsync(string name)
        {
            IEnumerable<Quiz> result = _db._quizzes.Where(quiz => quiz.QuizName.Contains(name));
            await Task.Delay(10);
            return result;
        }

        public async Task<IEnumerable<Quiz>> FilterQuizesByCategoriesAsync(params string[] categories)
        {
            IEnumerable<Quiz> result = _db._quizzes
                .Where(q => q.Categories
                    .Any(c => categories.Contains(c.Name))
                );
            await Task.Delay(10);
            return result;
        }

        public async Task<(IEnumerable<Quiz>, PaginationMetadata)> GetQuizesAsync(
            string[] categories,
            string? searchText,
            int currentPage = 1,
            int itemsPerPage = 2
            )
        {
            await Task.Delay(10);
            IEnumerable<Quiz> result = _db._quizzes;
            if (!string.IsNullOrWhiteSpace(searchText))
            {
                searchText = searchText.Trim();
                result = result.Where(q => q.QuizName == searchText);
            }
            if(categories.Length > 0)
            {
                result = result
                .Where(q => q.Categories
                    .Any(c => categories.Contains(c.Name))
                );
            }

            int totalItemCount = result.Count();
            var paginationMetadata = new PaginationMetadata(
                totalItemCount, itemsPerPage, currentPage);
            result = result
                .Skip((currentPage - 1) * itemsPerPage)
                .Take(itemsPerPage);

            return (result, paginationMetadata);
        }

        //CATEGORIES

        public async Task<Category> GetCategoryAsync(Guid id)
        {
            await Task.Delay(10);
            return _db._categories.Where(categories => categories.Id == id).FirstOrDefault();
        }

        public async Task<Category> AddCategoryAsync(CategoryDTO category)
        {
            Category categoryToAdd = parser.ParseToCategory(category);
            _db._categories.Add(categoryToAdd);
            await Task.Delay(10);
            return categoryToAdd;
        }

        public async Task<bool> RemoveCategoryAsync(Guid id)
        {
            Category category = await GetCategoryAsync(id);
            _db._categories.Remove(category);
            List<Quiz> quizzes = GenerateQuizzesAsync().Where(quiz => quiz.Categories.Any(categoryToUpdate => categoryToUpdate.Id == id)).ToList();
            if (quizzes.Count > 0)
            {
                foreach (Quiz quiz in quizzes)
                {
                    quiz.Categories.RemoveAll(obj => obj.Id == id);
                    QuizDTO quizToUpdate = parser.ParseToQuizDTO(quiz);
                    UpdateQuizAsync(quiz.Id, quizToUpdate);
                }
            }
            return true;
        }

        public async Task<Category> UpdateCategoryAsync(Guid id, CategoryDTO category)
        {
            Category currentCategory = await GetCategoryAsync(id);
            _db._categories.Remove(currentCategory);
            Category categoryToUpdate = parser.ParseToCategory(category);
            _db._categories.Add(categoryToUpdate);
            List<Quiz> quizzes = GenerateQuizzesAsync().Where(quiz => quiz.Categories.Any(categoryToUpdate => categoryToUpdate.Id == id)).ToList();
            if (quizzes.Count > 0)
            {
                foreach (Quiz quiz in quizzes)
                {
                    quiz.Categories.RemoveAll(obj => obj.Id == id);
                    List<Category> newCategories = new List<Category>(quiz.Categories);
                    newCategories.Add(categoryToUpdate);
                    quiz.Categories = newCategories;
                    QuizDTO quizToUpdate = parser.ParseToQuizDTO(quiz);
                    UpdateQuizAsync(quiz.Id, quizToUpdate);
                }
            }
            return categoryToUpdate;
        }

    }
    
}