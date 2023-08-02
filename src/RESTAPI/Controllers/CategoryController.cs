using Microsoft.AspNetCore.Mvc;
using QuizGame.Models.DTOs;
using QuizGame.Models;
using QuizGame.Services;
using Microsoft.AspNetCore.Authorization;
using System.Collections.Generic;

namespace RESTAPI.Controllers
{
    //[Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController
    {
        private readonly IQuizServices _services;

        public CategoryController(IQuizServices services)
        {
            _services = services ?? throw new ArgumentNullException(nameof(services));
        }

        /// <summary>
        /// Get the list of categories
        /// </summary>
        /// <returns>A list of categories</returns>
        /// <response code="200">Success response</response>
        /// <response code="400">Bad request response</response>
        [HttpGet]
        public async Task<IEnumerable<Category>> GetCategories()
        {
            var categories = await _services.GenerateCategoriesAsync();
            return categories;
        }

        /// <summary>
        /// Get a category by its id
        /// </summary>
        /// <param name="id">A category id</param>
        /// <returns>A category</returns>
        /// <response code="200">Success response</response>
        /// <response code="400">Bad request response</response>
        [HttpGet("{id}")]
        public async Task<Category> GetCategory(Guid id)
        {
            return await _services.GetCategoryAsync(id);
        }

        /// <summary>
        /// Create a category
        /// </summary>
        /// <param name="category">A category object</param>
        /// <returns>A category object</returns>
        /// <response code="200">Success response</response>
        /// <response code="400">Bad request response</response>
        [HttpPost]
        public async Task<Category> PostCategory(CategoryDTO category)
        {
            return await _services.AddCategoryAsync(category);
        }

        /// <summary>
        /// Delete a category from the database
        /// </summary>
        /// <param name="id">A category id</param>
        /// <response code="200">Success response</response>
        /// <response code="400">Bad request response</response>
        [HttpDelete]
        public void DeleteCategory(Guid id)
        {
            _services.RemoveCategoryAsync(id);
        }

        /// <summary>
        /// Update a category from the database
        /// </summary>
        /// <param name="idToUpdate">A category id</param>
        /// <response code="200">Success response</response>
        /// <response code="400">Bad request response</response>
        [HttpPut("{idToUpdate}")]
        public void UpdateCategory(Guid idToUpdate, [FromBody] CategoryDTO category)
        {
            _services.UpdateCategoryAsync(idToUpdate, category);
        }

    }
}
