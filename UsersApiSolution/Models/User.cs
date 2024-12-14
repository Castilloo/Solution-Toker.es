using System.ComponentModel.DataAnnotations;

namespace UsersApiSolution.Models;

public class User
{
    [Key]
    public Guid Id { get; set; }
    [Required]
    public string Nombre { get; set; }
    [Required]
    public string Telefono { get; set; }
}