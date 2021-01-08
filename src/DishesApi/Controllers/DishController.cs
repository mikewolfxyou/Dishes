using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DishesApi.DataAccess.Dish;
using DishesApi.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Serilog;

namespace DishesApi.Controllers
{
    [ApiController]
    [Route("dish-management")]
    public class DishController : ControllerBase
    {
        private readonly IDishRepository _dishRepository;
        private readonly ILogger _logger;

        public DishController(IDishRepository dishRepository, ILogger logger)
        {
            _dishRepository = dishRepository;
            _logger = logger;
        }
        
        [HttpPost("dish")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IStatusCodeActionResult> Create(DishDto dishDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                if (await _dishRepository.UpsertAsync(dishDto))
                {
                    return Accepted();
                }

                return BadRequest();
            }
            catch (ArgumentException exception)
            {
                return BadRequest(exception.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpGet("dish")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IStatusCodeActionResult> Get([FromQuery] IEnumerable<string> dishIds)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                return Ok(await _dishRepository.GetAsync(dishIds));
            }
            catch (ArgumentException exception)
            {
                return BadRequest(exception.Message);
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpDelete("dish")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IStatusCodeActionResult> Delete([FromQuery] IEnumerable<string> dishIds)
        {
            try
            {
                if (await _dishRepository.DeleteAsync(dishIds))
                {
                    return Accepted();
                }
                
                return BadRequest();
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}