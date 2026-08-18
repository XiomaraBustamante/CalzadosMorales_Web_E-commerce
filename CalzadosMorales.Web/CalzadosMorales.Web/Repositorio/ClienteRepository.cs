using CalzadosMorales.Web.Models;
using Microsoft.AspNetCore.Identity; // <-- 1. Necesario para encriptar la contraseña
using Microsoft.Data.SqlClient;
using System.Data;

namespace CalzadosMorales.Web.Repositorio
{
    public class ClienteRepository
    {
        private readonly string _cadenaConexion;

        public ClienteRepository(IConfiguration configuration)
        {
            _cadenaConexion = configuration.GetConnectionString("CadenaSQL");
        }

        // ==========================================
        // PERSONAS NATURALES (CRUD COMPLETO)
        // ==========================================

        public PersonaNatural ObtenerPersonaNaturalPorId(int idCliente)
        {
            PersonaNatural persona = null;
            using (var conexion = new SqlConnection(_cadenaConexion))
            {
                using (var cmd = new SqlCommand("sp_BuscarPersonaNaturalPorId", conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_cliente", idCliente);
                    conexion.Open();
                    using (var dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            persona = new PersonaNatural
                            {
                                IdCliente = Convert.ToInt32(dr["id_cliente"]),
                                Dni = dr["dni"].ToString(),
                                Nombre = dr["nombre"].ToString(),
                                Apellido = dr["apellido"].ToString(),
                                Genero = Convert.ToInt32(dr["genero"]),
                                Cliente = new Cliente
                                {
                                    Telefono = dr["telefono"].ToString(),
                                    Email = dr["email"].ToString(),
                                    Direccion = dr["direccion"].ToString(),
                                    Estado = Convert.ToBoolean(dr["estado"])
                                }
                            };
                        }
                    }
                }
            }
            return persona;
        }

