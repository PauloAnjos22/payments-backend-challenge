using System.ComponentModel.DataAnnotations;
using Payments.Backend.Challenge.Domain.Enums;

namespace Payments.Backend.Challenge.Application.DTOs;

public class RegisterUserRequestDto
{
    [Required(ErrorMessage = "User name is required")]
    [MinLength(1)]
    public string? FullName { get; set; }
    [Required(ErrorMessage = "Cpf is required")]
    public string? Cpf { get; set; }    
    [Required(ErrorMessage = "Email is required")]
    public string? Email { get; set; } 
    [Required(ErrorMessage = "Password is required")]
    public string? Password { get; set; } 
    [Required(ErrorMessage = "User type is required")]
    public UserType Type { get; set; } 
}