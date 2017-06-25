using Conetados.Webapi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conetados.Webapi.Infraestrutura.Autenticacao.AutenticacaoSap
{
    public interface IAutenticacaoSapService
    {
        Usuario Autenticar(string matricula, string cpf);
    }
}
