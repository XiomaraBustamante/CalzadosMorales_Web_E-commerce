using CalzadosMorales.Web.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace CalzadosMorales.Web.Repositorio
{
    public class ProductoTiendaRepository
    {
        private readonly string _cadenaConexion;

        public ProductoTiendaRepository(IConfiguration configuration)
        {
            _cadenaConexion = configuration.GetConnectionString("CadenaSQL");
        }

        // Método para listar los productos y sus imágenes unidas por categoría
        public List<ProductoTienda> ListarCatalogoPorCategoria(string categoria)
        {
            var lista = new List<ProductoTienda>();
            try
            {
                using (var conexion = new SqlConnection(_cadenaConexion))
                {
                    conexion.Open();
                    using (var cmd = new SqlCommand("sp_ListarCatalogoPorCategoria", conexion))
                    {
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@nombre_categoria", categoria);

                        using (var dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                lista.Add(new ProductoTienda
                                {
                                    IdProducto = Convert.ToInt32(dr["id_producto"]),
                                    Nombre = dr["nombre"].ToString(),
                                    Precio = Convert.ToDecimal(dr["precio"]),
                                    ImagenesUnidas = dr["imagenes_unidas"] != DBNull.Value ? dr["imagenes_unidas"].ToString() : string.Empty
                                });
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception("Error al listar el catálogo por categoría: " + ex.Message);
            }
            return lista;
        }
    }
}