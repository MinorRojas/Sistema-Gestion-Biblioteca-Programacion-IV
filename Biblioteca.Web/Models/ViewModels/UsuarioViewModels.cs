using System.ComponentModel.DataAnnotations;

namespace Biblioteca.Web.Models.ViewModels;

public class UsuarioCrearViewModel
{
    [Required(ErrorMessage = "El correo es obligatorio.")]
    [EmailAddress(ErrorMessage = "Ingrese un correo válido.")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "La contraseña es obligatoria.")]
    [DataType(DataType.Password)]
    [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres.")]
    public string Password { get; set; } = string.Empty;

    [Display(Name = "Administrador")]
    public bool EsAdministrador { get; set; }
}

public class UsuarioEditarViewModel
{
    public string Id { get; set; } = string.Empty;

    [Required(ErrorMessage = "El correo es obligatorio.")]
    [EmailAddress(ErrorMessage = "Ingrese un correo válido.")]
    public string Email { get; set; } = string.Empty;

    [DataType(DataType.Password)]
    [MinLength(6, ErrorMessage = "La contraseña debe tener al menos 6 caracteres.")]
    [Display(Name = "Nueva contraseña")]
    public string? NuevaPassword { get; set; }

    [Display(Name = "Administrador")]
    public bool EsAdministrador { get; set; }
}
