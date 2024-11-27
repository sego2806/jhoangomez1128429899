using apiFestivos.Dominio.Entidades;

namespace apiFestivos.Dominio.DTOs
{
    public class FechaFestivo
    {
        public int Id { get; set; }       // El Id del festivo
        public DateTime Fecha { get; set; } // La fecha del festivo
        public string Nombre { get; set; }  // El nombre del festivo
        public Tipo Tipo { get; set; }      // El tipo del festivo, que es una referencia a la clase Tipo
    }
}
