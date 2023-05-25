namespace ProyectoRom.Models.data
{
    public class Libro
    {
        public int id_libro { get; set; }
        public string descripcion { get; set; }
        public Asignatura asignatura { get; set; }
        public int stock { get; set; }
    }
}
