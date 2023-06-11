using Microsoft.AspNetCore.Mvc;
using StockTrackingApp.Business.Dto.Brand;
using StockTrackingApp.Business.Interface;
using System.Net;

namespace StockTrackingApp.Controllers
{
    [ApiController]
    [Route("api/brands")]
    public class BrandController : Controller
    {
        private readonly IBrandService _brandService;

        public BrandController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        [HttpPost]
        public IActionResult Add(CreateBrandDto createBrandDto)
        {
            var result = _brandService.Add(createBrandDto);

            if (result.Succeed)
            {
                return CreatedAtAction(nameof(GetById), new { id = result.Result.Id }, result.Result);
            }

            return BadRequest(result.ErrorMessage);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _brandService.Delete(id);

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
            var result = _brandService.GetAll();

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
        public IActionResult GetById(int id, bool withProducts = false)
        {
            var result = _brandService.GetById(id, withProducts);

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
        public IActionResult GetByName(string name, bool withProducts = false)
        {
            var result = _brandService.GetByName(name, withProducts);

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
        public IActionResult Update(UpdateBrandDto updateBrandDto)
        {
            var result = _brandService.Update(updateBrandDto);

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

