using Conetados.Webapi.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Web;

namespace Conetados.Webapi
{
    public class Contexto : DbContext
    {
        // You can add custom code to this file. Changes will not be overwritten.
        // 
        // If you want Entity Framework to drop and regenerate your database
        // automatically whenever you change your model schema, please use data migrations.
        // For more information refer to the documentation:
        // http://msdn.microsoft.com/en-us/data/jj591621.aspx

        public Contexto() : base("name=Contexto")
        {
            Configuration.LazyLoadingEnabled = false;
            Database.Log = m => Debug.WriteLine(m);
        }

        public DbSet<Alternativa> Alternativas { get; set; }
        public DbSet<AlternativaResposta> AlternativasRespostas { get; set; }
        public DbSet<ArtigoBase> ArtigosBase { get; set; }
        public DbSet<Artigo> Artigos { get; set; }
        public DbSet<Comentario> Comentarios { get; set; }
        public DbSet<Conteudo> Conteudos { get; set; }
        public DbSet<Curtida> Curtidas { get; set; }
        public DbSet<ImagemGaleria> ImagensGaleria { get; set; }
        public DbSet<Layout> Layouts { get; set; }
        public DbSet<Tag> Tags { get; set; }
        public DbSet<Upload> Uploads { get; set; }
        public DbSet<Denuncia> Denuncias { get; set; }
        public DbSet<AceiteTermoUso> TermoUsoAceites { get; set; }


        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Artigo>()
                    .HasMany(s => s.Tags)
                    .WithMany(c => c.Artigos)
                    .Map(cs =>
                    {
                        cs.MapLeftKey("ArtigoIdId");
                        cs.MapRightKey("TagId");
                        cs.ToTable("ArtigosTags");
                    });
        }
    }
}
