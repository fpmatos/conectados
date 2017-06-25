using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Conetados.Webapi;
using Conetados.Webapi.Models;
using Conetados.Webapi.Infraestrutura;
using Conetados.Webapi.Controllers.Base;
using Conetados.Webapi.Services.AceitesTermosUso;

namespace Conetados.Webapi.Controllers
{
    [Authorize]
    public class AceitesTermoUsoController: ControllerBase
    {
        private Contexto db;
        private AceitesTermosDeUsoService service;

        public AceitesTermoUsoController()
        {
            this.service = InjectorManager.GetInstance<AceitesTermosDeUsoService>();
            this.db = InjectorManager.GetInstance<Contexto>();
        }

        // GET: api/AceitesTermoUso
        public IQueryable<AceiteTermoUso> GetTermoUsoAceites()
        {
            return this.service.Retornar();
        }

        [Route("Api/AceitesTermoUso/count")]
        [ResponseType(typeof(int))]
        public async Task<IHttpActionResult> GetCount()
        {
            var result =  await this.service.Retornar().CountAsync();
            return Ok(result);
        }

        [Route("Api/AceitesTermoUso/jaAceitou")]
        [ResponseType(typeof(bool))]
        public async Task<IHttpActionResult> GetVerificaSeTermoDeUsoJaAceito()
        {
            var result = await this.service.VerificarSeUsuarioJaAceitou();

            return Ok(result);
        }

        // POST: api/AceitesTermoUso
        [HttpPost]
        [ResponseType(typeof(AceiteTermoUso))]
        public async Task<IHttpActionResult> PostAceiteTermoUso()
        {
            var result = await service.AceitarTermoDeUso();

            return Ok(result);
        }


        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}