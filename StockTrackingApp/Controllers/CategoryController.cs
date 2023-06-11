using Microsoft.AspNetCore.Mvc;
using StockTrackingApp.Business.Dto.Category;
using StockTrackingApp.Business.Interface;
using System.Net;

namespace StockTrackingApp.Controllers
{
    [ApiController]
    [Route("api/categories")]
    public class CategoryController : Controller
    {
        private readonly ICategoryService _categoryService;

        public CategoryController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpPost]
        public IActionResult Add(CreateCategoryDto createCategoryDto)
        {
            var result = _categoryService.Add(createCategoryDto);

            if (result.Succeed)
            {
                return CreatedAtAction(nameof(GetById), new { id = result.Result.Id }, result.Result);
            }

            return BadRequest(result.ErrorMessage);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _categoryService.Delete(id);

            if (result.Succeed)
            {
                return Ok(result.Result);
            }

            if (result.ErrorCode == (int)HttpStatusCode.NotFound)
            {
                return NotFound(result.ErrorMessage);
            }

            return BadRequest(result.ErrorMessage);
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var result = _categoryService.GetAll();

            if (result.Succeed)
            {
                return Ok(result.Result);
            }

            if (result.ErrorCode == (int)HttpStatusCode.NotFound)
            {
                return NotFound(result.ErrorMessage);
            }

            return BadRequest(result.ErrorMessage);
        }

        [HttpGet("{id}")]
        public IActionResult GetById(int id, bool withProducts = true)
        {
            var result = _categoryService.GetById(id, withProducts);

            if (result.Succeed)
            {
                return Ok(result.Result);
            }

            if (result.ErrorCode == (int)HttpStatusCode.NotFound)
            {
                return NotFound(result.ErrorMessage);
            }

            return BadRequest(result.ErrorMessage);
        }

        [HttpGet("name/{name}")]
        public IActionResult GetByName(string name, bool withProducts = true)
        {
            var result = _categoryService.GetByName(name, withProducts);

            if (result.Succeed)
            {
                return Ok(result.Result);
            }

            if (result.ErrorCode == (int)HttpStatusCode.NotFound)
            {
                return NotFound(result.ErrorMessage);
            }

            return BadRequest(result.ErrorMessage);
        }

        [HttpPut]
        public IActionResult Update(UpdateCategoryDto updateCategoryDto)
        {
            var result = _categoryService.Update(updateCategoryDto);

            if (result.Succeed)
            {
                return Ok(result.Result);
            }

            if (result.ErrorCode == (int)HttpStatusCode.NotFound)
            {
                return NotFound(result.ErrorMessage);
            }

            return BadRequest(result.ErrorMessage);
        }
    }
}
