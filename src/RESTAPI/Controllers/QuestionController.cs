using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using QuizGame.Services;
using QuizGame.Models;

namespace RESTAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private readonly IQuizServices _services;

        public QuestionController(IQuizServices services)
        {
            _services = services ?? throw new ArgumentNullException(nameof(services));
        }

        /// <summary>
        /// Get the list of questions
        /// </summary>
        /// <returns>A list of questions</returns>
        /// <response code="200">Success response</response>
        /// <response code="400">Bad request response</response>
        [HttpGet]
        public async Task<IActionResult> GetQuestions()
        {
            await Task.Delay(1000);
            return Ok(_services.GenerateQuestions());
        }

        /// <summary>
        /// Get a question by its id
        /// </summary>
        /// <param name="id">A question id</param>
        /// <returns>A question</returns>
        /// <response code="200">Success response</response>
        /// <response code="400">Bad request response</response>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetQuestion(Guid id)
        {
            await Task.Delay(1000);
            return Ok(_services.GetQuestion(id));
        }

        /// <summary>
        /// Create a question
        /// </summary>
        /// <param name="quiz">A question object</param>
        /// <returns>A question object</returns>
        /// <response code="200">Success response</response>
        /// <response code="400">Bad request response</response>
        [HttpPost]
        public async Task<IActionResult> PostQuestion(Question question)
        {
            await Task.Delay(1000);
            return Ok(_services.AddQuestion(question));
        }

        /// <summary>
        /// Delete a question from the database
        /// </summary>
        /// <param name="id">A question id</param>
        /// <response code="200">Success response</response>
        /// <response code="400">Bad request response</response>
        [HttpDelete]
        public async Task DeleteQuestion(Guid id) 
        {
            await Task.Delay(1000);
            _services.RemoveQuestion(id);
        }

        /// <summary>
        /// Update a question from the database
        /// </summary>
        /// <param name="id">A question id</param>
        /// <response code="200">Success response</response>
        /// <response code="400">Bad request response</response>
        [HttpPut("{idToUpdate}")]
        public async Task UpdateQuestion(Guid idToUpdate, [FromBody] Question question)
        {
             await Task.Delay(1000);
            _services.UpdateQuestion(idToUpdate, question);
        }

    }
}
