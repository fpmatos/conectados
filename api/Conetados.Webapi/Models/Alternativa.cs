using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Conetados.Webapi.Models
{
    public class Alternativa
    {
        public int Id { get; set; }
        public string Descricao { get; set; }
        public ICollection<AlternativaResposta> Respostas { get; set; } = new List<AlternativaResposta>();
        [NotMapped]
        public int? TotalRespostas { set; get; }
        //Remover
        public int EnqueteId { get; set; }
        [ForeignKey("EnqueteId")]
        public Conteudo Enquete { get; set; }
    }
}