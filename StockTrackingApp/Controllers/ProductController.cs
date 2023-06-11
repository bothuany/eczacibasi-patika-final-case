using Microsoft.AspNetCore.Mvc;
using StockTrackingApp.Business.Dto.Product;
using StockTrackingApp.Business.Dto.Stock;
using StockTrackingApp.Business.Interface;
using System.Net;

namespace StockTrackingApp.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : Controller
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        public IActionResult Add(CreateProductDto createProductDto)
        {
            var result = _productService.Add(createProductDto);

            if (result.Succeed)
            {
                return CreatedAtAction(nameof(GetById), new { id = result.Result.Id }, result.Result);
            }

            return BadRequest(result.ErrorMessage);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _productService.Delete(id);

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
            var result = _productService.GetById(id, withProducts);

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

        [HttpGet("search")]
        public IActionResult Search(string name, int? categoryId, int? brandId, double? minPrice, int? sizeId, int? colorId, bool withStocks = true, int page = 0, int pageSize = -1)
        {
            var result = _productService.Search(name, categoryId, brandId, minPrice, sizeId, colorId, withStocks, page, pageSize);

            if (result.Succeed)
            {
                return Ok(result.Result);
            }

            return BadRequest(result.ErrorMessage);
        }

        [HttpPut]
        public IActionResult Update(UpdateProductDto updateProductDto)
        {
            var result = _productService.Update(updateProductDto);

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

