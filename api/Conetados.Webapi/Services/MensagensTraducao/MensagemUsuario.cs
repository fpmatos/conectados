using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Conetados.Webapi.Services
{
    public class MensagemUsuario
    {
        public bool ErroCritico { get; set; }
        public string Mensagem { get; set; }
    }
}