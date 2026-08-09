
using System.ComponentModel.DataAnnotations;
namespace Biblioteca.Web.Models.Entities;

public class Prestamo
{
    public int Id { get; set; }

    public int LibroId { get; set; }

    public Libro Libro { get; set; } = null!;

    [Required] public string UsuarioId { get; set; } = string.Empty;

    public DateTime FechaPrestamo { get; set; } = DateTime.Now;

    public DateTime FechaDevolucionEsperada { get; set; }

    public DateTime? FechaDevolucionReal { get; set; }

    public bool Devuelto => FechaDevolucionReal.HasValue;

}