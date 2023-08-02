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
        private readonly ILogger<QuestionController> _logger;

        public QuestionController(IQuizServices services, ILogger<QuestionController> log)
        {
            _services = services ?? throw new ArgumentNullException(nameof(services));
            _logger = log;
        }

        /// <summary>
        /// Get the list of questions
        /// </summary>
        /// <returns>A list of questions</returns>
        /// <response code="200">Success response</response>
        /// <response code="400">Bad request response</response>
        [HttpGet]
        public List<Question> GetQuestions()
        {
            return _services.GenerateQuestionsAsync();
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
            var question = await _services.GetQuestionAsync(id);
            return Ok(question);
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
            var result = await _services.AddQuestionAsync(question);
            return Ok(result);
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
            await _services.RemoveQuestionAsync(id);
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
            await _services.UpdateQuestionAsync(idToUpdate, question);
        }

    }
}
