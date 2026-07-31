using CalzadosMorales.Web.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace CalzadosMorales.Web.Repositorio
{
    public class UsuarioRepository
    {
        private readonly string _cadenaConexion;

        public UsuarioRepository(IConfiguration configuration)
        {
            _cadenaConexion = configuration.GetConnectionString("CadenaSQL");
        }

        // 1. LOGIN
        public Usuario ValidarUsuario(string usuario, string clave)
        {
            Usuario user = null;
            using (var conexion = new SqlConnection(_cadenaConexion))
            {
                using (var cmd = new SqlCommand("sp_LoginUsuario", conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@usuario", usuario);
                    cmd.Parameters.AddWithValue("@clave", clave);
                    conexion.Open();
                    using (var dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            user = new Usuario
                            {
                                IdUsuario = Convert.ToInt32(dr["id_usuario"]),
                                Nombre = dr["nombre"].ToString(),
                                UserLogin = dr["usuario"].ToString(),
                                IdRol = Convert.ToInt32(dr["id_rol"]),
                                Estado = Convert.ToBoolean(dr["estado"]),
                                Rol = new Rol
                                {
                                    IdRol = Convert.ToInt32(dr["id_rol"]),
                                    Nombre = dr["nombre_rol"].ToString()
                                }
                            };
                        }
                    }
                }
            }
            return user;
        }

        // 2. LISTAR ROLES (Para combos)
        public List<Rol> ListarRoles()
        {
            var lista = new List<Rol>();
            using (var conexion = new SqlConnection(_cadenaConexion))
            {
                using (var cmd = new SqlCommand("sp_ListarRoles", conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    conexion.Open();
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Rol
                            {
                                IdRol = Convert.ToInt32(dr["id_rol"]),
                                Nombre = dr["nombre"].ToString()
                            });
                        }
                    }
                }
            }
            return lista;
        }

        // 3. LISTAR USUARIOS
        public List<Usuario> ListarUsuarios()
        {
            var lista = new List<Usuario>();
            using (var conexion = new SqlConnection(_cadenaConexion))
            {
                using (var cmd = new SqlCommand("sp_ListarUsuarios", conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    conexion.Open();
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Usuario
                            {
                                IdUsuario = Convert.ToInt32(dr["id_usuario"]),
                                Nombre = dr["nombre"].ToString(),
                                UserLogin = dr["usuario"].ToString(),
                                Clave = dr["clave"].ToString(),
                                IdRol = Convert.ToInt32(dr["id_rol"]),
                                Estado = Convert.ToBoolean(dr["estado"]),
                                Rol = new Rol
                                {
                                    IdRol = Convert.ToInt32(dr["id_rol"]),
                                    Nombre = dr["nombre_rol"].ToString()
                                }
                            });
                        }
                    }
                }
            }
            return lista;
        }

        // 4. OBTENER POR ID
        public Usuario ObtenerUsuarioPorId(int idUsuario)
        {
            Usuario user = null;
            using (var conexion = new SqlConnection(_cadenaConexion))
            {
                using (var cmd = new SqlCommand("sp_ObtenerUsuarioPorId", conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_usuario", idUsuario);
                    conexion.Open();
                    using (var dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            user = new Usuario
                            {
                                IdUsuario = Convert.ToInt32(dr["id_usuario"]),
                                Nombre = dr["nombre"].ToString(),
                                UserLogin = dr["usuario"].ToString(),
                                Clave = dr["clave"].ToString(),
                                IdRol = Convert.ToInt32(dr["id_rol"]),
                                Estado = Convert.ToBoolean(dr["estado"]),
                                Rol = new Rol
                                {
                                    IdRol = Convert.ToInt32(dr["id_rol"]),
                                    Nombre = dr["nombre_rol"].ToString()
                                }
                            };
                        }
                    }
                }
            }
            return user;
        }

        // 5. REGISTRAR
        public void RegistrarUsuario(string nombre, string usuario, string clave, int idRol)
        {
            using (var conexion = new SqlConnection(_cadenaConexion))
            {
                using (var cmd = new SqlCommand("sp_RegistrarUsuario", conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@nombre", nombre);
                    cmd.Parameters.AddWithValue("@usuario", usuario);
                    cmd.Parameters.AddWithValue("@clave", clave);
                    cmd.Parameters.AddWithValue("@id_rol", idRol);

                    conexion.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // 6. ACTUALIZAR
        public void ActualizarUsuario(int idUsuario, string nombre, string usuario, int idRol)
        {
            using (var conexion = new SqlConnection(_cadenaConexion))
            {
                using (var cmd = new SqlCommand("sp_ActualizarUsuario", conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_usuario", idUsuario);
                    cmd.Parameters.AddWithValue("@nombre", nombre);
                    cmd.Parameters.AddWithValue("@usuario", usuario);
                    cmd.Parameters.AddWithValue("@id_rol", idRol);

                    conexion.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // 7. CAMBIAR ESTADO
        public void CambiarEstadoUsuario(int idUsuario, bool estado)
        {
            using (var conexion = new SqlConnection(_cadenaConexion))
            {
                using (var cmd = new SqlCommand("sp_CambiarEstadoUsuario", conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_usuario", idUsuario);
                    cmd.Parameters.AddWithValue("@estado", estado);

                    conexion.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}