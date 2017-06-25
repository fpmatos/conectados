using Conetados.Webapi.Infraestrutura;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Conetados.Webapi.Services
{
    public static class MensagensTraducaoService
    {
        public static MensagemUsuario Traduzir(Exception exception)
        {
            var mensagem = new MensagemUsuario {
                ErroCritico = true,
                Mensagem = "Ops! Erro interno ocorreu. Contacte a área responsável."
            };

            if(exception is BusinessServiceException)
            {
                mensagem.ErroCritico = false;
                mensagem.Mensagem = exception.Message;
            }

            return mensagem;
        }
    }
}