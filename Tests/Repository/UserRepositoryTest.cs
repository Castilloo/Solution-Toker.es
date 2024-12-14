using FakeItEasy;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using UsersApiSolution;
using UsersApiSolution.Models;

namespace Tests.Repository
{
    public class UserRepositoryTest
    {
        private readonly UserRepository _userRepository;
        private readonly ApiContext _context;
        private readonly ILogger<UserRepository> _logger;

        public UserRepositoryTest()
        {
            _context = new ApiContext(new DbContextOptionsBuilder<ApiContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options);

            _logger = A.Fake<ILogger<UserRepository>>();

            _userRepository = new UserRepository(_context, _logger);
        }

        [Fact]
        public async Task CreateUserAsync_ShouldReturnChanges_WhenPhoneIsValid()
        {
            // Arrange
            var user = new User
            {
                Nombre = "Juan",
                Telefono = "+1234567890"
            };

            // Act
            var result = await _userRepository.CreateUserAsync(user);

            // Assert
            Assert.Equal(1, result);  
        }

        [Fact]
        public async Task GetUsersAsync_ShouldReturnUsers()
        {
            // Arrange
            var user1 = new User { Nombre = "Juan", Telefono = "+1234567890" };
            var user2 = new User { Nombre = "Ana", Telefono = "+0987654321" };

            await _context.Users.AddAsync(user1);
            await _context.Users.AddAsync(user2);
            await _context.SaveChangesAsync();

            // Act
            var users = await _userRepository.GetUsersAsync();

            // Assert
            Assert.NotNull(users);
            Assert.Equal(2, users.Count);
        }

    }
}
