using UsersApiSolution.Models;

namespace UsersApiSolution;

public interface IUserRepository
{
    Task<ICollection<User>> GetUsersAsync();
    Task<int> CreateUserAsync(User user);
}
