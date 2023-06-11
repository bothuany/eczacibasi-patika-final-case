using Microsoft.AspNetCore.Mvc;
using StockTrackingApp.Business.Dto.Stock;
using StockTrackingApp.Business.Interface;
using System.Net;

namespace StockTrackingApp.Controllers
{
    [ApiController]
    [Route("api/stocks")]
    public class StockController : Controller
    {
        private readonly IStockService _stockService;

        public StockController(IStockService stockService)
        {
            _stockService = stockService;
        }

        [HttpPost]
        public IActionResult Add(CreateStockDto createStockDto)
        {
            var result = _stockService.Add(createStockDto);

            if (result.Succeed)
            {
                return CreatedAtAction(nameof(GetById), new { id = result.Result.Id }, result.Result);
            }

            return BadRequest(result.ErrorMessage);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _stockService.Delete(id);

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
        public IActionResult GetAll(int page = 0, int pageSize = -1)
        {
            var result = _stockService.GetAll(page, pageSize);

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
        public IActionResult GetById(int id)
        {
            var result = _stockService.GetById(id);

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

        [HttpGet("product/{productId}")]
        public IActionResult GetAllByProductId(int productId)
        {
            var result = _stockService.GetAllByProductId(productId);

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
        public IActionResult Search(string productName, int? sizeId, int? colorId, bool? getOutOfStocks)
        {
            var result = _stockService.Search(productName, sizeId, colorId, getOutOfStocks);

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
        public IActionResult Update(UpdateStockDto updateStockDto)
        {
            var result = _stockService.Update(updateStockDto);

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

        [HttpPatch]
        public IActionResult UpdateQuantity(UpdateStockQuantityDto updateStockQuantityDto)
        {
            var result = _stockService.UpdateQuantity(updateStockQuantityDto);

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


