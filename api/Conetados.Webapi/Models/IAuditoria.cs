using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conetados.Webapi.Models
{
    interface IAuditoria
    {
        DateTime DataCriacao { get; set; }
        DateTime? DataAlteracao { get; set; }
    }
}
