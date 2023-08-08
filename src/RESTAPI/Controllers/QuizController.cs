using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using QuizGame.Models;
using QuizGame.Models.DTOs;
using QuizGame.Models.Utils;
using QuizGame.Services;
using System.Text.Json;
using System.Xml.Linq;

namespace RESTAPI.Controllers
{
    /// <summary>
    /// Quiz Controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class QuizController : ControllerBase
    {
        private readonly IQuizServices _services;

        /// <summary>
        /// Quiz Controller Constructor
        /// </summary>
        public QuizController(IQuizServices services)
        {
            _services = services ?? throw new ArgumentNullException(nameof(services));
        }

        /// <summary>
        /// Get the list of quizzes
        /// </summary>
        /// <returns>A list of quizzes</returns>
        /// <response code="200">Success response</response>
        /// <response code="204">No Content</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Quiz>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetQuizzes(
            [FromQuery] string[] categories,
            int page = 1,
            int pageSize = 50,
            string searchText = ""
        )
        {
            var (quizes, paginationMetadata) = await _services
                .GetQuizesAsync(categories, searchText, page, pageSize);
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(paginationMetadata));
            return quizes.Count() == 0 ? NoContent() : Ok(quizes);
        }

        /// <summary>
        /// Get a quiz by its id
        /// </summary>
        /// <param name="id">A quiz id</param>
        /// <returns>A quiz</returns>
        /// <response code="200">Success response</response>
        /// <response code="404">Not Found response</response>
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Quiz))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetQuiz(Guid id)
        {
            Quiz result = await _services.GetQuizAsync(id);
            if (result is null)
                return NotFound();
            return Ok(result);
        }

        /// <summary>
        /// Create a quiz
        /// </summary>
        /// <param name="quiz">A quiz object</param>
        /// <returns>A quiz object</returns>
        /// <response code="200">Success response</response>
        /// <response code="400">Bad request response</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Quiz))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> PostQuiz([FromBody] QuizDTO quiz)
        {
            if(quiz is null)
                return BadRequest("Please, send a correct body");

            Quiz result = await _services.AddQuizAsync(quiz);
            return Ok(result);
        }

        /// <summary>
        /// Delete a quiz from the database
        /// </summary>
        /// <param name="idToDelete">A quiz id</param>
        /// <response code="200">Success response</response>
        [HttpDelete("{idToDelete}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteQuiz(Guid idToDelete)
        {
            await _services.RemoveQuizAsync(idToDelete);
            return Ok();
        }

        /// <summary>
        /// Update a quiz from the database
        /// </summary>
        /// <param name="idToUpdate">A quiz id</param>
        /// <param name="quiz">A quizDTO</param>
        /// <response code="200">Success response</response>
        [HttpPut("{idToUpdate}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string))]
        public async Task<IActionResult> UpdateQuiz(Guid idToUpdate, [FromBody] QuizDTO quiz)
        {
            await _services.UpdateQuizAsync(idToUpdate, quiz);
            return Ok("update successful");
        }
    }
}
