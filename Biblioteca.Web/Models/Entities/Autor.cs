using System.ComponentModel.DataAnnotations;
namespace Biblioteca.Web.Models.Entities;
public class Autor
{
    public int Id { get; set; }
    [Required(ErrorMessage="El nombre es obligatorio.")][StringLength(100)] public string Nombre { get; set; } = string.Empty;
    [StringLength(1000)] public string? Biografia { get; set; }
    public ICollection<Libro> Libros { get; set; } = new List<Libro>();
}
