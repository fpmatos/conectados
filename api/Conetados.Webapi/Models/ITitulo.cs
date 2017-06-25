using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Conetados.Webapi.Models
{
    public interface ITitulo
    {
        int? Importancia { get; set; }
        string TextoTitulo { get; set; }
    }
}