        public List<PersonaNatural> ListarPersonasNaturales()
        {
            var lista = new List<PersonaNatural>();
            using (var conexion = new SqlConnection(_cadenaConexion))
            {
                using (var cmd = new SqlCommand("sp_ListarPersonasNaturales", conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    conexion.Open();
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new PersonaNatural
                            {
                                IdCliente = Convert.ToInt32(dr["id"]),
                                Dni = dr["dni"].ToString(),
                                Nombre = dr["nombre"].ToString(),
                                Apellido = dr["apellido"].ToString(),
                                Genero = Convert.ToInt32(dr["genero"]),
                                Cliente = new Cliente
                                {
                                    Telefono = dr["telefono"].ToString(),
                                    Email = dr["email"].ToString(),
                                    Direccion = dr["direccion"].ToString(),
                                    Estado = Convert.ToBoolean(dr["estado"])
                                }
                            });
                        }
                    }
                }
            }
            return lista;
        }

        public void RegistrarPersonaNatural(string dni, int genero, string nombre, string apellido, string telefono, string email, string direccion, string passwordPlana)
        {
            string passwordHash = null;

            if (!string.IsNullOrEmpty(passwordPlana))
            {
                var passwordHasher = new PasswordHasher<object>();
                passwordHash = passwordHasher.HashPassword(null, passwordPlana);
            }

            try
            {
                using (var conexion = new SqlConnection(_cadenaConexion))
                {
                    using (var cmd = new SqlCommand("sp_RegistrarPersonaNatural", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@dni", dni);
                        cmd.Parameters.AddWithValue("@genero", genero);
                        cmd.Parameters.AddWithValue("@nombre", nombre);
                        cmd.Parameters.AddWithValue("@apellido", apellido);
                        cmd.Parameters.AddWithValue("@telefono", telefono);
                        cmd.Parameters.AddWithValue("@email", email);
                        cmd.Parameters.AddWithValue("@direccion", direccion);
                        cmd.Parameters.AddWithValue("@password", (object)passwordHash ?? DBNull.Value);

                        conexion.Open();
                        cmd.ExecuteNonQuery();
                    }
                }
            }
            catch (SqlException ex)
            {
                // 2627 y 2601 son los códigos de error en SQL Server para duplicidad de Unique Key / Primary Key
                if (ex.Number == 2627 || ex.Number == 2601)
                {
                    throw new Exception("El DNI o el correo electrónico ya se encuentran registrados en el sistema.");
                }
                throw new Exception("Error de base de datos: " + ex.Message);
            }
        }

        public void ActualizarPersonaNatural(int idCliente, string dni, int genero, string nombre, string apellido, string telefono, string email, string direccion, string passwordPlana)
        {
            string passwordHash = null;

            // Si ingresó una contraseña nueva, la encripta. Si la dejó vacía, se manda null.
            if (!string.IsNullOrEmpty(passwordPlana))
            {
                var passwordHasher = new PasswordHasher<object>();
                passwordHash = passwordHasher.HashPassword(null, passwordPlana);
            }

            using (var conexion = new SqlConnection(_cadenaConexion))
            {
                using (var cmd = new SqlCommand("sp_ActualizarPersonaNatural", conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_cliente", idCliente);
                    cmd.Parameters.AddWithValue("@dni", dni);
                    cmd.Parameters.AddWithValue("@genero", genero);
                    cmd.Parameters.AddWithValue("@nombre", nombre);
                    cmd.Parameters.AddWithValue("@apellido", apellido);
                    cmd.Parameters.AddWithValue("@telefono", telefono);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@direccion", direccion);
                    cmd.Parameters.AddWithValue("@password", (object)passwordHash ?? DBNull.Value);

                    conexion.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void CambiarEstadoPersonaNatural(int idCliente, bool estado)
        {
            using (var conexion = new SqlConnection(_cadenaConexion))
            {
                using (var cmd = new SqlCommand("sp_CambiarEstadoPersonaNatural", conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_cliente", idCliente);
                    cmd.Parameters.AddWithValue("@estado", estado);

                    conexion.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // ==========================================
        // PERSONAS JURÍDICAS (CRUD COMPLETO)
        // ==========================================

        public PersonaJuridica ObtenerPersonaJuridicaPorId(int idCliente)
        {
            PersonaJuridica persona = null;
            using (var conexion = new SqlConnection(_cadenaConexion))
            {
                using (var cmd = new SqlCommand("sp_BuscarPersonaJuridicaPorId", conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_cliente", idCliente);
                    conexion.Open();
                    using (var dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            persona = new PersonaJuridica
                            {
                                IdCliente = Convert.ToInt32(dr["id_cliente"]),
                                Ruc = dr["ruc"].ToString(),
                                RazonSocial = dr["razon_social"].ToString(),
                                RepreLegal = dr["repre_legal"].ToString(),
                                Cliente = new Cliente
                                {
                                    Telefono = dr["telefono"].ToString(),
                                    Email = dr["email"].ToString(),
                                    Direccion = dr["direccion"].ToString(),
                                    Estado = Convert.ToBoolean(dr["estado"])
                                }
                            };
                        }
                    }
                }
            }
            return persona;
        }

        public List<PersonaJuridica> ListarPersonasJuridicas()
        {
            var lista = new List<PersonaJuridica>();
            using (var conexion = new SqlConnection(_cadenaConexion))
            {
                using (var cmd = new SqlCommand("sp_ListarPersonasJuridicas", conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    conexion.Open();
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new PersonaJuridica
                            {
                                IdCliente = Convert.ToInt32(dr["id"]),
                                Ruc = dr["ruc"].ToString(),
                                RazonSocial = dr["razon_social"].ToString(),
                                RepreLegal = dr["repre_legal"].ToString(),
                                Cliente = new Cliente
                                {
                                    Telefono = dr["telefono"].ToString(),
                                    Email = dr["email"].ToString(),
                                    Direccion = dr["direccion"].ToString(),
                                    Estado = Convert.ToBoolean(dr["estado"])
                                }
                            });
                        }
                    }
                }
            }
            return lista;
        }

        public void RegistrarPersonaJuridica(string ruc, string razonSocial, string repreLegal, string telefono, string email, string direccion, string passwordPlana)
        {
            string passwordHash = null;

            if (!string.IsNullOrEmpty(passwordPlana))
            {
                var passwordHasher = new PasswordHasher<object>();
                passwordHash = passwordHasher.HashPassword(null, passwordPlana);
            }

            using (var conexion = new SqlConnection(_cadenaConexion))
            {
                using (var cmd = new SqlCommand("sp_RegistrarPersonaJuridica", conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@ruc", ruc);
                    cmd.Parameters.AddWithValue("@razon_social", razonSocial);
                    cmd.Parameters.AddWithValue("@repre_legal", repreLegal);
                    cmd.Parameters.AddWithValue("@telefono", telefono);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@direccion", direccion);
                    cmd.Parameters.AddWithValue("@password", (object)passwordHash ?? DBNull.Value);

                    conexion.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void ActualizarPersonaJuridica(int idCliente, string ruc, string razonSocial, string repreLegal, string telefono, string email, string direccion, string passwordPlana)
        {
            string passwordHash = null;

            if (!string.IsNullOrEmpty(passwordPlana))
            {
                var passwordHasher = new PasswordHasher<object>();
                passwordHash = passwordHasher.HashPassword(null, passwordPlana);
            }

            using (var conexion = new SqlConnection(_cadenaConexion))
            {
                using (var cmd = new SqlCommand("sp_ActualizarPersonaJuridica", conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_cliente", idCliente);
                    cmd.Parameters.AddWithValue("@ruc", ruc);
                    cmd.Parameters.AddWithValue("@razon_social", razonSocial);
                    cmd.Parameters.AddWithValue("@repre_legal", repreLegal);
                    cmd.Parameters.AddWithValue("@telefono", telefono);
                    cmd.Parameters.AddWithValue("@email", email);
                    cmd.Parameters.AddWithValue("@direccion", direccion);
                    cmd.Parameters.AddWithValue("@password", (object)passwordHash ?? DBNull.Value);

                    conexion.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void CambiarEstadoPersonaJuridica(int idCliente, bool estado)
        {
            using (var conexion = new SqlConnection(_cadenaConexion))
            {
                using (var cmd = new SqlCommand("sp_CambiarEstadoPersonaJuridica", conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_cliente", idCliente);
                    cmd.Parameters.AddWithValue("@estado", estado);

                    conexion.Open();
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // ==========================================
        // LOGIN Y AUTENTICACIÓN
        // ==========================================

        public Cliente LoginCliente(string email)
        {
            Cliente cliente = null;
            using (var conexion = new SqlConnection(_cadenaConexion))
            {
                using (var cmd = new SqlCommand("sp_LoginCliente", conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@email", email);
                    conexion.Open();
                    using (var dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            cliente = new Cliente
                            {
                                IdCliente = Convert.ToInt32(dr["id_cliente"]),
                                Email = dr["email"].ToString(),
                                Direccion = dr["direccion"].ToString(),
                                Telefono = dr["telefono"].ToString(),
                                Password = dr["password"] != DBNull.Value ? dr["password"].ToString() : null,
                                Estado = Convert.ToBoolean(dr["estado"]) // <--- ¡AQUÍ ESTABA EL DETALLE!
                            };
                        }
                    }
                }
            }
            return cliente;
        }
    }
}