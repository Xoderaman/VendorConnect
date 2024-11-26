using System.ComponentModel.DataAnnotations;

public class UserRegistrationModel
{
    public int Id { get; set; }

    public required string Username { get; set; }


    public required string Email { get; set; }

    
    public required string Password { get; set; } // Store hashed password, not plain text
}
