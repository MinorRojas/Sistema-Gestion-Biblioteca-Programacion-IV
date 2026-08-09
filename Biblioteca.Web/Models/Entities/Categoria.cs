using System.ComponentModel.DataAnnotations;
namespace Biblioteca.Web.Models.Entities;
public class Categoria
{
    public int Id { get; set; }
    [Required(ErrorMessage="El nombre es obligatorio.")][StringLength(80)] public string Nombre { get; set; } = string.Empty;
    [StringLength(300)] public string? Descripcion { get; set; }
    public ICollection<Libro> Libros { get; set; } = new List<Libro>();
}
