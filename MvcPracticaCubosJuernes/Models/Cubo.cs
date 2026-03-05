namespace MvcPracticaCubosJuernes.Models
{
    public class Cubo
    {
        public int IdCubo { get; set; }
        public string Nombre { get; set; } = null!;
        public string Modelo { get; set; } = null!;
        public string Marca { get; set; } = null!;
        public string Imagen { get; set; } = null!;
        public int Precio { get; set; }
    }
}
