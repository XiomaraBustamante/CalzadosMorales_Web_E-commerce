using CalzadosMorales.Web.Models;
using CalzadosMorales.Web.Repositorio;

namespace CalzadosMorales.Web.Servicios
{
    public class UsuarioService
    {
        private readonly UsuarioRepository _usuarioRepository;

        public UsuarioService(UsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public Usuario ValidarUsuario(string usuario, string clave)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(usuario) || string.IsNullOrWhiteSpace(clave))
                {
                    throw new Exception("El usuario y la contraseña son obligatorios para iniciar sesión.");
                }

                return _usuarioRepository.ValidarUsuario(usuario.Trim(), clave);
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

                _usuarioRepository.RegistrarUsuario(nombre.Trim(), usuario.Trim(), clave, idRol);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public void ActualizarUsuario(int idUsuario, string nombre, string usuario, int idRol)
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

                _usuarioRepository.ActualizarUsuario(idUsuario, nombre.Trim(), usuario.Trim(), idRol);
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