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
            _cadenaConexion = configuration.GetConnectionString("CadenaConexion");
        }

        // ================= CATEGORÍAS =================

        public Categoria ObtenerCategoriaPorId(int id)
        {
            Categoria categoria = null;
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
            return categoria;
        }

        public List<Categoria> ListarCategorias()
        {
            var lista = new List<Categoria>();
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
            return lista;
        }

        public void RegistrarCategoria(string nombre)
        {
            using (var conexion = new SqlConnection(_cadenaConexion))
            {
                using (var comando = new SqlCommand("sp_RegistrarCategoria", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@nombre", nombre);
                    conexion.Open();
                    comando.ExecuteNonQuery();
                }
            }
        }

        public void ActualizarCategoria(int id, string nombre)
        {
            using (var conexion = new SqlConnection(_cadenaConexion))
            {
                using (var comando = new SqlCommand("sp_ActualizarCategoria", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@id_categoria", id);
                    comando.Parameters.AddWithValue("@nombre", nombre);
                    conexion.Open();
                    comando.ExecuteNonQuery();
                }
            }
        }

        public void CambiarEstadoCategoria(int id, bool estado)
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

        // ================= COLORES =================
        public Color ObtenerColorPorId(int id)
        {
            Color color = null;
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
            return color;
        }

        public List<Color> ListarColores()
        {
            var lista = new List<Color>();
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
            return lista;
        }

        public void RegistrarColor(string nombre)
        {
            using (var conexion = new SqlConnection(_cadenaConexion))
            {
                using (var comando = new SqlCommand("sp_RegistrarColor", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@nombre", nombre);
                    conexion.Open();
                    comando.ExecuteNonQuery();
                }
            }
        }

        public void ActualizarColor(int id, string nombre)
        {
            using (var conexion = new SqlConnection(_cadenaConexion))
            {
                using (var comando = new SqlCommand("sp_ActualizarColor", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@id_color", id);
                    comando.Parameters.AddWithValue("@nombre", nombre);
                    conexion.Open();
                    comando.ExecuteNonQuery();
                }
            }
        }

        public void CambiarEstadoColor(int id, bool estado)
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

        // ================= MATERIALES =================

        public Material ObtenerMaterialPorId(int id)
        {
            Material material = null;
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
            return material;
        }

        public List<Material> ListarMateriales()
        {
            var lista = new List<Material>();
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
            return lista;
        }

        public void RegistrarMaterial(string tipo)
        {
            using (var conexion = new SqlConnection(_cadenaConexion))
            {
                using (var comando = new SqlCommand("sp_RegistrarMaterial", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@tipo", tipo);
                    conexion.Open();
                    comando.ExecuteNonQuery();
                }
            }
        }

        public void ActualizarMaterial(int id, string tipo)
        {
            using (var conexion = new SqlConnection(_cadenaConexion))
            {
                using (var comando = new SqlCommand("sp_ActualizarMaterial", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@id_material", id);
                    comando.Parameters.AddWithValue("@tipo", tipo);
                    conexion.Open();
                    comando.ExecuteNonQuery();
                }
            }
        }

        public void CambiarEstadoMaterial(int id, bool estado)
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

        // ================= TALLAS =================

        public Talla ObtenerTallaPorId(int id)
        {
            Talla talla = null;
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
            return talla;
        }

        public List<Talla> ListarTallas()
        {
            var lista = new List<Talla>();
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
            return lista;
        }

        public void RegistrarTalla(string nombre)
        {
            using (var conexion = new SqlConnection(_cadenaConexion))
            {
                using (var comando = new SqlCommand("sp_RegistrarTalla", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@nombre", nombre);
                    conexion.Open();
                    comando.ExecuteNonQuery();
                }
            }
        }

        public void ActualizarTalla(int id, string nombre)
        {
            using (var conexion = new SqlConnection(_cadenaConexion))
            {
                using (var comando = new SqlCommand("sp_ActualizarTalla", conexion))
                {
                    comando.CommandType = CommandType.StoredProcedure;
                    comando.Parameters.AddWithValue("@id_talla", id);
                    comando.Parameters.AddWithValue("@nombre", nombre);
                    conexion.Open();
                    comando.ExecuteNonQuery();
                }
            }
        }

        public void CambiarEstadoTalla(int id, bool estado)
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
    }
}