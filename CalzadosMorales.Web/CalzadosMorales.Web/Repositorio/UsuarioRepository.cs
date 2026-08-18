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
            _cadenaConexion = configuration.GetConnectionString("CadenaSQL") ?? throw new InvalidOperationException("La cadena de conexión 'CadenaSQL' no está configurada.");
        }

        // 1. LOGIN
        public Usuario ValidarUsuario(string usuario, string clave)
        {
            Usuario user = null;
            try
            {
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
                                    Nombre = dr["nombre"]?.ToString() ?? string.Empty,
                                    UserLogin = dr["usuario"]?.ToString() ?? string.Empty,
                                    IdRol = Convert.ToInt32(dr["id_rol"]),
                                    Estado = Convert.ToBoolean(dr["estado"]),
                                    Rol = new Rol
                                    {
                                        IdRol = Convert.ToInt32(dr["id_rol"]),
                                        Nombre = dr["nombre_rol"]?.ToString() ?? string.Empty
                                    }
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al validar el usuario en la base de datos: " + ex.Message);
            }
            return user;
        }

        // 2. LISTAR ROLES (Para combos)
        public List<Rol> ListarRoles()
        {
            var lista = new List<Rol>();
            try
            {
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
                                    Nombre = dr["nombre"]?.ToString() ?? string.Empty
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar los roles: " + ex.Message);
            }
            return lista;
        }

        // 3. LISTAR USUARIOS
        public List<Usuario> ListarUsuarios()
        {
            var lista = new List<Usuario>();
            try
            {
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
                                    Nombre = dr["nombre"]?.ToString() ?? string.Empty,
                                    UserLogin = dr["usuario"]?.ToString() ?? string.Empty,
                                    Clave = dr["clave"]?.ToString(), // Opcional si el SP lo devuelve
                                    IdRol = Convert.ToInt32(dr["id_rol"]),
                                    Estado = Convert.ToBoolean(dr["estado"]),
                                    Rol = new Rol
                                    {
                                        IdRol = Convert.ToInt32(dr["id_rol"]),
                                        Nombre = dr["nombre_rol"]?.ToString() ?? string.Empty
                                    }
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar los usuarios: " + ex.Message);
            }
            return lista;
        }

        // 4. OBTENER POR ID
        public Usuario ObtenerUsuarioPorId(int idUsuario)
        {
            Usuario user = null;
            try
            {
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
                                    Nombre = dr["nombre"]?.ToString() ?? string.Empty,
                                    UserLogin = dr["usuario"]?.ToString() ?? string.Empty,
                                    Clave = dr["clave"]?.ToString(),
                                    IdRol = Convert.ToInt32(dr["id_rol"]),
                                    Estado = Convert.ToBoolean(dr["estado"]),
                                    Rol = new Rol
                                    {
                                        IdRol = Convert.ToInt32(dr["id_rol"]),
                                        Nombre = dr["nombre_rol"]?.ToString() ?? string.Empty
                                    }
                                };
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al obtener el usuario: " + ex.Message);
            }
            return user;
        }

        // 5. REGISTRAR
        public void RegistrarUsuario(string nombre, string usuario, string clave, int idRol)
        {
            try
            {
                using (var conexion = new SqlConnection(_cadenaConexion))
                {
                    using (var cmd = new SqlCommand("sp_RegistrarUsuario", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@nombre", nombre);
                        cmd.Parameters.AddWithValue("@usuario", usuario);
                        cmd.Parameters.AddWithValue("@clave", clave); // <-- Asegurar que esté presente
                        cmd.Parameters.AddWithValue("@id_rol", idRol);

                        conexion.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("UNIQUE KEY") || ex.Message.Contains("duplicate key"))
                {
                    throw new Exception("El nombre de usuario ya se encuentra registrado. Por favor, elija otro.");
                }

                throw new Exception("Error al registrar el usuario en la base de datos: " + ex.Message);
            }
        }

        // 6. ACTUALIZAR
        public void ActualizarUsuario(int idUsuario, string nombre, string usuario, string clave, int idRol) // <-- Agregar parámetro clave
        {
            try
            {
                using (var conexion = new SqlConnection(_cadenaConexion))
                {
                    using (var cmd = new SqlCommand("sp_ActualizarUsuario", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@id_usuario", idUsuario);
                        cmd.Parameters.AddWithValue("@nombre", nombre);
                        cmd.Parameters.AddWithValue("@usuario", usuario);
                        cmd.Parameters.AddWithValue("@clave", string.IsNullOrEmpty(clave) ? (object)DBNull.Value : clave); // <-- Enviar DBNull si viene vacía
                        cmd.Parameters.AddWithValue("@id_rol", idRol);

                        conexion.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception ex)
            {
                if (ex.Message.Contains("UNIQUE KEY") || ex.Message.Contains("duplicate key"))
                {
                    throw new Exception("El nombre de usuario ya está en uso por otro registro. Por favor, elija uno diferente.");
                }

                throw new Exception("Error al actualizar el usuario: " + ex.Message);
            }
        }

        // 7. CAMBIAR ESTADO
        public void CambiarEstadoUsuario(int idUsuario, bool estado)
        {
            try
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
            catch (Exception ex)
            {
                throw new Exception("Error al cambiar el estado del usuario: " + ex.Message);
            }
        }
    }
}