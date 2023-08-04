using QuizGame.Models;
using QuizGame.Models.DTOs;
using QuizGame.Models.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizGame.Services
{
    public interface IQuizServices
    {
        List<Question> GenerateQuestionsAsync();
        Task<IEnumerable<Category>> GenerateCategoriesAsync();
        List<Quiz> GenerateQuizzesAsync();

        Task<Question> GetQuestionAsync(Guid? id);
        Task<Question> AddQuestionAsync(Question question);
        Task<bool> RemoveQuestionAsync(Guid id);
        Task<Question> UpdateQuestionAsync(Guid id, Question question);

        Task<Quiz> GetQuizAsync(Guid? id);
        Task<Quiz> AddQuizAsync(QuizDTO quiz);
        Task<bool> RemoveQuizAsync(Guid id);
        Task<Quiz> UpdateQuizAsync(Guid id, QuizDTO quiz);
        Task<(IEnumerable<Quiz>, PaginationMetadata)> GetQuizesAsync(
            string[] categories,
            string? searchText,
            int currentPage = 1,
            int itemsPerPage = 2
            );

        Task<Category> GetCategoryAsync(Guid id);
        Task<Category> AddCategoryAsync(CategoryDTO category);
        Task<bool> RemoveCategoryAsync(Guid id);
        Task<Category> UpdateCategoryAsync(Guid id, CategoryDTO category);
    }
}
