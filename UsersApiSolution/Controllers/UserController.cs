using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using UsersApiSolution;
using UsersApiSolution.Models;

namespace MyApp.Namespace
{
    [ApiController]
    public class UserController : ControllerBase
    {
        public readonly IUserRepository _repository;
        public readonly ILogger<UserController> _logger;
        public UserController(IUserRepository repository, ILogger<UserController> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        [HttpGet]
        [Route("users")]
        public async Task<ActionResult> GetUsers()
        {
            try
            {
                var users = await _repository.GetUsersAsync();
                if (users.Count > 0) _logger.LogInformation("---Usuarios obtenidos---");
                return Ok(users);
            }
            catch (Exception ex)
            {
                _logger.LogError("--- Error consulta de usuarios ---");
                return BadRequest(ex.Message);
            }
        }


        [HttpPost]
        [Route("new-user")]
        public async Task<ActionResult> CreateUser([FromBody] UserDto user)
        {
            try
            {
                var newUser = new User
                {
                    Id = Guid.NewGuid(),
                    Nombre = user.Nombre,
                    Telefono = user.Telefono
                };

                int datosGuardados = await _repository.CreateUserAsync(newUser);


                return datosGuardados > 0
                    ? CreatedAtAction(nameof(CreateUser), new { mensaje = "Los datos fueron recibidos correctamente" })
                    : BadRequest(new { mensaje = "Datos no recibidos, revise el numero de teléfono" });
            } catch (Exception ex)
            {
                _logger.LogError("--- Error al guardar los usuarios ---");
                return BadRequest(ex.Message);
            }

        }

    }
}
