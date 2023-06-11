using Microsoft.AspNetCore.Mvc;
using StockTrackingApp.Business.Dto.Size;
using StockTrackingApp.Business.Interface;
using System.Net;

namespace StockTrackingApp.Controllers
{
    [ApiController]
    [Route("api/sizes")]
    public class SizeController : Controller
    {
        private readonly ISizeService _sizeService;

        public SizeController(ISizeService sizeService)
        {
            _sizeService = sizeService;
        }

        [HttpPost]
        public IActionResult Add(CreateSizeDto createSizeDto)
        {
            var result = _sizeService.Add(createSizeDto);

            if (result.Succeed)
            {
                return CreatedAtAction(nameof(GetById), new { id = result.Result.Id }, result.Result);
            }

            return BadRequest(result.ErrorMessage);
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            var result = _sizeService.Delete(id);

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
            var result = _sizeService.GetAll();

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
            var result = _sizeService.GetById(id);

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
        public IActionResult GetByName(string name)
        {
            var result = _sizeService.GetByName(name);

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
        public IActionResult Update(UpdateSizeDto updateSizeDto)
        {
            var result = _sizeService.Update(updateSizeDto);

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
