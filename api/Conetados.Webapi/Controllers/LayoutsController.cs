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
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace Conetados.Webapi.Controllers
{
    public class LayoutsController : ApiController
    {
        private Contexto db = new Contexto();

        // GET: api/Layouts
        public IQueryable<Layout> GetLayouts()
        {
            return db.Layouts;
        }

        // GET: api/Layouts/5
        [ResponseType(typeof(Layout))]
        public async Task<IHttpActionResult> GetLayout(int id)
        {
            Layout layout = await db.Layouts.FindAsync(id);
            if (layout == null)
            {
                return NotFound();
            }

            await db.Entry(layout).Collection(x => x.Conteudos).Query().OrderBy(x => x.Ordem).LoadAsync();

            foreach (var enquete in layout.Conteudos.Where(x => x.TipoConteudo == TipoConteudo.Enquete))
            {
                await db.Entry(enquete).Collection(x => x.Alternativas).LoadAsync();
            }

            foreach (var imagem in layout.Conteudos.Where(x => x.TipoConteudo == TipoConteudo.Imagem))
            {
                await db.Entry(imagem).Reference(x => x.Upload).LoadAsync();
            }

            foreach (var galeria in layout.Conteudos.Where(x => x.TipoConteudo == TipoConteudo.Galeria))
            {
                await db.Entry(galeria).Collection(x => x.ImagensGaleria).Query().OrderBy(x => x.Ordem).Include(item => item.Upload).LoadAsync();
            }

            return Ok(layout);
        }

        [ResponseType(typeof(Layout))]
        [Route("api/layouts/{id}/copia")]
        public async Task<IHttpActionResult> GetCopia(int id)
        {
            Layout layout = await db.Layouts.FindAsync(id);
            if (layout == null)
                return NotFound();

            await db.Entry(layout).Collection(x => x.Conteudos).Query().OrderBy(x => x.Ordem).LoadAsync();

            foreach (var enquete in layout.Conteudos.Where(x => x.TipoConteudo == TipoConteudo.Enquete))
            {
                await db.Entry(enquete).Collection(x => x.Alternativas).LoadAsync();
            }

            foreach (var imagem in layout.Conteudos.Where(x => x.TipoConteudo == TipoConteudo.Imagem))
            {
                await db.Entry(imagem).Reference(x => x.Upload).LoadAsync();
            }

            foreach (var galeria in layout.Conteudos.Where(x => x.TipoConteudo == TipoConteudo.Galeria))
            {
                await db.Entry(galeria).Collection(x => x.ImagensGaleria).Query().OrderBy(x => x.Ordem).Include(item => item.Upload).LoadAsync();
            }

            var settings = new JsonSerializerSettings()
            {
                ReferenceLoopHandling = ReferenceLoopHandling.Ignore,
                PreserveReferencesHandling = PreserveReferencesHandling.None
            };
            
            var artigo = JsonConvert.DeserializeObject<Artigo>(JsonConvert.SerializeObject(layout, settings));
            
            artigo.Id = 0;
            artigo.LayoutId = layout.Id;
            artigo.DataCriacao = DateTime.Now;
            artigo.DataAlteracao = null;
            artigo.DataPublicacao = artigo.DataCriacao;
            foreach (var conteudo in artigo.Conteudos)
            {
                conteudo.Id = 0;
                conteudo.ArtigoId = 0;
                foreach (var alternativa in conteudo.Alternativas)
                {
                    alternativa.Id = 0;
                    alternativa.EnqueteId = 0;
                }
                foreach (var imagem in conteudo.ImagensGaleria)
                {
                    imagem.Id = 0;
                    imagem.GaleriaId = 0;
                }
            }

            return Ok(artigo);
        }

        // PUT: api/Layouts/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutLayout(int id, Layout layout)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != layout.Id)
            {
                return BadRequest();
            }

            db.Entry(layout).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!LayoutExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Layouts
        [ResponseType(typeof(Layout))]
        public async Task<IHttpActionResult> PostLayout(Layout layout)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Layouts.Add(layout);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = layout.Id }, layout);
        }

        // DELETE: api/Layouts/5
        [ResponseType(typeof(Layout))]
        public async Task<IHttpActionResult> DeleteLayout(int id)
        {
            Layout layout = await db.Layouts.FindAsync(id);
            if (layout == null)
            {
                return NotFound();
            }

            db.Layouts.Remove(layout);
            await db.SaveChangesAsync();

            return Ok(layout);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool LayoutExists(int id)
        {
            return db.Layouts.Count(e => e.Id == id) > 0;
        }
    }
}