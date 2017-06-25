using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace Conetados.Webapi.Models
{
    public interface IImagem
    {
        string Descricao { get; set; }
        int? UploadId { get; set; }
        [ForeignKey("UploadId")]
        Upload Upload { get; set; }
    }
}