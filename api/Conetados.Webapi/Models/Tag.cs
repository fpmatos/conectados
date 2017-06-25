using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Conetados.Webapi.Models
{
    public class Tag
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descrição { get; set; }
        public bool Editoria { get; set; }
        public ICollection<Artigo> Artigos { get; set; } = new List<Artigo>();
    }
}