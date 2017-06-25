using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conetados.Webapi.Models
{
    interface IPossuiUsuarioApp
    {
        string UsuarioAppId { get; set; }
        string UsuarioAppNome { get; set; }
    }
}
