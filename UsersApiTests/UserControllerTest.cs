namespace UsersApiTests;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.Logging;
using MyApp.Namespace;
using UsersApiSolution.Models;
using UsersApiSolution;

public class UserControllerTest
{
    private readonly Mock<IUserRepository> _mockRepository;
    private readonly Mock<ILogger<UserController>> _mockLogger;
    private readonly UserController _controller;

    public UserControllerTests()
    {
        // Inicializamos los mocks
        _mockRepository = new Mock<IUserRepository>();
        _mockLogger = new Mock<ILogger<UserController>>();

        // Creamos el controlador pasando los mocks
        _controller = new UserController(_mockRepository.Object, _mockLogger.Object);
    }

    [Fact]
    public async Task GetUsers_ReturnsOkResult_WhenUsersExist()
    {
        // Arrange
        var users = new List<User>
            {
                new User { Id = Guid.NewGuid(), Nombre = "John", Telefono = "123456789" },
                new User { Id = Guid.NewGuid(), Nombre = "Jane", Telefono = "987654321" }
            };

        _mockRepository.Setup(repo => repo.GetUsers()).ReturnsAsync(users);

        // Act
        var result = await _controller.GetUsers();

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnUsers = Assert.IsType<List<User>>(okResult.Value);
        Assert.Equal(2, returnUsers.Count);
    }

    [Fact]
    public async Task GetUsers_ReturnsBadRequest_WhenExceptionOccurs()
    {
        // Arrange
        _mockRepository.Setup(repo => repo.GetUsers()).ThrowsAsync(new Exception("Error al obtener los usuarios"));

        // Act
        var result = await _controller.GetUsers();

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Error al obtener los usuarios", badRequestResult.Value);
    }

    [Fact]
    public async Task CreateUser_ReturnsOkResult_WhenUserCreatedSuccessfully()
    {
        // Arrange
        var userDto = new UserDto { Nombre = "Alice", Telefono = "123456789" };
        var newUser = new User { Id = Guid.NewGuid(), Nombre = "Alice", Telefono = "123456789" };

        _mockRepository.Setup(repo => repo.CreateUser(It.IsAny<User>())).ReturnsAsync(1); // Simulamos que se guardó correctamente

        // Act
        var result = await _controller.CreateUser(userDto);

        // Assert
        var okResult = Assert.IsType<OkObjectResult>(result);
        var returnValue = Assert.IsType<dynamic>(okResult.Value);
        Assert.Equal("Los datos fueron recibidos correctamente", returnValue.mensaje);
    }

    [Fact]
    public async Task CreateUser_ReturnsBadRequest_WhenUserNotCreated()
    {
        // Arrange
        var userDto = new UserDto { Nombre = "Bob", Telefono = "123456789" };
        var newUser = new User { Id = Guid.NewGuid(), Nombre = "Bob", Telefono = "123456789" };

        _mockRepository.Setup(repo => repo.CreateUser(It.IsAny<User>())).ReturnsAsync(0); // Simulamos que no se guardó

        // Act
        var result = await _controller.CreateUser(userDto);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        var returnValue = Assert.IsType<dynamic>(badRequestResult.Value);
        Assert.Equal("Datos no recibidos, revise el numero de teléfono", returnValue.mensaje);
    }

    [Fact]
    public async Task CreateUser_ReturnsBadRequest_WhenExceptionOccurs()
    {
        // Arrange
        var userDto = new UserDto { Nombre = "Charlie", Telefono = "123456789" };
        _mockRepository.Setup(repo => repo.CreateUser(It.IsAny<User>())).ThrowsAsync(new Exception("Error al crear el usuario"));

        // Act
        var result = await _controller.CreateUser(userDto);

        // Assert
        var badRequestResult = Assert.IsType<BadRequestObjectResult>(result);
        Assert.Equal("Error al crear el usuario", badRequestResult.Value);
    }
}