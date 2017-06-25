namespace Conetados.Webapi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initial : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Alternativas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descricao = c.String(),
                        EnqueteId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Conteudos", t => t.EnqueteId, cascadeDelete: true)
                .Index(t => t.EnqueteId);
            
            CreateTable(
                "dbo.Conteudos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Ordem = c.Int(nullable: false),
                        TipoConteudo = c.Int(nullable: false),
                        ArtigoId = c.Int(nullable: false),
                        TextoParagrafo = c.String(),
                        Importancia = c.Int(),
                        TextoTitulo = c.String(),
                        Descricao = c.String(),
                        UploadId = c.Int(),
                        DataEncerramentoEnquete = c.DateTime(),
                        YoutubeVideoId = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Artigos", t => t.ArtigoId, cascadeDelete: true)
                .ForeignKey("dbo.Uploads", t => t.UploadId)
                .Index(t => t.ArtigoId)
                .Index(t => t.UploadId);
            
            CreateTable(
                "dbo.Artigos",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Titulo = c.String(),
                        Thumbnail = c.Binary(),
                        UsuarioCmsId = c.String(),
                        UsuarioCmsNome = c.String(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                        Ativo = c.Boolean(nullable: false),
                        DataPublicacao = c.DateTime(),
                        LayoutId = c.Int(),
                        Discriminator = c.String(nullable: false, maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Artigos", t => t.LayoutId)
                .Index(t => t.LayoutId);
            
            CreateTable(
                "dbo.Comentarios",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ArtigoId = c.Int(nullable: false),
                        Mensagem = c.String(),
                        MarcadoComoImproprio = c.Boolean(nullable: false),
                        UsuarioAppId = c.String(),
                        UsuarioCmdId = c.String(),
                        UsuarioCmsNome = c.String(),
                        UsuarioAppNome = c.String(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Artigos", t => t.ArtigoId, cascadeDelete: true)
                .Index(t => t.ArtigoId);
            
            CreateTable(
                "dbo.Denuncias",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ComentarioId = c.Int(nullable: false),
                        UsuarioAppId = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Comentarios", t => t.ComentarioId, cascadeDelete: true)
                .Index(t => t.ComentarioId);
            
            CreateTable(
                "dbo.Curtidas",
                c => new
                    {
                        ArtigoId = c.Int(nullable: false),
                        UsuarioAppId = c.String(nullable: false, maxLength: 128),
                        UsuarioAppNome = c.String(),
                    })
                .PrimaryKey(t => new { t.ArtigoId, t.UsuarioAppId })
                .ForeignKey("dbo.Artigos", t => t.ArtigoId, cascadeDelete: true)
                .Index(t => t.ArtigoId);
            
            CreateTable(
                "dbo.Tags",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Nome = c.String(),
                        Descrição = c.String(),
                        Editoria = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.ImagemGalerias",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Ordem = c.Int(nullable: false),
                        Descricao = c.String(),
                        UploadId = c.Int(),
                        GaleriaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Conteudos", t => t.GaleriaId, cascadeDelete: true)
                .ForeignKey("dbo.Uploads", t => t.UploadId)
                .Index(t => t.UploadId)
                .Index(t => t.GaleriaId);
            
            CreateTable(
                "dbo.Uploads",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        NomeArquivo = c.String(),
                        Blob = c.Binary(),
                        MediaType = c.String(),
                        Width = c.Int(),
                        Height = c.Int(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AlternativaRespostas",
                c => new
                    {
                        AlternativaId = c.Int(nullable: false),
                        UsuarioAppId = c.String(nullable: false, maxLength: 128),
                        UsuarioAppNome = c.String(),
                        DataCriacao = c.DateTime(nullable: false),
                        DataAlteracao = c.DateTime(),
                    })
                .PrimaryKey(t => new { t.AlternativaId, t.UsuarioAppId })
                .ForeignKey("dbo.Alternativas", t => t.AlternativaId, cascadeDelete: true)
                .Index(t => t.AlternativaId);
            
            CreateTable(
                "dbo.ArtigosTags",
                c => new
                    {
                        ArtigoIdId = c.Int(nullable: false),
                        TagId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ArtigoIdId, t.TagId })
                .ForeignKey("dbo.Artigos", t => t.ArtigoIdId, cascadeDelete: true)
                .ForeignKey("dbo.Tags", t => t.TagId, cascadeDelete: true)
                .Index(t => t.ArtigoIdId)
                .Index(t => t.TagId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AlternativaRespostas", "AlternativaId", "dbo.Alternativas");
            DropForeignKey("dbo.ImagemGalerias", "UploadId", "dbo.Uploads");
            DropForeignKey("dbo.Conteudos", "UploadId", "dbo.Uploads");
            DropForeignKey("dbo.ImagemGalerias", "GaleriaId", "dbo.Conteudos");
            DropForeignKey("dbo.ArtigosTags", "TagId", "dbo.Tags");
            DropForeignKey("dbo.ArtigosTags", "ArtigoIdId", "dbo.Artigos");
            DropForeignKey("dbo.Artigos", "LayoutId", "dbo.Artigos");
            DropForeignKey("dbo.Curtidas", "ArtigoId", "dbo.Artigos");
            DropForeignKey("dbo.Denuncias", "ComentarioId", "dbo.Comentarios");
            DropForeignKey("dbo.Comentarios", "ArtigoId", "dbo.Artigos");
            DropForeignKey("dbo.Conteudos", "ArtigoId", "dbo.Artigos");
            DropForeignKey("dbo.Alternativas", "EnqueteId", "dbo.Conteudos");
            DropIndex("dbo.ArtigosTags", new[] { "TagId" });
            DropIndex("dbo.ArtigosTags", new[] { "ArtigoIdId" });
            DropIndex("dbo.AlternativaRespostas", new[] { "AlternativaId" });
            DropIndex("dbo.ImagemGalerias", new[] { "GaleriaId" });
            DropIndex("dbo.ImagemGalerias", new[] { "UploadId" });
            DropIndex("dbo.Curtidas", new[] { "ArtigoId" });
            DropIndex("dbo.Denuncias", new[] { "ComentarioId" });
            DropIndex("dbo.Comentarios", new[] { "ArtigoId" });
            DropIndex("dbo.Artigos", new[] { "LayoutId" });
            DropIndex("dbo.Conteudos", new[] { "UploadId" });
            DropIndex("dbo.Conteudos", new[] { "ArtigoId" });
            DropIndex("dbo.Alternativas", new[] { "EnqueteId" });
            DropTable("dbo.ArtigosTags");
            DropTable("dbo.AlternativaRespostas");
            DropTable("dbo.Uploads");
            DropTable("dbo.ImagemGalerias");
            DropTable("dbo.Tags");
            DropTable("dbo.Curtidas");
            DropTable("dbo.Denuncias");
            DropTable("dbo.Comentarios");
            DropTable("dbo.Artigos");
            DropTable("dbo.Conteudos");
            DropTable("dbo.Alternativas");
        }
    }
}
