using System.Data;
using Microsoft.Data.SqlClient;
using CalzadosMorales.Web.Models;
using Microsoft.Extensions.Configuration;

namespace CalzadosMorales.Web.Repositorio
{
    public class FiltroTiendaRepository
    {
        private readonly string _cadenaSql;

        public FiltroTiendaRepository(IConfiguration configuration)
        {
            _cadenaSql = configuration.GetConnectionString("CadenaSQL");
        }

        public List<ProductoViewModel> FiltrarCatalogo(int? id_categoria, int? id_color, int? id_material, int? id_talla, decimal? precio_min, decimal? precio_max, string busqueda, string orden)
        {
            var lista = new List<ProductoViewModel>();

            using (var conexion = new SqlConnection(_cadenaSql))
            {
                using (var cmd = new SqlCommand("sp_FiltroCatalogoWeb", conexion))
                {
                    cmd.CommandType = CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("@id_categoria", (object)id_categoria ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@id_color", (object)id_color ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@id_material", (object)id_material ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@id_talla", (object)id_talla ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@precio_min", (object)precio_min ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@precio_max", (object)precio_max ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@busqueda", (object)busqueda ?? DBNull.Value);
                    cmd.Parameters.AddWithValue("@orden", (object)orden ?? "recientes");

                    conexion.Open();
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new ProductoViewModel
                            {
                                id_producto = Convert.ToInt32(dr["id_producto"]),
                                nombre = dr["nombre"].ToString(),
                                precio = Convert.ToDecimal(dr["precio"]),
                                categoria_nombre = dr["categoria_nombre"].ToString(),
                                color_nombre = dr["color_nombre"].ToString(),
                                material_tipo = dr["material_tipo"].ToString(),
                                stock_total = Convert.ToInt32(dr["stock_total"]),
                                imagenes_unidas = dr["imagenes_unidas"].ToString()
                            });
                        }
                    }
                }
            }
            return lista;
        }

        public List<CategoriaViewModel> ObtenerCategorias()
        {
            var lista = new List<CategoriaViewModel>();
            using (var conexion = new SqlConnection(_cadenaSql))
            {
                using (var cmd = new SqlCommand("SELECT id_categoria, nombre FROM categorias", conexion))
                {
                    cmd.CommandType = CommandType.Text;
                    conexion.Open();
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new CategoriaViewModel
                            {
                                id_categoria = Convert.ToInt32(dr["id_categoria"]),
                                nombre = dr["nombre"].ToString()
                            });
                        }
                    }
                }
            }
            return lista;
        }

        public List<ColorViewModel> ObtenerColores()
        {
            var lista = new List<ColorViewModel>();
            using (var conexion = new SqlConnection(_cadenaSql))
            {
                using (var cmd = new SqlCommand("SELECT id_color, nombre FROM colores", conexion))
                {
                    cmd.CommandType = CommandType.Text;
                    conexion.Open();
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new ColorViewModel
                            {
                                id_color = Convert.ToInt32(dr["id_color"]),
                                nombre = dr["nombre"].ToString()
                            });
                        }
                    }
                }
            }
            return lista;
        }

        public List<MaterialViewModel> ObtenerMateriales()
        {
            var lista = new List<MaterialViewModel>();
            using (var conexion = new SqlConnection(_cadenaSql))
            {
                using (var cmd = new SqlCommand("SELECT id_material, tipo FROM materiales", conexion))
                {
                    cmd.CommandType = CommandType.Text;
                    conexion.Open();
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new MaterialViewModel
                            {
                                id_material = Convert.ToInt32(dr["id_material"]),
                                tipo = dr["tipo"].ToString()
                            });
                        }
                    }
                }
            }
            return lista;
        }

        public List<TallaViewModel> ObtenerTallas()
        {
            var lista = new List<TallaViewModel>();
            using (var conexion = new SqlConnection(_cadenaSql))
            {
                // Usamos 'nombre AS numero' para que coincida con lo que tu C# espera leer
                using (var cmd = new SqlCommand("SELECT id_talla, nombre FROM tallas", conexion))
                {
                    cmd.CommandType = CommandType.Text;
                    conexion.Open();
                    using (var dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            lista.Add(new TallaViewModel()
                            {
                                id_talla = Convert.ToInt32(dr["id_talla"]),
                                nombre = dr["nombre"].ToString()
                            });
                        }
                    }
                }
            }
            return lista;
        }
    }
}