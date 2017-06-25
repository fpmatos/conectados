using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Conetados.Webapi.Models
{
    public interface IEnquete
    {
        ICollection<Alternativa> Alternativas { get; set; }
        DateTime? DataEncerramentoEnquete { get; set; }

        [NotMapped]
        int? RespostaEnqueteId { set; get; }
    }
}