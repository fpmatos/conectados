using Conetados.Webapi.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Threading.Tasks;

namespace Conetados.Webapi.Services.Comentarios
{
    public class ComentariosService
    {
        Contexto db;
        UsuarioContexto usuarioContexto;

        private ComentarioDto ConvertToDto(Comentario item)
        {
            return new ComentarioDto
            {
                Id = item.Id,
                ArtigoId = item.ArtigoId,
                ArtigoTitulo = item.Artigo != null ? item.Artigo.Titulo : "",
                DataCriacao = item.DataCriacao,
                Denunciado = db.Denuncias.Count(_item => item.Id.Equals(_item.ComentarioId)) > 0,
                Mensagem = item.Mensagem,
                MarcadoComoImproprio = item.MarcadoComoImproprio,
                NomeUsuario = item.UsuarioAppNome ?? item.UsuarioCmsNome,
                PostadoPorModerador = !string.IsNullOrEmpty(item.UsuarioCmsNome)
            };
        }

        public ComentariosService(Contexto contexto, UsuarioContexto usuarioContexto)
        {
            this.db = contexto;
            this.usuarioContexto = usuarioContexto;
        }

        public IEnumerable<ComentarioDto> RetornarComentarios(int? artigoId)
        {
            return db.Comentarios
                .Include(item => item.Artigo)
                .Where(item => !artigoId.HasValue || item.ArtigoId.Equals(artigoId.Value))
                .OrderByDescending(item => item.DataCriacao)
                .ToList()
                .Select(item => ConvertToDto(item));
        }   

        public int RetornarTotalComentarios(bool? denunciados)
        {
            return db.Comentarios.Count(item => !denunciados.HasValue
                || (denunciados.Value && item.Denuncias.Count() > 0)
                || (!denunciados.Value && item.Denuncias.Count() == 0));
        }

        public async Task<Comentario> RetornarComentario(int id)
        {
            Comentario comentario = await db.Comentarios.FindAsync(id);
            return comentario;
        }

        public async Task<ComentarioDto> SalvarComentario(ComentarioPostDto comentarioDto)
        {
            var comentario = new Comentario
            {
                DataCriacao = DateTime.Now,
                ArtigoId = comentarioDto.ArtigoId,
                Mensagem = comentarioDto.Mensagem,
            };

            if (usuarioContexto.UsuarioCms)
            {
                comentario.UsuarioCmdId = usuarioContexto.NomeDeUsuario;
                comentario.UsuarioCmsNome = usuarioContexto.NomeDePerfil;
            }
            else
            {
                comentario.UsuarioAppId = usuarioContexto.NomeDeUsuario;
                comentario.UsuarioAppNome = usuarioContexto.NomeDePerfil;
            }

            db.Comentarios.Add(comentario);

            await db.SaveChangesAsync();

            return ConvertToDto(comentario);
        }
        
    }
}