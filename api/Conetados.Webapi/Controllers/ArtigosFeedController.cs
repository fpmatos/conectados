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
using Conetados.Webapi.Services.Artigos;
using Conetados.Webapi.Services.Comentarios;
using Conetados.Webapi.Infraestrutura;

namespace Conetados.Webapi.Controllers
{
    [Authorize]
    public class ArtigosFeedController : ControllerBase
    {
        private ComentariosService comentariosService;
        private Contexto db;
        private ArtigosService _artigoService;

        public ArtigosFeedController(ComentariosService service, Contexto db, ArtigosService artigoService)
        {
            this.comentariosService = service;
            this._artigoService = artigoService;
            this.db = db;
        }

        private async Task<IQueryable<Artigo>> DefinirQueryPaginacao(IQueryable<Artigo> query, FeedArtigoPesquisaDTO feedArtigoQuery)
        {
            if (feedArtigoQuery == null || !feedArtigoQuery.PorRequisicao.HasValue)
                return query;

            feedArtigoQuery.PaginaCorrente = feedArtigoQuery.PaginaCorrente.HasValue ? feedArtigoQuery.PaginaCorrente.Value + 1 : 1;
           
            
            var result = query
                .Skip((feedArtigoQuery.PaginaCorrente.Value - 1) * feedArtigoQuery.PorRequisicao.Value);

            var registrosASeremLidos = await result.CountAsync();
            feedArtigoQuery.TodosRegistrosLidos = registrosASeremLidos <= feedArtigoQuery.PorRequisicao.Value;


            result=  result.Take(feedArtigoQuery.PorRequisicao.Value);

            return result;
        }

        private async Task<IQueryable<Artigo>> DefinirFiltro(IQueryable<Artigo> query, FeedArtigoPesquisaDTO feedArtigoQuery)
        {
            if (feedArtigoQuery == null)
                return query;

            var result = query.Where(item => (string.IsNullOrEmpty(feedArtigoQuery.TituloPesquisa) || item.Titulo.Contains(feedArtigoQuery.TituloPesquisa))
                && (!feedArtigoQuery.TagId.HasValue || item.Tags.Any(tag => tag.Id.Equals(feedArtigoQuery.TagId.Value)))
                && (!feedArtigoQuery.ApenasCurtidos || item.Curtidas.Any(curtida => curtida.UsuarioAppId.Equals(UsuarioContexto.NomeDeUsuario))));

            return result;
        }

        [Route("api/ArtigosFeed/pesquisa")]
        [HttpPost]
        // GET: api/ArtigosFeed
        public async Task<IHttpActionResult> GetArtigos(FeedArtigoPesquisaDTO feedArtigoQuery)
        {



            var usuarioAppId = base.UsuarioContexto.NomeDeUsuario;

            var query = db.Artigos
                .Where(item => item.Ativo && (!item.DataPublicacao.HasValue || item.DataPublicacao.Value <= DateTime.Now))
                .OrderByDescending(item => item.DataPublicacao) as IQueryable<Artigo>;

            query = await DefinirFiltro(query, feedArtigoQuery);
            query = await DefinirQueryPaginacao(query, feedArtigoQuery);

            var artigos = await query.Select(x => new ArtigoBasicoFeedDTO
                {
                    Id = x.Id,
                    Titulo = x.Titulo,
                    Thumbnail = x.Thumbnail,
                    DataCriacao = x.DataCriacao,
                    DataAlteracao = x.DataAlteracao,
                    DataPublicacao = x.DataPublicacao,
                    Tags = x.Tags,
                    TotalCurtidas = x.Curtidas.Count,
                    TotalComentarios = x.Comentarios.Count,
                    UsuarioJaCurtiu = x.Curtidas.FirstOrDefault(item => item.UsuarioAppId.Equals(usuarioAppId)) != null
                }).ToListAsync();


            var result = new FeedArtigoResultadotDto { Data = artigos, Metadata = feedArtigoQuery };

            return Ok(result);
        }

        // GET: api/ArtigosFeed/5
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

        [HttpPost]
        [Route("api/ArtigosFeed/{id}/curtir")]
        [ResponseType(typeof(Curtida))]
        // POST: api/ArtigosFeed/5/curtir
        public async Task<IHttpActionResult> CurtirArtigo(int id)
        {
            var usuarioId = UsuarioContexto.NomeDeUsuario;
            var usuarioJaCurtiu = await db.Curtidas.AnyAsync(item => item.ArtigoId.Equals(id) && item.UsuarioAppId.Equals(usuarioId));

            if(usuarioJaCurtiu)
                return Ok();

            var curtida = new Curtida { ArtigoId = id, UsuarioAppId = base.UsuarioContexto.NomeDeUsuario, UsuarioAppNome = base.UsuarioContexto.NomeDePerfil };

            db.Curtidas.Add(curtida);

            await db.SaveChangesAsync();

            return Ok(curtida);
        }

