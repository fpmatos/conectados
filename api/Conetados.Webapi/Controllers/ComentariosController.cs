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
using Conetados.Webapi.Controllers.Base;
using Conetados.Webapi.Services.Comentarios;

namespace Conetados.Webapi.Controllers
{
    public class ComentariosController : ControllerBase
    {
        ComentariosService service;
        Contexto db;

        public ComentariosController(ComentariosService service, Contexto db)
        {
            this.service = service;
            this.db = db;
        }

        [Route("api/Comentarios/total")]
        // GET: api/Comentarios
        public int GetNumeroComentarios(bool? denunciados)
        {
            return service.RetornarTotalComentarios(denunciados);
        }

        // GET: api/Comentarios
        public IEnumerable<ComentarioDto> GetComentarios(int? artigoId)
        {
            return service.RetornarComentarios(artigoId);
        }

        // GET: api/Comentarios/5
        [ResponseType(typeof(Comentario))]
        public async Task<IHttpActionResult> GetComentario(int id)
        {
            Comentario comentario = await service.RetornarComentario(id);

            if (comentario == null)
            {
                return NotFound();
            }

            return Ok(comentario);
        }

        // PUT: api/Comentarios/5
        // POST: api/Comentarios
        [ResponseType(typeof(Comentario))]
        public async Task<IHttpActionResult> PostComentario(ComentarioPostDto comentarioDto)
        {
            var comentario = await service.SalvarComentario(comentarioDto);

            return Ok(comentario);
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