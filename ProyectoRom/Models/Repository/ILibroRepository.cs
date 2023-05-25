using ProyectoRom.Models.data;

namespace ProyectoRom.Models.Repository
{
    public interface ILibroRepository
    {
        Task<List<Libro>> GetLibro();
        Task <Libro>  GetLibroById(int id);

        Task<bool> CreateLibroAsync(Libro lib);
        Task<bool> UpdateLibroAsync(Libro lib);
        Task<bool> DeleteLibroAsync(int lib);
        Task<List<Libro>> BuscarLibros(string criterio);

    }
}
