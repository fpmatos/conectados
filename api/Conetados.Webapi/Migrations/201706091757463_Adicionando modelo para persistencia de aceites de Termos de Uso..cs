namespace Conetados.Webapi.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AdicionandomodeloparapersistenciadeaceitesdeTermosdeUso : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AceiteTermoUsoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        MatriculaUsuario = c.String(),
                        NomeUsuario = c.String(),
                        DataAceite = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.AceiteTermoUsoes");
        }
    }
}
