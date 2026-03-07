namespace CentroCapacitacionEmergencias.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateCursos : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Cursoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Titulo = c.String(nullable: false),
                        CodigoCurso = c.String(nullable: false),
                        HorasPracticas = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.UsuarioCursoes",
                c => new
                    {
                        Usuario_Id = c.Int(nullable: false),
                        Curso_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Usuario_Id, t.Curso_Id })
                .ForeignKey("dbo.Usuarios", t => t.Usuario_Id, cascadeDelete: true)
                .ForeignKey("dbo.Cursoes", t => t.Curso_Id, cascadeDelete: true)
                .Index(t => t.Usuario_Id)
                .Index(t => t.Curso_Id);
            
            AddColumn("dbo.Cohortes", "Curso_Id", c => c.Int());
            CreateIndex("dbo.Cohortes", "Curso_Id");
            AddForeignKey("dbo.Cohortes", "Curso_Id", "dbo.Cursoes", "Id");
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.UsuarioCursoes", "Curso_Id", "dbo.Cursoes");
            DropForeignKey("dbo.UsuarioCursoes", "Usuario_Id", "dbo.Usuarios");
            DropForeignKey("dbo.Cohortes", "Curso_Id", "dbo.Cursoes");
            DropIndex("dbo.UsuarioCursoes", new[] { "Curso_Id" });
            DropIndex("dbo.UsuarioCursoes", new[] { "Usuario_Id" });
            DropIndex("dbo.Cohortes", new[] { "Curso_Id" });
            DropColumn("dbo.Cohortes", "Curso_Id");
            DropTable("dbo.UsuarioCursoes");
            DropTable("dbo.Cursoes");
        }
    }
}
