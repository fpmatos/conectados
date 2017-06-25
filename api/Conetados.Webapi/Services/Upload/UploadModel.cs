namespace Conetados.Webapi.Services
{
    public class UploadModel
    {
        public int ArquivoId { get; set; }
        public float Height { get; set; }
        public string MediaType { get; set; }
        public string Nome { get; set; }
        public float Width { get; set; }
    }
}