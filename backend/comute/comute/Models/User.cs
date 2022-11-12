using System.ComponentModel.DataAnnotations;

namespace comute.Models;
public class User
{
    [Key]
    public int UserId { get; set; }
    [Required]
    public string Name { get; set; }
    [Required]
    public string Surname { get; set; } 
    public string? Phone { get; set; }
    [Required]
    public string Email { get; set; }
    [Required]
    public string Password { get; set; }

    public string Role { get; set; } = "User";
    public DateTime CreatedOn { get; set; } = DateTime.Now;

    public User() { }
    public User(
        int userId,
        string name,
        string surname,
        string? phone,
        string email,
        string password,
        string role,
        DateTime createdOn)
    {
        UserId = userId;
        Name = name;
        Surname = surname;
        Phone = phone;
        Email = email;
        Password = password;
        Role = role;
        CreatedOn = createdOn;
    }
}
