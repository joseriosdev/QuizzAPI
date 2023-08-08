﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizGame.Models;
using QuizGame.Models.DTOs;
using QuizGame.Services;

namespace RESTAPI.Controllers
{
    [Route("api/[controller]")]
    [Authorize]
    [ApiController]
    public class QuizController : ControllerBase
    {
        private readonly IQuizServices _services;

        public QuizController(IQuizServices services)
        {
            _services = services ?? throw new ArgumentNullException(nameof(services));
        }

        /// <summary>
        /// Get the list of quizzes
        /// </summary>
        /// <returns>A list of quizzes</returns>
        /// <response code="200">Success response</response>
        /// <response code="400">Bad request response</response>
        [HttpGet]
        public List<Quiz> GetQuizzes()
        {
            return _services.GenerateQuizzesAsync();
        }

        /// <summary>
        /// Get a quiz by its id
        /// </summary>
        /// <param name="id">A quiz id</param>
        /// <returns>A quiz</returns>
        /// <response code="200">Success response</response>
        /// <response code="400">Bad request response</response>
        [HttpGet("{id}")]
        public async Task<Quiz> GetQuiz(Guid id)
        {
            return await _services.GetQuizAsync(id);
        }

        /// <summary>
        /// Create a quiz
        /// </summary>
        /// <param name="quiz">A quiz object</param>
        /// <returns>A quiz object</returns>
        /// <response code="200">Success response</response>
        /// <response code="400">Bad request response</response>
        [HttpPost]
        public async Task<Quiz> PostQuestion([FromBody] QuizDTO quiz)
        {
            return await _services.AddQuizAsync(quiz);
        }

        /// <summary>
        /// Delete a quiz from the database
        /// </summary>
        /// <param name="idToDelete">A quiz id</param>
        /// <response code="200">Success response</response>
        /// <response code="400">Bad request response</response>
        [HttpDelete("{idToDelete}")]
        public void DeleteQuiz(Guid idToDelete)
        {
            _services.RemoveQuizAsync(idToDelete);
        }

        /// <summary>
        /// Update a quiz from the database
        /// </summary>
        /// <param name="id">A quiz id</param>
        /// <response code="200">Success response</response>
        /// <response code="400">Bad request response</response>
        [HttpPut("{idToUpdate}")]
        public void UpdateQuiz(Guid idToUpdate, [FromBody] QuizDTO quiz)
        {
            _services.UpdateQuizAsync(idToUpdate, quiz);
        }
    }
}
