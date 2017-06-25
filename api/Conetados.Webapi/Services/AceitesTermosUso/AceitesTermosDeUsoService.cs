using Conetados.Webapi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Data.Entity;
using Conetados.Webapi.Infraestrutura;

namespace Conetados.Webapi.Services.AceitesTermosUso
{
    public class AceitesTermosDeUsoService
    {
        Contexto db;
        UsuarioContexto usuarioContexto;

        public AceitesTermosDeUsoService(Contexto contexto, UsuarioContexto usuarioContexto)
        {
            this.db = contexto;
            this.usuarioContexto = usuarioContexto;
        }

        public IQueryable<AceiteTermoUso> Retornar()
        {
            return db.TermoUsoAceites.OrderByDescending(item => item.DataAceite);
        }

        public async Task<bool> VerificarSeUsuarioJaAceitou()
        {
            var idUsuario = this.usuarioContexto.NomeDeUsuario;
            var jaAceito = await db.TermoUsoAceites.AnyAsync(item => item.MatriculaUsuario.Equals(idUsuario));

            return jaAceito;
        }

        public async Task<AceiteTermoUso> AceitarTermoDeUso()
        {
            var idUsuario = this.usuarioContexto.NomeDeUsuario;
            var nomePerfil = this.usuarioContexto.NomeDePerfil;

            var jaAceitouTermoDeUso = await db.TermoUsoAceites.AnyAsync(item => item.MatriculaUsuario.Equals(idUsuario));

            AceiteTermoUso aceiteTermoDeUso = null;

            if (jaAceitouTermoDeUso)
                throw new BusinessServiceException("Termo de uso já foi aceito.");
            else
            {
                aceiteTermoDeUso = db.TermoUsoAceites.Add(new AceiteTermoUso { DataAceite = DateTime.Now, MatriculaUsuario = idUsuario, NomeUsuario = nomePerfil });
                await db.SaveChangesAsync();
            }        
            
            return aceiteTermoDeUso;
        }
    }
}