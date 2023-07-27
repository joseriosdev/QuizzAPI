using QuizGame.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QuizGame.Models.Utils
{
    public class Parser
    {
        public Category ParseToCategory(CategoryDTO categoryDTO)
        {
            Category category;
            if (categoryDTO.Id != null)
            {
                category = new Category(Guid.Parse(categoryDTO.Id), categoryDTO.Name);
            }
            else
            {
                category = new Category(categoryDTO.Name);
            }
            return category;
        }

        public CategoryDTO ParseToCategoryDTO(Category category)
        {
            CategoryDTO categoryDTO;
            if (category.Id != null)
            {
                categoryDTO = new CategoryDTO(category.Id.ToString(), category.Name);
            }
            else
            {
                categoryDTO = new CategoryDTO(category.Name);
            }
            return categoryDTO;
        }

        public Quiz ParseToQuiz(QuizDTO quizDTO)
        {
            Quiz quiz;
            if (quizDTO.Id != null)
            {
                quiz = new Quiz(Guid.Parse(quizDTO.Id), quizDTO.QuizName, quizDTO.Description, quizDTO.Categories, quizDTO.questions);
            }
            else
            {
                quiz = new Quiz(quizDTO.QuizName, quizDTO.Description, quizDTO.Categories, quizDTO.questions);
            }
            return quiz;
        }

        public QuizDTO ParseToQuizDTO(Quiz quiz)
        {
            QuizDTO quizDTO;
            if (quiz.Id != null)
            {
                quizDTO = new QuizDTO(quiz.Id.ToString(), quiz.QuizName, quiz.Description, quiz.Categories, quiz.questions);
            }
            else
            {
                quizDTO = new QuizDTO(quiz.QuizName, quiz.Description, quiz.Categories, quiz.questions);
            }
            return quizDTO;
        }
    }
}
