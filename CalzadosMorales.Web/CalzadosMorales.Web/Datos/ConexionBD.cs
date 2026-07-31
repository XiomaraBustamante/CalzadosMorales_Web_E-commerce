namespace CalzadosMorales.Web.Datos
{
    public class ConexionBD
    {
        private readonly IConfiguration _configuration;
        private readonly string _cadena;

        public ConexionBD(IConfiguration configuration)
        {
            _configuration = configuration;
            // Lee la cadena "CadenaSQL" que tienes en tu archivo appsettings.json
            _cadena = _configuration.GetConnectionString("CadenaSQL") ?? "";
        }

        public string ObtenerCadena()
        {
            return _cadena;
        }
    }
}
