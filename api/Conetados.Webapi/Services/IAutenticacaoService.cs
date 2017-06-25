using Conetados.Webapi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conetados.Webapi.Services
{
    public enum AutenticacaoTipo
    {
        Mobile,
        Cms
    }

    public interface IAutenticacaoService
    {
        Usuario Autenticar(AutenticacaoTipo autenticacaoTipo, string nomeDeUsuario, string Senha);
    }
}
