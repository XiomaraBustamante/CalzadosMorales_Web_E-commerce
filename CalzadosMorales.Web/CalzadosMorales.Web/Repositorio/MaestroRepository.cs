using CalzadosMorales.Web.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace CalzadosMorales.Web.Repositorio
{
    public class MaestroRepository
    {
        private readonly string _cadenaConexion;

        public MaestroRepository(IConfiguration configuration)
        {
            _cadenaConexion = configuration.GetConnectionString("CadenaSQL");
        }

        // ================= CATEGORÍAS =================

        public Categoria ObtenerCategoriaPorId(int id)
        {
            if (id <= 0) return null;

            Categoria categoria = null;
            try
            {
                using (var conexion = new SqlConnection(_cadenaConexion))
                {
                    using (var comando = new SqlCommand("sp_BuscarCategoriaPorId", conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;
                        comando.Parameters.AddWithValue("@id_categoria", id);
                        conexion.Open();
                        using (var reader = comando.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                categoria = new Categoria
                                {
                                    IdCategoria = Convert.ToInt32(reader["id_categoria"]),
                                    Nombre = reader["nombre"].ToString(),
                                    Estado = Convert.ToBoolean(reader["estado"])
                                };
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error de base de datos al buscar la categoría: " + ex.Message);
            }
            return categoria;
        }

        public List<Categoria> ListarCategorias()
        {
            var lista = new List<Categoria>();
            try
            {
                using (var conexion = new SqlConnection(_cadenaConexion))
                {
                    using (var comando = new SqlCommand("sp_ListarCategorias", conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;
                        conexion.Open();
                        using (var reader = comando.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                lista.Add(new Categoria
                                {
                                    IdCategoria = Convert.ToInt32(reader["id_categoria"]),
                                    Nombre = reader["nombre"].ToString(),
                                    Estado = Convert.ToBoolean(reader["estado"])
                                });
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error al listar las categorías: " + ex.Message);
            }
            return lista;
        }

        public void RegistrarCategoria(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre de la categoría no puede estar vacío.");

            try
            {
                using (var conexion = new SqlConnection(_cadenaConexion))
                {
                    using (var comando = new SqlCommand("sp_RegistrarCategoria", conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;
                        comando.Parameters.AddWithValue("@nombre", nombre.Trim());
                        conexion.Open();
                        comando.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error al registrar la categoría: " + ex.Message);
            }
        }

        public void ActualizarCategoria(int id, string nombre)
        {
            if (id <= 0) throw new ArgumentException("ID de categoría inválido.");
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre de la categoría es obligatorio.");

            try
            {
                using (var conexion = new SqlConnection(_cadenaConexion))
                {
                    using (var comando = new SqlCommand("sp_ActualizarCategoria", conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;
                        comando.Parameters.AddWithValue("@id_categoria", id);
                        comando.Parameters.AddWithValue("@nombre", nombre.Trim());
                        conexion.Open();
                        comando.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error al actualizar la categoría: " + ex.Message);
            }
        }

        public void CambiarEstadoCategoria(int id, bool estado)
        {
            if (id <= 0) throw new ArgumentException("ID de categoría inválido.");

            try
            {
                using (var conexion = new SqlConnection(_cadenaConexion))
                {
                    using (var comando = new SqlCommand("sp_CambiarEstadoCategoria", conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;
                        comando.Parameters.AddWithValue("@id_categoria", id);
                        comando.Parameters.AddWithValue("@estado", estado);
                        conexion.Open();
                        comando.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error al cambiar el estado de la categoría: " + ex.Message);
            }
        }

        // ================= COLORES =================

        public Color ObtenerColorPorId(int id)
        {
            if (id <= 0) return null;

            Color color = null;
            try
            {
                using (var conexion = new SqlConnection(_cadenaConexion))
                {
                    using (var comando = new SqlCommand("sp_BuscarColorPorId", conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;
                        comando.Parameters.AddWithValue("@id_color", id);
                        conexion.Open();
                        using (var reader = comando.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                color = new Color
                                {
                                    IdColor = Convert.ToInt32(reader["id_color"]),
                                    Nombre = reader["nombre"].ToString(),
                                    Estado = Convert.ToBoolean(reader["estado"])
                                };
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error de base de datos al buscar el color: " + ex.Message);
            }
            return color;
        }

        public List<Color> ListarColores()
        {
            var lista = new List<Color>();
            try
            {
                using (var conexion = new SqlConnection(_cadenaConexion))
                {
                    using (var comando = new SqlCommand("sp_ListarColores", conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;
                        conexion.Open();
                        using (var reader = comando.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                lista.Add(new Color
                                {
                                    IdColor = Convert.ToInt32(reader["id_color"]),
                                    Nombre = reader["nombre"].ToString(),
                                    Estado = Convert.ToBoolean(reader["estado"])
                                });
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error al listar los colores: " + ex.Message);
            }
            return lista;
        }

        public void RegistrarColor(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre del color no puede estar vacío.");

            try
            {
                using (var conexion = new SqlConnection(_cadenaConexion))
                {
                    using (var comando = new SqlCommand("sp_RegistrarColor", conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;
                        comando.Parameters.AddWithValue("@nombre", nombre.Trim());
                        conexion.Open();
                        comando.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error al registrar el color: " + ex.Message);
            }
        }

        public void ActualizarColor(int id, string nombre)
        {
            if (id <= 0) throw new ArgumentException("ID de color inválido.");
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre del color es obligatorio.");

            try
            {
                using (var conexion = new SqlConnection(_cadenaConexion))
                {
                    using (var comando = new SqlCommand("sp_ActualizarColor", conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;
                        comando.Parameters.AddWithValue("@id_color", id);
                        comando.Parameters.AddWithValue("@nombre", nombre.Trim());
                        conexion.Open();
                        comando.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error al actualizar el color: " + ex.Message);
            }
        }

        public void CambiarEstadoColor(int id, bool estado)
        {
            if (id <= 0) throw new ArgumentException("ID de color inválido.");

            try
            {
                using (var conexion = new SqlConnection(_cadenaConexion))
                {
                    using (var comando = new SqlCommand("sp_CambiarEstadoColor", conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;
                        comando.Parameters.AddWithValue("@id_color", id);
                        comando.Parameters.AddWithValue("@estado", estado);
                        conexion.Open();
                        comando.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error al cambiar el estado del color: " + ex.Message);
            }
        }

        // ================= MATERIALES =================

        public Material ObtenerMaterialPorId(int id)
        {
            if (id <= 0) return null;

            Material material = null;
            try
            {
                using (var conexion = new SqlConnection(_cadenaConexion))
                {
                    using (var comando = new SqlCommand("sp_BuscarMaterialPorId", conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;
                        comando.Parameters.AddWithValue("@id_material", id);
                        conexion.Open();
                        using (var reader = comando.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                material = new Material
                                {
                                    IdMaterial = Convert.ToInt32(reader["id_material"]),
                                    Tipo = reader["tipo"].ToString(),
                                    Estado = Convert.ToBoolean(reader["estado"])
                                };
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error de base de datos al buscar el material: " + ex.Message);
            }
            return material;
        }

        public List<Material> ListarMateriales()
        {
            var lista = new List<Material>();
            try
            {
                using (var conexion = new SqlConnection(_cadenaConexion))
                {
                    using (var comando = new SqlCommand("sp_ListarMateriales", conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;
                        conexion.Open();
                        using (var reader = comando.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                lista.Add(new Material
                                {
                                    IdMaterial = Convert.ToInt32(reader["id_material"]),
                                    Tipo = reader["tipo"].ToString(),
                                    Estado = Convert.ToBoolean(reader["estado"])
                                });
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error al listar los materiales: " + ex.Message);
            }
            return lista;
        }

        public void RegistrarMaterial(string tipo)
        {
            if (string.IsNullOrWhiteSpace(tipo))
                throw new ArgumentException("El tipo de material no puede estar vacío.");

            try
            {
                using (var conexion = new SqlConnection(_cadenaConexion))
                {
                    using (var comando = new SqlCommand("sp_RegistrarMaterial", conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;
                        comando.Parameters.AddWithValue("@tipo", tipo.Trim());
                        conexion.Open();
                        comando.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error al registrar el material: " + ex.Message);
            }
        }

        public void ActualizarMaterial(int id, string tipo)
        {
            if (id <= 0) throw new ArgumentException("ID de material inválido.");
            if (string.IsNullOrWhiteSpace(tipo))
                throw new ArgumentException("El tipo de material es obligatorio.");

            try
            {
                using (var conexion = new SqlConnection(_cadenaConexion))
                {
                    using (var comando = new SqlCommand("sp_ActualizarMaterial", conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;
                        comando.Parameters.AddWithValue("@id_material", id);
                        comando.Parameters.AddWithValue("@tipo", tipo.Trim());
                        conexion.Open();
                        comando.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error al actualizar el material: " + ex.Message);
            }
        }

        public void CambiarEstadoMaterial(int id, bool estado)
        {
            if (id <= 0) throw new ArgumentException("ID de material inválido.");

            try
            {
                using (var conexion = new SqlConnection(_cadenaConexion))
                {
                    using (var comando = new SqlCommand("sp_CambiarEstadoMaterial", conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;
                        comando.Parameters.AddWithValue("@id_material", id);
                        comando.Parameters.AddWithValue("@estado", estado);
                        conexion.Open();
                        comando.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error al cambiar el estado del material: " + ex.Message);
            }
        }

        // ================= TALLAS =================

        public Talla ObtenerTallaPorId(int id)
        {
            if (id <= 0) return null;

            Talla talla = null;
            try
            {
                using (var conexion = new SqlConnection(_cadenaConexion))
                {
                    using (var comando = new SqlCommand("sp_BuscarTallaPorId", conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;
                        comando.Parameters.AddWithValue("@id_talla", id);
                        conexion.Open();
                        using (var reader = comando.ExecuteReader())
                        {
                            if (reader.Read())
                            {
                                talla = new Talla
                                {
                                    IdTalla = Convert.ToInt32(reader["id_talla"]),
                                    Nombre = reader["nombre"].ToString(),
                                    Estado = Convert.ToBoolean(reader["estado"])
                                };
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error de base de datos al buscar la talla: " + ex.Message);
            }
            return talla;
        }

        public List<Talla> ListarTallas()
        {
            var lista = new List<Talla>();
            try
            {
                using (var conexion = new SqlConnection(_cadenaConexion))
                {
                    using (var comando = new SqlCommand("sp_ListarTallas", conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;
                        conexion.Open();
                        using (var reader = comando.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                lista.Add(new Talla
                                {
                                    IdTalla = Convert.ToInt32(reader["id_talla"]),
                                    Nombre = reader["nombre"].ToString(),
                                    Estado = Convert.ToBoolean(reader["estado"])
                                });
                            }
                        }
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error al listar las tallas: " + ex.Message);
            }
            return lista;
        }

        public void RegistrarTalla(string nombre)
        {
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre de la talla no puede estar vacío.");

            try
            {
                using (var conexion = new SqlConnection(_cadenaConexion))
                {
                    using (var comando = new SqlCommand("sp_RegistrarTalla", conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;
                        comando.Parameters.AddWithValue("@nombre", nombre.Trim());
                        conexion.Open();
                        comando.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error al registrar la talla: " + ex.Message);
            }
        }

        public void ActualizarTalla(int id, string nombre)
        {
            if (id <= 0) throw new ArgumentException("ID de talla inválido.");
            if (string.IsNullOrWhiteSpace(nombre))
                throw new ArgumentException("El nombre de la talla es obligatorio.");

            try
            {
                using (var conexion = new SqlConnection(_cadenaConexion))
                {
                    using (var comando = new SqlCommand("sp_ActualizarTalla", conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;
                        comando.Parameters.AddWithValue("@id_talla", id);
                        comando.Parameters.AddWithValue("@nombre", nombre.Trim());
                        conexion.Open();
                        comando.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error al actualizar la talla: " + ex.Message);
            }
        }

        public void CambiarEstadoTalla(int id, bool estado)
        {
            if (id <= 0) throw new ArgumentException("ID de talla inválido.");

            try
            {
                using (var conexion = new SqlConnection(_cadenaConexion))
                {
                    using (var comando = new SqlCommand("sp_CambiarEstadoTalla", conexion))
                    {
                        comando.CommandType = CommandType.StoredProcedure;
                        comando.Parameters.AddWithValue("@id_talla", id);
                        comando.Parameters.AddWithValue("@estado", estado);
                        conexion.Open();
                        comando.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                throw new Exception("Error al cambiar el estado de la talla: " + ex.Message);
            }
        }
    }
}