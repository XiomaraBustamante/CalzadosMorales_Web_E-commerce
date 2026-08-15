using CalzadosMorales.Web.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace CalzadosMorales.Web.Repositorio
{
    public class ProductoRepository
    {
        private readonly string _cadenaConexion;

        public ProductoRepository(IConfiguration configuration)
        {
            _cadenaConexion = configuration.GetConnectionString("CadenaSQL");
        }

        // Obtener producto por ID (con tallas y ahora también con imágenes)
        public Producto ObtenerProductoPorId(int idProducto)
        {
            Producto producto = null;
            using (var conexion = new SqlConnection(_cadenaConexion))
            {
                conexion.Open();

                // 1. Obtener los datos básicos del producto
                using (var cmd = new SqlCommand("sp_BuscarProductoPorId", conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_producto", idProducto);
                    using (var dr = cmd.ExecuteReader())
                    {
                        if (dr.Read())
                        {
                            producto = new Producto
                            {
                                IdProducto = Convert.ToInt32(dr["id_producto"]),
                                Nombre = dr["nombre"].ToString(),
                                Descripcion = dr["descripcion"] != DBNull.Value ? dr["descripcion"].ToString() : "",
                                Precio = Convert.ToDecimal(dr["precio"]),
                                IdCategoria = Convert.ToInt32(dr["id_categoria"]),
                                IdColor = dr["id_color"] != DBNull.Value ? Convert.ToInt32(dr["id_color"]) : (int?)null,
                                IdMaterial = dr["id_material"] != DBNull.Value ? Convert.ToInt32(dr["id_material"]) : (int?)null,
                                Estado = Convert.ToBoolean(dr["estado"])
                            };
                        }
                    }
                }

                if (producto != null)
                {
                    // 2. Obtener las tallas y stocks asociados
                    using (var cmdTallas = new SqlCommand("sp_ListarTallasPorProducto", conexion))
                    {
                        cmdTallas.CommandType = CommandType.StoredProcedure;
                        cmdTallas.Parameters.AddWithValue("@id_producto", idProducto);
                        using (var drTallas = cmdTallas.ExecuteReader())
                        {
                            while (drTallas.Read())
                            {
                                producto.ListaTallasStock.Add(new ProductoTalla
                                {
                                    IdProducto = idProducto,
                                    IdTalla = Convert.ToInt32(drTallas["id_talla"]),
                                    Stock = Convert.ToInt32(drTallas["stock"])
                                });
                            }
                        }
                    }

                    // 3. Obtener las imágenes asociadas para el formulario de edición
                    producto.ListaImagenes = ListarImagenesPorProducto(idProducto);
                }
            }
            return producto;
        }

        // Listar todos los productos (con soporte para Tallas y Stock integrados para la vista)
        public List<Producto> ListarProductos()
        {
            var lista = new List<Producto>();
            using (var conexion = new SqlConnection(_cadenaConexion))
            {
                conexion.Open();
                using (var cmd = new SqlCommand("sp_ListarProductos", conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new Producto
                            {
                                IdProducto = Convert.ToInt32(dr["id_producto"]),
                                Nombre = dr["nombre"].ToString(),
                                Descripcion = dr["descripcion"] != DBNull.Value ? dr["descripcion"].ToString() : "",
                                Precio = Convert.ToDecimal(dr["precio"]),
                                IdCategoria = Convert.ToInt32(dr["id_categoria"]),
                                IdColor = dr["id_color"] != DBNull.Value ? Convert.ToInt32(dr["id_color"]) : (int?)null,
                                IdMaterial = dr["id_material"] != DBNull.Value ? Convert.ToInt32(dr["id_material"]) : (int?)null,
                                Estado = Convert.ToBoolean(dr["estado"]),

                                // Campos informativos y de visualización en tabla
                                CategoriaNombre = dr["categoria_nombre"].ToString(),
                                ColorNombre = dr["color_nombre"] != DBNull.Value ? dr["color_nombre"].ToString() : "Sin Color",
                                MaterialTipo = dr["material_tipo"] != DBNull.Value ? dr["material_tipo"].ToString() : "Sin Material",
                                Talla = dr["talla"] != DBNull.Value ? dr["talla"].ToString() : "N/A",
                                Stock = Convert.ToInt32(dr["stock"])
                            });
                        }
                    }
                }
            }
            return lista;
        }

        // Registrar producto y devolver el ID generado
        public int RegistrarProducto(Producto producto)
        {
            int idGenerado = 0;
            using (var conexion = new SqlConnection(_cadenaConexion))
            {
                conexion.Open();
                using (var cmd = new SqlCommand("sp_RegistrarProducto", conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@nombre", producto.Nombre);
                    cmd.Parameters.AddWithValue("@descripcion", producto.Descripcion ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@id_color", producto.IdColor ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@id_material", producto.IdMaterial ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@precio", producto.Precio);
                    cmd.Parameters.AddWithValue("@id_categoria", producto.IdCategoria);

                    SqlParameter paramOutput = new SqlParameter("@nuevo_id", SqlDbType.Int)
                    {
                        Direction = ParameterDirection.Output
                    };
                    cmd.Parameters.Add(paramOutput);

                    cmd.ExecuteNonQuery();
                    idGenerado = Convert.ToInt32(paramOutput.Value);
                }
            }
            return idGenerado;
        }

        // Actualizar producto
        public void ActualizarProducto(Producto producto)
        {
            using (var conexion = new SqlConnection(_cadenaConexion))
            {
                conexion.Open();
                using (var cmd = new SqlCommand("sp_ActualizarProducto", conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_producto", producto.IdProducto);
                    cmd.Parameters.AddWithValue("@nombre", producto.Nombre);
                    cmd.Parameters.AddWithValue("@descripcion", producto.Descripcion ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@id_color", producto.IdColor ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@id_material", producto.IdMaterial ?? (object)DBNull.Value);
                    cmd.Parameters.AddWithValue("@precio", producto.Precio);
                    cmd.Parameters.AddWithValue("@id_categoria", producto.IdCategoria);

                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Cambiar Estado (Activo / Inactivo)
        public void CambiarEstadoProducto(int idProducto, bool estado)
        {
            using (var conexion = new SqlConnection(_cadenaConexion))
            {
                conexion.Open();
                using (var cmd = new SqlCommand("sp_CambiarEstadoProducto", conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_producto", idProducto);
                    cmd.Parameters.AddWithValue("@estado", estado);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Guardar o actualizar stock de tallas
        public void GuardarProductoTallaStock(int idProducto, int idTalla, int stock)
        {
            using (var conexion = new SqlConnection(_cadenaConexion))
            {
                conexion.Open();
                using (var cmd = new SqlCommand("sp_GuardarProductoTallaStock", conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_producto", idProducto);
                    cmd.Parameters.AddWithValue("@id_talla", idTalla);
                    cmd.Parameters.AddWithValue("@stock", stock);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Registrar imagen asociada (Actualizado con el parámetro @orden)
        public void RegistrarImagen(int idProducto, string imagenUrl, int orden)
        {
            using (var conexion = new SqlConnection(_cadenaConexion))
            {
                conexion.Open();
                using (var cmd = new SqlCommand("sp_RegistrarProductoImagen", conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_producto", idProducto);
                    cmd.Parameters.AddWithValue("@imagen_url", imagenUrl);
                    cmd.Parameters.AddWithValue("@orden", orden);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // --- NUEVOS MÉTODOS DE IMÁGENES ---

        // Listar imágenes por producto (para el modal de editar - Actualizado para mapear Orden)
        public List<ProductoImagen> ListarImagenesPorProducto(int idProducto)
        {
            var lista = new List<ProductoImagen>();
            using (var conexion = new SqlConnection(_cadenaConexion))
            {
                conexion.Open();
                using (var cmd = new SqlCommand("sp_ListarImagenesPorProducto", conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_producto", idProducto);
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new ProductoImagen
                            {
                                IdImagen = Convert.ToInt32(dr["id_imagen"]),
                                IdProducto = Convert.ToInt32(dr["id_producto"]),
                                ImagenUrl = dr["imagen_url"].ToString(),
                                Orden = Convert.ToInt32(dr["orden"])
                            });
                        }
                    }
                }
            }
            return lista;
        }

        // Actualizar URL de una imagen existente (cuando reemplazan una foto)
        public void ActualizarImagen(int idImagen, string imagenUrl)
        {
            using (var conexion = new SqlConnection(_cadenaConexion))
            {
                conexion.Open();
                using (var cmd = new SqlCommand("sp_ActualizarProductoImagen", conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddWithValue("@id_imagen", idImagen);
                    cmd.Parameters.AddWithValue("@imagen_url", imagenUrl);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        // Limpiar tallas anteriores del producto
        public void LimpiarTallasProducto(int idProducto)
        {
            using (var conexion = new SqlConnection(_cadenaConexion))
            {
                var cmd = new SqlCommand("DELETE FROM producto_talla WHERE id_producto = @id", conexion);
                cmd.Parameters.AddWithValue("@id", idProducto);
                conexion.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}