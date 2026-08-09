using Biblioteca.Web.Data; using Biblioteca.Web.Models.Entities; using Microsoft.EntityFrameworkCore;
namespace Biblioteca.Web.Services;
public class CategoriaService(ApplicationDbContext context) : ICategoriaService
{
 public Task<List<Categoria>> ListarAsync()=>context.Categorias.AsNoTracking().OrderBy(x=>x.Nombre).ToListAsync();
 public Task<Categoria?> ObtenerAsync(int id)=>context.Categorias.AsNoTracking().FirstOrDefaultAsync(x=>x.Id==id);
 public Task<bool> ExisteNombreAsync(string nombre,int? excluir=null)=>context.Categorias.AnyAsync(x=>x.Nombre==nombre && (!excluir.HasValue || x.Id!=excluir));
 public async Task CrearAsync(Categoria c){context.Categorias.Add(c);await context.SaveChangesAsync();}
 public async Task<bool> ActualizarAsync(Categoria c){var actual=await context.Categorias.FindAsync(c.Id);if(actual is null)return false;actual.Nombre=c.Nombre;actual.Descripcion=c.Descripcion;await context.SaveChangesAsync();return true;}
 public async Task<(bool,string?)> EliminarAsync(int id){var c=await context.Categorias.Include(x=>x.Libros).FirstOrDefaultAsync(x=>x.Id==id);if(c is null)return(false,null);if(c.Libros.Count>0)return(false,"No se puede eliminar porque tiene libros asociados.");context.Remove(c);await context.SaveChangesAsync();return(true,null);}
}
