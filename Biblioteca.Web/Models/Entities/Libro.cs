using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace Biblioteca.Web.Models.Entities;

public class Libro
{
    public int Id { get; set; }
    [Required][StringLength(150)] public string Titulo { get; set; } = string.Empty;
    [StringLength(20)] public string? Isbn { get; set; }
    [StringLength(1200)] public string? Sinopsis { get; set; }
    public int AnioPublicacion { get; set; }
    public int CantidadEjemplares { get; set; }
    public int EjemplaresDisponibles { get; set; }
    public int AutorId { get; set; }
    [ValidateNever]
    public Autor Autor { get; set; } = null!;
    public int CategoriaId { get; set; }
    [ValidateNever]
    public Categoria Categoria { get; set; } = null!;
}
