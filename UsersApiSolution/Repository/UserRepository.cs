using Microsoft.EntityFrameworkCore;
using System.Text.RegularExpressions;
using UsersApiSolution.Models;

namespace UsersApiSolution;

public class UserRepository : IUserRepository
{
    private readonly ApiContext _context;
    private readonly ILogger<UserRepository> _logger;

    public UserRepository(ApiContext context, ILogger<UserRepository> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<int> CreateUserAsync(User? user)
    {
        const string pattern = @"^\+\d+$|^\d+$";
        try
        {
            bool isNumber = Regex.IsMatch(user!.Telefono ?? "", pattern);

            if (!isNumber)
            {
                _logger.LogError($"--- El teléfono \"{user.Telefono}\" no es un número de teléfono ---");
                return 0;
            };

            await _context.AddAsync(user);
            int changes = await _context.SaveChangesAsync();

            _logger.LogInformation($"--- Mensaje de bienvenida enviado a \"{user.Nombre}\" al número de téléfono \"{user.Telefono}\" ---");

            return changes;
        }
        catch(Exception ex)
        {
            throw new Exception("Error en la consulta: ", ex);
        }
    }

    public async Task<ICollection<User>> GetUsersAsync()
    {
        try
        {
            return await _context.Users.ToListAsync();
        }
        catch (Exception ex)
        {
            throw new Exception("Error en la consulta: ", ex);
        }
    }
}
