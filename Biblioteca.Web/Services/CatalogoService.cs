using Biblioteca.Web.Data;
using Biblioteca.Web.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
namespace Biblioteca.Web.Services;
public class CatalogoService(ApplicationDbContext context) : ICatalogoService
{
    public async Task<CatalogoViewModel> ObtenerCatalogoAsync(string? busqueda, int? categoriaId)
    {
        var consulta = context.Libros.AsNoTracking().AsQueryable();
        if (!string.IsNullOrWhiteSpace(busqueda))
        {
            var texto = busqueda.Trim();
            consulta = consulta.Where(x => x.Titulo.Contains(texto) || x.Autor.Nombre.Contains(texto));
        }
        if (categoriaId.HasValue) consulta = consulta.Where(x => x.CategoriaId == categoriaId.Value);
        return new CatalogoViewModel
        {
            Busqueda=busqueda, CategoriaId=categoriaId,
            Libros=await consulta.OrderBy(x=>x.Titulo).Select(x=>new LibroCatalogoItemViewModel{Id=x.Id,Titulo=x.Titulo,Autor=x.Autor.Nombre,Categoria=x.Categoria.Nombre,EjemplaresDisponibles=x.EjemplaresDisponibles}).ToListAsync(),
            Categorias=await context.Categorias.AsNoTracking().OrderBy(x=>x.Nombre).Select(x=>new SelectListItem(x.Nombre,x.Id.ToString())).ToListAsync()
        };
    }
    public Task<LibroDetalleViewModel?> ObtenerDetalleAsync(int id) => context.Libros.AsNoTracking().Where(x=>x.Id==id).Select(x=>new LibroDetalleViewModel{Id=x.Id,Titulo=x.Titulo,Autor=x.Autor.Nombre,Categoria=x.Categoria.Nombre,Isbn=x.Isbn,Sinopsis=x.Sinopsis,AnioPublicacion=x.AnioPublicacion,CantidadEjemplares=x.CantidadEjemplares,EjemplaresDisponibles=x.EjemplaresDisponibles}).FirstOrDefaultAsync();
}
