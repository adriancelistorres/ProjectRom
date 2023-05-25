using ProyectoRom.Models.data;

namespace ProyectoRom.Models.Repository
{
    public interface IAsignaturaRepository
    {         
        Task<List<Asignatura>> GetAsignatura();
        Task<Asignatura> GetAsignaturaById(int id);
        Task<bool> CreateAsignaturaAsync(Asignatura asig);
        Task<bool> UpdateAsignaturaAsync(Asignatura asig);
        Task<bool> DeleteAsignaturaAsync(Asignatura asig);

    }
}
