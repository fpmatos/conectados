using Conetados.Webapi.Models;
using RefactorThis.GraphDiff;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace Conetados.Webapi.Services.Artigos
{
    public class ArtigosService
    {
        private Contexto db;
        private UsuarioContexto usuario;

        public ArtigosService(Contexto contexto, UsuarioContexto usuario)
        {
            db = contexto;
            this.usuario = usuario;
        }

        public Task<List<ArtigoSumaryDto>> ListarArtigos()
        {
            return db.Artigos
                .OrderByDescending(item => item.DataPublicacao)
                .Select(x => new ArtigoSumaryDto
                {
                    Id = x.Id,
                    Titulo = x.Titulo,
                    Thumbnail = x.Thumbnail,
                    LayoutId = x.LayoutId,
                    LayoutNome = x.Layout.Titulo,
                    UsuarioCmsId = x.UsuarioCmsId,
                    UsuarioCmsNome = x.UsuarioCmsNome,
                    DataCriacao = x.DataCriacao,
                    DataAlteracao = x.DataAlteracao,
                    DataPublicacao = x.DataPublicacao,
                    Ativo = x.Ativo,
                    Tags = x.Tags,
                    TotalCurtidas = x.Curtidas.Count,
                    TotalComentarios = x.Comentarios.Count,
                    TotalComentariosImproprios = x.Comentarios.Count(y => y.MarcadoComoImproprio)
                })
                .ToListAsync();
        }
        public async Task<Artigo> ObterArtigo(int id)
        {
            Artigo artigo = await db.Artigos.FindAsync(id);

            if (artigo == null)
                return null;

            await db.Entry(artigo).Reference(x => x.Layout).LoadAsync();

            await db.Entry(artigo).Collection(x => x.Conteudos).Query().OrderBy(x => x.Ordem).LoadAsync();

            await db.Entry(artigo).Collection(x => x.Tags).LoadAsync();            

            foreach (var imagem in artigo.Conteudos.Where(x => x.TipoConteudo == TipoConteudo.Imagem))
            {
                await db.Entry(imagem).Reference(x => x.Upload).LoadAsync();
            }

            foreach (var galeria in artigo.Conteudos.Where(x => x.TipoConteudo == TipoConteudo.Galeria))
            {
                await db.Entry(galeria).Collection(x => x.ImagensGaleria).Query().OrderBy(x => x.Ordem).Include(item => item.Upload).LoadAsync();
            }

            foreach (var enquete in artigo.Conteudos.Where(x => x.TipoConteudo == TipoConteudo.Enquete))
            {
                await db.Entry(enquete).Collection(x => x.Alternativas).Query().LoadAsync();
                
                    var alternativasIds = enquete.Alternativas.Select(x => x.Id).ToArray();
                    var respostas = db.AlternativasRespostas.Where(resposta => alternativasIds.Contains(resposta.AlternativaId));

                    if (usuario.UsuarioApp)
                    {
                        enquete.RespostaEnqueteId = respostas
                            .Where(resposta => usuario.NomeDeUsuario == resposta.UsuarioAppId)
                            .Select(x => x.AlternativaId).FirstOrDefault();
                    }

                    if (usuario.UsuarioCms
                        || ! enquete.DataEncerramentoEnquete.HasValue
                        || enquete.DataEncerramentoEnquete < DateTime.Now)
                    {
                        var totalRespostas = respostas.GroupBy(x => x.AlternativaId)
                            .Select(x => new { Id = x.Key, TotalRespostas = x.Count() })
                            .ToList();

                        var intersecao = totalRespostas.Join(enquete.Alternativas,
                            resposta => resposta.Id,
                            alternativa => alternativa.Id,
                            (resposta, alternativa) => new { alternativa, resposta });

                        foreach (var item in intersecao)
                        {
                            item.alternativa.TotalRespostas = item.resposta.TotalRespostas;
                        }                                                
                    }
                
            }

            if (usuario.UsuarioApp)
            {
                artigo.TotalCurtidas = db.Entry(artigo).Collection(x => x.Curtidas).Query().Count();
                artigo.TotalComentarios = db.Entry(artigo).Collection(x => x.Comentarios).Query().Count();
                artigo.UsuarioJaCurtiu = db.Entry(artigo).Collection(x => x.Curtidas).Query().Any(item => item.UsuarioAppId.Equals(usuario.NomeDeUsuario));
            }

            return artigo;
        }

        public async Task<Artigo> AdicionarArtigo(Artigo artigoNovo)
        {
            AppendUsuario(artigoNovo);
            AppendThumbnail(artigoNovo);
            AppendTitulo(artigoNovo);
            artigoNovo.DataPublicacao =  (artigoNovo.DataPublicacao.HasValue ? artigoNovo.DataPublicacao.Value.Date : artigoNovo.DataCriacao);

            artigoNovo = db.UpdateGraph(artigoNovo, map => map
                .AssociatedEntity(artigo => artigo.Layout)
                .AssociatedCollection(artigo => artigo.Tags)
                .OwnedCollection(artigo => artigo.Conteudos, with => with
                    .OwnedCollection(conteudo => conteudo.Alternativas)
                    .OwnedCollection(conteudo => conteudo.ImagensGaleria)));

            await db.SaveChangesAsync();

            return artigoNovo;
        }

        public async Task<Artigo> AtualizarArtigo(Artigo artigoExistente)
        {
            AppendUsuario(artigoExistente);
            AppendThumbnail(artigoExistente);
            AppendTitulo(artigoExistente);
            artigoExistente.DataPublicacao = artigoExistente.DataPublicacao ?? artigoExistente.DataCriacao;

            artigoExistente.DataAlteracao = DateTime.Now;            

            artigoExistente = db.UpdateGraph(artigoExistente, map => map
                .AssociatedEntity(artigo => artigo.Layout)
                .AssociatedCollection(artigo => artigo.Tags)
                .OwnedCollection(artigo => artigo.Conteudos, with => with
                    .OwnedCollection(conteudo => conteudo.Alternativas)
                    .OwnedCollection(conteudo => conteudo.ImagensGaleria)));

            await db.SaveChangesAsync();


            return artigoExistente;
        }

        private byte[] RetornarThumbnailUpload(int uploadId)
        {
            byte[] thumbnail = null;

            var upload = db.Uploads.FirstOrDefault(item => item.Id.Equals(uploadId));

            if (upload != null)
                thumbnail = upload.Blob;

            return thumbnail;
        }

        private byte[] RetornarThumbnailVideo(string youtubeId)
        {
            var service = new YoutubeThumbnailService();
            return service.RetornarThumbnailVideo(youtubeId);
        }

        private void AppendThumbnail(Artigo artigo)
        {
            byte[] thumbnail = null;
            var conteudo = artigo
                .Conteudos
                .FirstOrDefault(item =>
                        item.TipoConteudo == TipoConteudo.Video || item.TipoConteudo == TipoConteudo.Imagem || item.TipoConteudo == TipoConteudo.Galeria);

            if (conteudo != null)
            {
                switch (conteudo.TipoConteudo)
                {
                    case TipoConteudo.Video:
                        thumbnail = RetornarThumbnailVideo(conteudo.YoutubeVideoId);
                        break;
                    case TipoConteudo.Imagem:
                        thumbnail = RetornarThumbnailUpload(conteudo.UploadId.Value);
                        break;
                    case TipoConteudo.Galeria:
                        {
                            var imagemGaleria = conteudo.ImagensGaleria.FirstOrDefault();

                            if (imagemGaleria != null)
                                thumbnail = RetornarThumbnailUpload(imagemGaleria.UploadId.Value);
                        }
                        break;
                }

                artigo.Thumbnail = thumbnail;
            }
        }

        private void AppendTitulo(Artigo artigo)
        {

            var conteudo = artigo
                .Conteudos
                .FirstOrDefault(item =>
                        item.TipoConteudo == TipoConteudo.Titulo);

            if (conteudo != null)
                artigo.Titulo = conteudo.TextoTitulo;
        }

        private void AppendUsuario(Artigo artigo)
        {
            artigo.UsuarioCmsId = usuario.NomeDeUsuario;
            artigo.UsuarioCmsNome = usuario.NomeDePerfil;
        }
    }
}