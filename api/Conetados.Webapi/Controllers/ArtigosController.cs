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
using Conetados.Webapi.Infraestrutura;
using RefactorThis.GraphDiff;
using Conetados.Webapi.Services;
using Conetados.Webapi.Services.Artigos;

namespace Conetados.Webapi.Controllers
{
    [Authorize]
    public class ArtigosController : ControllerBase
    {
        private Contexto db;
        private ArtigosService _artigoService;        

        public ArtigosController(Contexto contexto, ArtigosService artigoService)
        {
            db = contexto;
            _artigoService = artigoService;
        }

        [Route("api/Artigos/total")]
        // GET: api/Comentarios
        public int GetNumeroArtigos()
        {
            return db.Artigos.Count(item => item.Ativo);
        }

        // GET: api/Artigos
        [ResponseType(typeof(IEnumerable<ArtigoSumaryDto>))]
        public async Task<IHttpActionResult> GetArtigos()
        {
            var artigos = await _artigoService.ListarArtigos();
            return Ok(artigos);
        }

        // GET: api/Artigos/5
        [ResponseType(typeof(Artigo))]
        public async Task<IHttpActionResult> GetArtigo(int id)
        {
            Artigo artigo = await _artigoService.ObterArtigo(id);

            if (artigo == null)
            {
                return NotFound();
            }

            return Ok(artigo);
        }

        // PUT: api/Artigos/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutArtigo(int id, Artigo artigoExistente)
        {            
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != artigoExistente.Id)
            {
                return BadRequest();
            }            

            artigoExistente = await _artigoService.AdicionarArtigo(artigoExistente);

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Artigos
        [ResponseType(typeof(Artigo))]
        public async Task<IHttpActionResult> PostArtigo(Artigo artigoNovo)
        {            
            artigoNovo = await _artigoService.AdicionarArtigo(artigoNovo);
            return CreatedAtRoute("DefaultApi", new { id = artigoNovo.Id }, artigoNovo);
        }

        // DELETE: api/Artigos/5
        [ResponseType(typeof(Artigo))]
        public async Task<IHttpActionResult> DeleteArtigo(int id)
        {
            Artigo artigo = await db.Artigos.FindAsync(id);
            if (artigo == null)
            {
                return NotFound();
            }

            db.Artigos.Remove(artigo);
            await db.SaveChangesAsync();

            return Ok(artigo);
        }

        [HttpGet]
        [Route("api/Artigos/{id}/ativacao")]
        [ResponseType(typeof(Artigo))]
        public async Task<IHttpActionResult> AtivaArtigo(int id, bool value)
        {
            var artigo = await db.Artigos.FindAsync(id);
            if (artigo == null)
            {
                return NotFound();
            }

            artigo.Ativo = value;
            await db.SaveChangesAsync();

            return Ok(artigo);
        }

        [HttpGet]
        [Route("api/Artigos/{id}/Comentarios/{comentarioId}/negativa")]
        [ResponseType(typeof(Artigo))]
        public async Task<IHttpActionResult> NegativaComentario(int id, int comentarioId, bool value)
        {
            var comentario = await db.Comentarios.FindAsync(comentarioId);
            if (comentario == null)
            {
                return NotFound();
            }

            comentario.MarcadoComoImproprio = value;
            await db.SaveChangesAsync();

            return Ok(comentario);
        }

        [HttpGet]
        [Route("api/Artigos/{id}/Comentarios")]
        [ResponseType(typeof(IEnumerable<Comentario>))]
        public async Task<IHttpActionResult> GetComentarios(int id)
        {
            var artigo = await db.Artigos
                .Include(_artigo => _artigo.Comentarios)
                .FirstOrDefaultAsync(_artigo => _artigo.Id.Equals(id));


            if (artigo == null)
            {
                return NotFound();
            }

            var comentarios = artigo.Comentarios;

            return Ok(comentarios.ToList());
        }

        [HttpGet]
        [Route("api/Artigos/TotalArtigos")]
        [ResponseType(typeof(IEnumerable<Comentario>))]
        public async Task<IHttpActionResult> GetTotalArtigos()
        {
            var total = await db.Artigos.CountAsync();
            return Ok(total);
        }

        [HttpGet]
        [Route("api/Artigos/TotalComentarios")]
        [ResponseType(typeof(IEnumerable<Comentario>))]
        public async Task<IHttpActionResult> GetTotalComentarios()
        {
            var total = await db.Comentarios.CountAsync();
            return Ok(total);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ArtigoExists(int id)
        {
            return db.Artigos.Count(e => e.Id == id) > 0;
        }
    }
}