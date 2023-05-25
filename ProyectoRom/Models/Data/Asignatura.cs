namespace ProyectoRom.Models.data
{
    public class Asignatura
    {
        public int id_asig { get; set; }
        public string descripcion { get; set; }
        public bool estado { get; set; }

        public static implicit operator bool(Asignatura v)
        {
            throw new NotImplementedException();
        }
    }
}
