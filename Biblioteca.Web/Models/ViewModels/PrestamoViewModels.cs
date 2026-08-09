
using Microsoft.AspNetCore.Mvc.Rendering;
namespace Biblioteca.Web.Models.ViewModels;

public class PrestamoListItemViewModel

{
    public int Id { get; set; }
    public string LibroTitulo { get; set; } = string.Empty;
    public string UsuarioCorreo { get; set; } = string.Empty;
    public DateTime FechaPrestamo { get; set; }
    public DateTime FechaDevolucionEsperada { get; set; }
    public DateTime? FechaDevolucionReal { get; set; }
    public bool Devuelto => FechaDevolucionReal.HasValue;

    public int? DiasAtraso
    {
        get
        {
            if (Devuelto)
            {
                var dias = (FechaDevolucionReal!.Value.Date - FechaDevolucionEsperada.Date).Days;
                return dias > 0 ? dias : null;
            }

            var diasHoy = (DateTime.Now.Date - FechaDevolucionEsperada.Date).Days;
            return diasHoy > 0 ? diasHoy : null;
        }
    }

}

public class PrestamoCreateViewModel

{
    public int LibroId { get; set; }
    public string UsuarioId { get; set; } = string.Empty;
    public DateTime FechaDevolucionEsperada { get; set; } = DateTime.Now.AddDays(7);
    public List<SelectListItem> Libros { get; set; } = [];
    public List<SelectListItem> Usuarios { get; set; } = [];

}