using CalzadosMorales.Web.Models;
using CalzadosMorales.Web.Repositorio;
using System.Security.Cryptography;
using System.Text;

namespace CalzadosMorales.Web.Servicios
{
    public class UsuarioService
    {
        private readonly UsuarioRepository _usuarioRepository;

        public UsuarioService(UsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        // Método auxiliar para encriptar la contraseña con SHA256
        private string EncriptarClave(string texto)
        {
            if (string.IsNullOrEmpty(texto)) return string.Empty;

            using (var sha256 = SHA256.Create())
            {
                var bytes = Encoding.UTF8.GetBytes(texto);
                var hashBytes = sha256.ComputeHash(bytes);
                return string.Concat(hashBytes.Select(b => b.ToString("x2")));
            }
        }

        public Usuario ValidarUsuario(string usuario, string clave)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(usuario) || string.IsNullOrWhiteSpace(clave))
                {
                    throw new Exception("El usuario y la contraseña son obligatorios para iniciar sesión.");
                }

                // Al validar el login, también debemos encriptar la contraseña ingresada 
                // para que coincida con la que está guardada en la base de datos de forma segura.
                string claveEncriptada = EncriptarClave(clave.Trim());

                return _usuarioRepository.ValidarUsuario(usuario.Trim(), claveEncriptada);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Rol> ListarRoles()
        {
            try
            {
                return _usuarioRepository.ListarRoles();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public List<Usuario> ListarUsuarios()
        {
            try
            {
                return _usuarioRepository.ListarUsuarios();
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public Usuario ObtenerUsuarioPorId(int idUsuario)
        {
            try
            {
                if (idUsuario <= 0)
                {
                    throw new Exception("El ID de usuario proporcionado no es válido.");
                }

                var usuario = _usuarioRepository.ObtenerUsuarioPorId(idUsuario);
                if (usuario == null)
                {
                    throw new Exception("No se encontró ningún usuario registrado con el ID especificado.");
                }

                return usuario;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void RegistrarUsuario(string nombre, string usuario, string clave, int idRol)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(nombre))
                    throw new Exception("El nombre del usuario es obligatorio.");

                if (string.IsNullOrWhiteSpace(usuario))
                    throw new Exception("El nombre de cuenta (usuario) es obligatorio.");

                if (string.IsNullOrWhiteSpace(clave))
                    throw new Exception("La contraseña es obligatoria.");

                if (idRol <= 0)
                    throw new Exception("Debe seleccionar un rol válido para el usuario.");

                // Encriptamos la clave antes de enviarla al repositorio
                string claveEncriptada = EncriptarClave(clave.Trim());

                _usuarioRepository.RegistrarUsuario(nombre.Trim(), usuario.Trim(), claveEncriptada, idRol);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        // Se agregó el parámetro 'string clave' para permitir actualizarla de forma opcional
        public void ActualizarUsuario(int idUsuario, string nombre, string usuario, string clave, int idRol)
        {
            try
            {
                if (idUsuario <= 0)
                    throw new Exception("ID de usuario inválido para actualizar.");

                if (string.IsNullOrWhiteSpace(nombre))
                    throw new Exception("El nombre no puede estar vacío.");

                if (string.IsNullOrWhiteSpace(usuario))
                    throw new Exception("El usuario no puede estar vacío.");

                if (idRol <= 0)
                    throw new Exception("Debe seleccionar un rol válido.");

                // Si la clave viene vacía/nula, mandamos cadena vacía para que el repositorio maneje el DBNull y conserve la anterior.
                // Si escribieron una nueva, la encriptamos.
                string claveEncriptada = string.IsNullOrWhiteSpace(clave) ? string.Empty : EncriptarClave(clave.Trim());

                _usuarioRepository.ActualizarUsuario(idUsuario, nombre.Trim(), usuario.Trim(), claveEncriptada, idRol);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void CambiarEstadoUsuario(int idUsuario, bool estado)
        {
            try
            {
                if (idUsuario <= 0)
                {
                    throw new Exception("El ID de usuario no es válido para cambiar el estado.");
                }

                _usuarioRepository.CambiarEstadoUsuario(idUsuario, estado);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}