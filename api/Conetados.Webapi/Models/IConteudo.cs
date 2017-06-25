using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Conetados.Webapi.Models
{
    public interface IConteudo
    {
        int Id { get; set; }
        int Ordem { get; set; }
        int ArtigoId { get; set; }
        TipoConteudo TipoConteudo { get; set; }
    }
}