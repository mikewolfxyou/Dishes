using System;
using System.Threading.Tasks;
using DishesApi.DataAccess.Restaurant;
using DishesApi.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Serilog;

namespace DishesApi.Controllers
{
    [ApiController]
    [Route("restaurant-management")]
    public class RestaurantController :  ControllerBase
    {
        private readonly IRestaurantRepository _restaurantRepository;
        private readonly ILogger _logger;

        public RestaurantController(IRestaurantRepository restaurantRepository, ILogger logger)
        {
            _restaurantRepository = restaurantRepository;
            _logger = logger;
        }

        [HttpPost("restaurant")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IStatusCodeActionResult> Create(RestaurantDto restaurantDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                //TODO better create a model for response and create contract test
                return Ok("{\"restaurantId\": \"" 
                                + await _restaurantRepository.UpsertAsync(restaurantDto)
                                + "\"");

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

        [HttpGet("restaurant")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IStatusCodeActionResult> Get()
        {
            try
            {
                return Ok(await _restaurantRepository.GetAsync());
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
        
        [HttpGet("restaurant-id")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IStatusCodeActionResult> Get([FromQuery] string restaurantId)
        {
            try
            {
                return Ok(await _restaurantRepository.GetAsync(restaurantId));
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

        [HttpDelete("restaurant")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status202Accepted)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IStatusCodeActionResult> Delete(string restaurantId)
        {
            try
            {
                if (await _restaurantRepository.DeleteAsync(restaurantId))
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