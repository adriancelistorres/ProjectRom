using ProyectoRom.Models.data;
using System.Data;
using System.Data.SqlClient;

namespace ProyectoRom.Models.Repository
{
    public class AsignaturaRepository:IAsignaturaRepository

    {
        protected readonly string _conexionSQL="";
        public AsignaturaRepository(IConfiguration configuration)
        {
            _conexionSQL = configuration.GetConnectionString("SQLconexion");
        }

        public async Task<List<Asignatura>> GetAsignatura()
        {
            List<Asignatura> _lista = new List<Asignatura>();
            using (var conexion = new SqlConnection(_conexionSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("ListarAsignaturas", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync())
                    {
                        _lista.Add(new Asignatura
                        {
                            id_asig = Convert.ToInt32(dr["id_asig"]),
                            descripcion = Convert.ToString(dr["descripcion"]),
                            estado = Convert.ToBoolean(dr["estado"])

                        });

                    }
                }

            }
            return _lista;
        }

        public async Task<Asignatura> GetAsignaturaById(int id)
        {
            Asignatura _lista = null;

            using (var conexion = new SqlConnection(_conexionSQL))
            {
                await conexion.OpenAsync();
                SqlCommand cmd = new SqlCommand("ListarAsignaturasPorID", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sql = new SqlParameter("@id_asig", id);
                cmd.Parameters.Add(sql);
                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync())
                    {
                        _lista = new()
                        {
                            id_asig = Convert.ToInt32(dr["id_asig"]),
                            descripcion = Convert.ToString(dr["descripcion"]),
                            estado = Convert.ToBoolean(dr["estado"]),
                        }
                       ;
                    }
                }
            }
            return _lista;
        }

        public async Task<bool> CreateAsignaturaAsync(Asignatura asig)
        {
            using (var conexion = new SqlConnection(_conexionSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("CrearAsignatura", conexion);
                cmd.Parameters.AddWithValue("@descripcion", asig.descripcion);
                cmd.Parameters.AddWithValue("@estado", asig.estado);
          
                cmd.CommandType = CommandType.StoredProcedure;

                int filas_new = await cmd.ExecuteNonQueryAsync();
                if (filas_new > 0)
                    return true;
                else
                    return false;

            }

        }


        public async Task<bool> UpdateAsignaturaAsync(Asignatura asig)
        {
            using (var conexion = new SqlConnection(_conexionSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("ActualizarAsignatura", conexion);
                cmd.Parameters.AddWithValue("@id_asig", asig.id_asig); // Corregido: Asignar el valor correcto del objeto asig
                cmd.Parameters.AddWithValue("@descripcion", asig.descripcion);
                cmd.Parameters.AddWithValue("@estado", asig.estado); // Corregido: Asignar el valor correcto del objeto asig

                cmd.CommandType = CommandType.StoredProcedure;

                int filas_new = await cmd.ExecuteNonQueryAsync();
                if (filas_new > 0)
                    return true;
                else
                    return false;
            }
        }


        public Task<bool> DeleteAsignaturaAsync(Asignatura asig)
        {
            throw new NotImplementedException();
        }

   
      
    }
}
