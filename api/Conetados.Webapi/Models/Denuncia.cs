using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Conetados.Webapi.Models
{
    public class Denuncia
    {
        public int ComentarioId { get; set; }
        public Comentario Comentario {get;set;}
        public int Id { get; set; }
        public string UsuarioAppId { get; set; }

    }
}