namespace MvcPracticaCubosJuernes.Models
{
    public class CarritoItem
    {
        public Cubo Cubo { get; set; } = null!;
        public int Cantidad { get; set; }
        public int Subtotal => Cubo.Precio * Cantidad;
    }
}
