using Biblioteca.Web.Data; using Biblioteca.Web.Models.Entities; using Microsoft.EntityFrameworkCore;
namespace Biblioteca.Web.Services;
public class AutorService(ApplicationDbContext context) : IAutorService
{
 public Task<List<Autor>> ListarAsync()=>context.Autores.AsNoTracking().OrderBy(x=>x.Nombre).ToListAsync();
 public Task<Autor?> ObtenerAsync(int id)=>context.Autores.AsNoTracking().FirstOrDefaultAsync(x=>x.Id==id);
 public Task<bool> ExisteNombreAsync(string nombre,int? excluir=null)=>context.Autores.AnyAsync(x=>x.Nombre==nombre && (!excluir.HasValue || x.Id!=excluir));
 public async Task CrearAsync(Autor autor){context.Autores.Add(autor);await context.SaveChangesAsync();}
 public async Task<bool> ActualizarAsync(Autor autor){var actual=await context.Autores.FindAsync(autor.Id);if(actual is null)return false;actual.Nombre=autor.Nombre;actual.Biografia=autor.Biografia;await context.SaveChangesAsync();return true;}
 public async Task<(bool,string?)> EliminarAsync(int id){var autor=await context.Autores.Include(x=>x.Libros).FirstOrDefaultAsync(x=>x.Id==id);if(autor is null)return(false,null);if(autor.Libros.Count>0)return(false,"No se puede eliminar porque tiene libros asociados.");context.Remove(autor);await context.SaveChangesAsync();return(true,null);}
}