        [HttpPost]
        [Route("api/ArtigosFeed/{id}/descurtir")]
        // GET: api/ArtigosFeed
        public async Task<IHttpActionResult> DescurtirArtigo(int id)
        {
            var usuarioId = UsuarioContexto.NomeDeUsuario;

            var curtida = await db.Curtidas.FirstOrDefaultAsync(item => item.ArtigoId.Equals(id) && item.UsuarioAppId.Equals(usuarioId));

            if(curtida != null)
                db.Curtidas.Remove(curtida);

            await db.SaveChangesAsync();

            return StatusCode(HttpStatusCode.NoContent);
        }

        [HttpPost]
        [Route("api/ArtigosFeed/{id}/comentar")]
        [ResponseType(typeof(ComentarioDto))]
        // GET: api/ArtigosFeed
        public async Task<IHttpActionResult> ComentarArtigo(int id, string mensagem)
        {
            var comentario = await this.comentariosService.SalvarComentario(new ComentarioPostDto { ArtigoId = id, Mensagem = mensagem });

            return Ok(comentario);
        }

        [HttpPost]
        [Route("api/ArtigosFeed/{id}/comentarios/{comentarioId}/denunciar")]
        [ResponseType(typeof(Denuncia))]
        // GET: api/ArtigosFeed
        public async Task<IHttpActionResult> DenunciarComentario(int id, int comentarioId)
        {
            if(db.Denuncias.Any(item => item.ComentarioId.Equals(comentarioId) && item.UsuarioAppId.Equals(UsuarioContexto.NomeDeUsuario)))
                return Ok();

            var denuncia = new Denuncia();

            denuncia.ComentarioId = comentarioId;
            denuncia.UsuarioAppId = base.UsuarioContexto.NomeDeUsuario;

            db.Denuncias.Add(denuncia);

            await db.SaveChangesAsync();

            return Ok(denuncia);
        }

        [HttpGet]
        [Route("api/ArtigosFeed/{id}/NumeroComentarios")]
        [ResponseType(typeof(int))]
        // GET: api/ArtigosFeed
        public async Task<IHttpActionResult> RetornarNumeroComentarios(int id)
        {
            var quantidadeComentarios = await db.Comentarios.CountAsync(item => item.ArtigoId.Equals(id));

            return Ok(quantidadeComentarios);
        }

        [HttpGet]
        [Route("api/ArtigosFeed/{id}/comentarios")]
        [ResponseType(typeof(IEnumerable<ComentarioDto>))]
        // GET: api/ArtigosFeed
        public async Task<IHttpActionResult> RetornarComentarios(int id)
        {
            var itens = this.comentariosService.RetornarComentarios(id)
                .Where(item => !item.MarcadoComoImproprio);

            return Ok(itens.ToList());
        }

        [HttpGet]
        [Route("api/ArtigosFeed/{id}/numeroCurtidas")]
        [ResponseType(typeof(int))]
        // GET: api/ArtigosFeed
        public async Task<IHttpActionResult> RetornarNumeroCurtidas(int id)
        {
            var quantidadeCurtidas = await db.Curtidas.CountAsync(item => item.ArtigoId.Equals(id));

            return Ok(quantidadeCurtidas);
        }

        [HttpPost]
        [Route("api/ArtigosFeed/{artigoId}/Enquetes/{enqueteId}/Alternativas/{alternativaId}/Responder")]
        [ResponseType(typeof(Curtida))]
        // POST: api/ArtigosFeed/5/curtir
        public async Task<IHttpActionResult> ResponderEnquete(int artigoId, int enqueteId, int alternativaId)
        {
            var usuarioId = UsuarioContexto.NomeDeUsuario;
            var usuarioJaRespondeu = await db.AlternativasRespostas.AnyAsync(item => item.UsuarioAppId.Equals(usuarioId) && item.AlternativaId == alternativaId);

            if (usuarioJaRespondeu)
                return Ok();

            var resposta = new AlternativaResposta { AlternativaId = alternativaId, UsuarioAppId = base.UsuarioContexto.NomeDeUsuario, UsuarioAppNome = base.UsuarioContexto.NomeDePerfil };

            db.AlternativasRespostas.Add(resposta);

            await db.SaveChangesAsync();

            return Ok(resposta);
        }
    }
}