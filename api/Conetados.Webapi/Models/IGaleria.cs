using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Conetados.Webapi.Models
{
    public interface IGaleria
    {
        ICollection<ImagemGaleria> ImagensGaleria { get; set; }
    }
}