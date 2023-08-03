using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using QuizGame.Models;
using QuizGame.Models.DTOs;
using QuizGame.Services;

namespace RESTAPI.Controllers
{
    [Route("api/[controller]")]
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
        public async Task DeleteQuiz(Guid idToDelete)
        {
            await _services.RemoveQuizAsync(idToDelete);
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

        /// <summary>
        /// Handles Pagination
        /// </summary>
        /// <param page="page">The current page</param>
        /// <param pageSize="pageSize">The number of items to show per page</param>
        /// <response code="200">Success response</response>
        /// <response code="400">Bad request response</response>
        /// <response code="404">Not Found response</response>
        [HttpGet("{page}/{pageSize}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Quiz>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        [ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public async Task<IActionResult> GetPaginationQuiz(int page, int pageSize)
        {
            if (page <= 0)
            {
                return BadRequest("The current page should be 1 or greater");
            }

            try
            {
                var res = await _services.HandlePaginationAsync(page, pageSize);
                return Ok(res);
            }
            catch (Exception ex)
            {
                return NotFound(ex.Message);
            }
        }

        /// <summary>
        /// Searcher (base on name)
        /// </summary>
        /// <param searchText="searchText">The text to search the name of the quiz or quizes</param>
        /// <response code="200">Success response</response>
        /// <response code="204">If nothing is found</response>
        /// <response code="400">Bad request response</response>
        [HttpGet("search/{searchText}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<Quiz>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public async Task<IActionResult> SearchQuizesByName(string searchText)
        {
            if (searchText.IsNullOrEmpty())
                return BadRequest("Null or empty");

            var result = await _services.QuizSearcherByNameAsync(searchText);
            return result.Count() != 0 ? Ok(result) : NoContent();
        }
    }
}
