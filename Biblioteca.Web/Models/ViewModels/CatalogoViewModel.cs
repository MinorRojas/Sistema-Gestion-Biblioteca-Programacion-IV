using Microsoft.AspNetCore.Mvc.Rendering;
namespace Biblioteca.Web.Models.ViewModels;
public class CatalogoViewModel
{
    public string? Busqueda { get; set; }
    public int? CategoriaId { get; set; }
    public List<LibroCatalogoItemViewModel> Libros { get; set; } = [];
    public List<SelectListItem> Categorias { get; set; } = [];
}
public class LibroCatalogoItemViewModel
{
    public int Id { get; set; } public string Titulo { get; set; } = string.Empty; public string Autor { get; set; } = string.Empty;
    public string Categoria { get; set; } = string.Empty; public int EjemplaresDisponibles { get; set; }
    public bool Disponible => EjemplaresDisponibles > 0;
}
public class LibroDetalleViewModel
{
    public int Id { get; set; } public string Titulo { get; set; } = string.Empty; public string Autor { get; set; } = string.Empty;
    public string Categoria { get; set; } = string.Empty; public string? Isbn { get; set; } public string? Sinopsis { get; set; }
    public int AnioPublicacion { get; set; } public int CantidadEjemplares { get; set; } public int EjemplaresDisponibles { get; set; }
    public bool Disponible => EjemplaresDisponibles > 0;
}
