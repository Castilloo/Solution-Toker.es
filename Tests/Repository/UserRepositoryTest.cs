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
            // Crear un DbContext en memoria para simular la base de datos
            _context = new ApiContext(new DbContextOptionsBuilder<ApiContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options);

            // Crear un mock de ILogger
            _logger = A.Fake<ILogger<UserRepository>>();

            // Crear una instancia del repositorio con las dependencias mockeadas
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
            Assert.Equal(1, result);  // Si se guardó el usuario, se espera que `SaveChangesAsync` devuelva 1.
        }

        //[Fact]
        //public async Task CreateUserAsync_ShouldReturnZero_WhenPhoneIsInvalid()
        //{
        //    // Arrange
        //    var user = new User
        //    {
        //        Nombre = "Juan",
        //        Telefono = "invalid_phone"
        //    };

        //    // Act
        //    var result = await _userRepository.CreateUserAsync(user);

        //    // Assert
        //    Assert.Equal(0, result);  // Se espera que el resultado sea 0 si el teléfono no es válido.
        //    A.CallTo(() => _logger.LogError(A<string>.Ignored)).MustHaveHappened();  // Verifica que se haya registrado un error
        //}

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

        //[Fact]
        //public async Task GetUsersAsync_ShouldThrowException_WhenDbFails()
        //{
        //    // Simula un fallo en la base de datos
        //    var faultyContext = A.Fake<ApiContext>();
        //    var users = await faultyContext.Users.ToListAsync();

        //    A.CallTo(() => users).Throws(new Exception("Database failure"));

        //    var faultyRepository = new UserRepository(faultyContext, _logger);

        //    // Act & Assert
        //    await Assert.ThrowsAsync<Exception>(() => faultyRepository.GetUsersAsync());
        //}
    }
}
