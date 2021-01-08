using System;
using System.Threading.Tasks;
using DishesApi.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Serilog;

namespace DishesApi.Controllers
{
    [ApiController]
    [Route("restaurant-menu")]
    public class MenuController : ControllerBase
    {
        private readonly IMenuRepository _menuRepository;
        private readonly ILogger _logger;

        public MenuController(IMenuRepository menuRepository, ILogger logger)
        {
            _menuRepository = menuRepository;
            _logger = logger;
        }

        [HttpGet("menu")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IStatusCodeActionResult> Get([FromQuery]string restaurantId)
        {
            try
            {
                return Ok(await _menuRepository.GetAsync(restaurantId));
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
    }
}