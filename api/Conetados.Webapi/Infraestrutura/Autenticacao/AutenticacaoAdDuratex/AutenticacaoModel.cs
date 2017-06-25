using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Conetados.Webapi.Infraestrutura
{
    public class AutenticacaoModel
    {
        public string Nome { get; set; }
        public List<string> Grupos { get; set; }
        public Guid? IdRepresentante { get; set; }
        public string Message { get; set; }
        public bool UsuarioERepresentante() => IdRepresentante.HasValue && IdRepresentante.Value != Guid.Empty;
        public string Token
        {
            get
            {
                var data = DateTime.Now.AddMinutes(480);
                byte[] bytes = Encoding.UTF8.GetBytes(data.ToString("MM/dd/yyyy HH:mm:ss"));
                return Convert.ToBase64String(bytes.ToArray());
            }
            internal set
            {
            }
        }
    }
}