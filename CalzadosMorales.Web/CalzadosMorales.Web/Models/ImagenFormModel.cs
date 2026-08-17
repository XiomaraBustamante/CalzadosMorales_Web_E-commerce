using Microsoft.AspNetCore.Http;

namespace CalzadosMorales.Web.Models
{
    public class ImagenFormModel
    {
        public int Orden { get; set; }
        public IFormFile? Archivo { get; set; }
    }
}