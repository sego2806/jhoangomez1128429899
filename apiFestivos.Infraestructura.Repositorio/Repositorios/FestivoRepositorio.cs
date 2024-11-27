
using apiFestivos.Dominio.Entidades;
using apiFestivos.Infraestructura.Repositorio.Contextos;
using Microsoft.EntityFrameworkCore;
using apiFestivos.Core.Interfaces.Repositorios;
using apiFestivos.Dominio.DTOs;

namespace apiFestivos.Infraestructura.Repositorio.Repositorios
{
    public class FestivoRepositorio : IFestivoRepositorio
    {
        private FestivosContexto context;

        public FestivoRepositorio(FestivosContexto context)
        {
            this.context = context;
        }

        public async Task<Festivo> Obtener(int Id)
        {
            return await context.Festivos
                .Include(item => item.Tipo) // Incluye el Tipo
                .FirstOrDefaultAsync(item => item.Id == Id);
        }

        public async Task<IEnumerable<Festivo>> ObtenerTodos()
        {
            return await context.Festivos
                .Include(item => item.Tipo) // Incluye el Tipo
                .ToListAsync();
        }

        public async Task<IEnumerable<Festivo>> Buscar(string Dato)
        {
            return await context.Festivos
                .Where(item => item.Nombre.Contains(Dato)) // Filtrar elementos
                .Include(item => item.Tipo) // Incluye el Tipo
                .ToListAsync(); // Convertir a una lista IEnumerable<Festivo>
        }

        public async Task<Festivo> Agregar(Festivo Festivo)
        {
            context.Festivos.Add(Festivo);
            await context.SaveChangesAsync();
            return Festivo;
        }

        public async Task<Festivo> Modificar(Festivo Festivo)
        {
            var FestivoExistente = await context.Festivos.FindAsync(Festivo.Id);
            if (FestivoExistente == null)
            {
                return null;
            }
            context.Entry(FestivoExistente).CurrentValues.SetValues(Festivo);
            await context.SaveChangesAsync();
            return await context.Festivos.FindAsync(Festivo.Id);
        }

        public async Task<bool> Eliminar(int Id)
        {
            var FestivoExistente = await context.Festivos.FindAsync(Id);
            if (FestivoExistente == null)
            {
                return false;
            }

            context.Festivos.Remove(FestivoExistente);
            await context.SaveChangesAsync();
            return true;
        }

        // Implementación del método ObtenerAño
        public async Task<IEnumerable<FechaFestivo>> ObtenerAño(int año)
        {
            // Asegúrate de que la clase FechaFestivo tiene la propiedad Fecha de tipo DateTime
            return await context.Festivos
                .Where(f => f.Dia > 0 && f.Mes > 0) // Filtrar para que Día y Mes sean válidos
                .Select(f => new FechaFestivo
                {
                    Id = f.Id,
                    Nombre = f.Nombre,
                    Fecha = new DateTime(año, f.Mes, f.Dia), // Construir la fecha a partir de Año, Mes y Día
                    Tipo = f.Tipo
                })
                .ToListAsync();
        }
    }
}
