using FakeItEasy;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyApp.Namespace;
using UsersApiSolution;
using UsersApiSolution.Models;

namespace Tests.Controller
{
    public class UserControllerTest
    {
        private readonly IUserRepository _repository;
        private readonly UserController _controller;
        public readonly ILogger<UserController> _logger;

        public UserControllerTest()
        {
            _repository = A.Fake<IUserRepository>();
            _logger = A.Fake<ILogger<UserController>>();

            _controller = new UserController(_repository, _logger);
        }

        private static UserDto CreateFakeUserDto() => A.Fake<UserDto>();
       
        //Create 
        //returns Created(success) | BadRequest(fails) action results
        [Fact]
        public async void UserController_CreateUser_ReturnCreated()
        {
            //Arrange
            var userDto = CreateFakeUserDto();

            var user = new User
            {
                Id = Guid.NewGuid(),
                Nombre = userDto.Nombre,
                Telefono = userDto.Telefono
            };
            //Act
            A.CallTo(() => _repository
                .CreateUserAsync(A<User>.That.Matches(
                    u => u.Nombre == user.Nombre && u.Telefono == user.Telefono
                    )))
                .Returns(1);

            var result = (CreatedAtActionResult) await _controller.CreateUser(userDto);
            
            //Assert
            result.StatusCode.Should().Be(StatusCodes.Status201Created);
            result.Should().NotBeNull();
        }

        [Fact]
        public async void UserController_GetUsers_ReturnOk()
        {
            //Arrange
            var users = A.Fake<ICollection<User>>();

            //Act
            A.CallTo(() => _repository.GetUsersAsync()).Returns(users);
            var result = (OkObjectResult)await _controller.GetUsers();
            
            //Assert
            result.StatusCode.Should().Be(StatusCodes.Status200OK);
            result.Should().NotBeNull();
        }
    }
}
