using ProyectoRom.Models.data;
using System.Data.SqlClient;
using System.Data;

namespace ProyectoRom.Models.Repository
{
    public class LibroRepository : ILibroRepository
    {
        protected readonly string _conexionSQL = "";
        public LibroRepository(IConfiguration configuration)
        {
            _conexionSQL = configuration.GetConnectionString("SQLconexion");
        }

        public async Task<List<Libro>> GetLibro()
        {
            List<Libro> _lista = new List<Libro>();
            using (var conexion = new SqlConnection(_conexionSQL))
            {
                await conexion.OpenAsync();
                SqlCommand cmd = new SqlCommand("ListarLibros", conexion);
                cmd.CommandType = CommandType.StoredProcedure;

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync())
                    {
                        _lista.Add(new Libro
                        {
                            id_libro = Convert.ToInt32(dr["id_libro"]),
                            descripcion = Convert.ToString(dr["descripcion"]),
                            asignatura = new Asignatura
                            {
                                id_asig = Convert.ToInt32(dr["asignatura"]),
                                descripcion = Convert.ToString(dr["asignatura_descripcion"]),
                                estado = Convert.ToBoolean(dr["estado"])
                            },
                            stock = Convert.ToInt32(dr["stock"]),
                        });
                    }
                }
            }
            return _lista;
        }




        public async Task<Libro> GetLibroById(int id)
        {
            Libro _lista = null;

            using (var conexion = new SqlConnection(_conexionSQL))
            {
                await conexion.OpenAsync();
                SqlCommand cmd = new SqlCommand("ListarLibrosPorID", conexion);
                cmd.CommandType = CommandType.StoredProcedure;
                SqlParameter sql = new SqlParameter("@id_libro",id);
                cmd.Parameters.Add(sql);
                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync())
                    {
                        _lista=new()
                        {
                            id_libro = Convert.ToInt32(dr["id_libro"]),
                            descripcion = Convert.ToString(dr["descripcion"]),
                            asignatura = new Asignatura
                            {
                                id_asig = Convert.ToInt32(dr["asignatura"]),
                                descripcion = Convert.ToString(dr["descripcion"]),
                           
                            },
                            stock = Convert.ToInt32(dr["stock"]),
                        }
                       ;
                    }
                }
            }
            return _lista;
        }
        public async Task<bool> CreateLibroAsync(Libro lib)
        {
            using (var conexion = new SqlConnection(_conexionSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("CrearLibro", conexion);
                cmd.Parameters.AddWithValue("descripcion", lib.descripcion);
                cmd.Parameters.AddWithValue("asignatura", lib.asignatura.id_asig);
                cmd.Parameters.AddWithValue("stock", lib.stock);
                cmd.CommandType = CommandType.StoredProcedure;

                int filas_new=await cmd.ExecuteNonQueryAsync();
                if(filas_new > 0)
                    return true;
                else
                    return false;
                
            }
        }


        public async Task<bool> UpdateLibroAsync(Libro lib)
        {
            using (var conexion = new SqlConnection(_conexionSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("ActualizarLibro", conexion);
                cmd.Parameters.AddWithValue("id_libro", lib.id_libro);
                cmd.Parameters.AddWithValue("descripcion", lib.descripcion);
                cmd.Parameters.AddWithValue("asignatura", lib.asignatura.id_asig);
                cmd.Parameters.AddWithValue("stock", lib.stock);
                cmd.CommandType = CommandType.StoredProcedure;

                int filas_new = await cmd.ExecuteNonQueryAsync();
                if (filas_new > 0)
                    return true;
                else
                    return false;

            }
        }

        public async Task<bool> DeleteLibroAsync(int lib)
        {
          
            using (var conexion = new SqlConnection(_conexionSQL))
            {
                conexion.Open();
                SqlCommand cmd = new SqlCommand("EliminarLibro", conexion);
                cmd.Parameters.AddWithValue("id_libro", lib);
               
                cmd.CommandType = CommandType.StoredProcedure;

                int filas_new = await cmd.ExecuteNonQueryAsync();
                if (filas_new > 0)
                    return true;
                else
                    return false;

            }
        }

        public async Task<List<Libro>> BuscarLibros(string criterio)
        {
            using (var conexion = new SqlConnection(_conexionSQL))
            {
                await conexion.OpenAsync();

                string query = "SELECT * FROM Libro WHERE descripcion LIKE '%' + @criterio + '%'";

                SqlCommand cmd = new SqlCommand(query, conexion);
                cmd.Parameters.Add("@criterio", SqlDbType.NVarChar).Value = criterio;

                List<Libro> libros = new List<Libro>();

                using (var dr = await cmd.ExecuteReaderAsync())
                {
                    while (await dr.ReadAsync())
                    {
                        libros.Add(new Libro
                        {
                            id_libro = Convert.ToInt32(dr["id_libro"]),
                            descripcion = Convert.ToString(dr["descripcion"]),
                            asignatura = new Asignatura
                            {
                                id_asig = Convert.ToInt32(dr["asignatura"]),
                                descripcion = Convert.ToString(dr["descripcion"]),
                            },
                            stock = Convert.ToInt32(dr["stock"]),
                        });
                    }
                }

                return libros;
            }
        }

    





}
}
