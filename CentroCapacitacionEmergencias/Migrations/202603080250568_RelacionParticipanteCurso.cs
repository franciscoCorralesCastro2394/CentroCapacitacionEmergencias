namespace CentroCapacitacionEmergencias.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RelacionParticipanteCurso : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.UsuarioCursoes", "Usuario_Id", "dbo.Usuarios");
            DropForeignKey("dbo.UsuarioCursoes", "Curso_Id", "dbo.Cursoes");
            DropIndex("dbo.UsuarioCursoes", new[] { "Usuario_Id" });
            DropIndex("dbo.UsuarioCursoes", new[] { "Curso_Id" });
            CreateTable(
                "dbo.ParticipanteCursoes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ParticipanteId = c.Int(nullable: false),
                        CursoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cursoes", t => t.CursoId, cascadeDelete: true)
                .ForeignKey("dbo.Participantes", t => t.ParticipanteId, cascadeDelete: true)
                .Index(t => t.ParticipanteId)
                .Index(t => t.CursoId);
            
            AddColumn("dbo.Cursoes", "Participante_Id", c => c.Int());
            AddColumn("dbo.Usuarios", "Curso_Id", c => c.Int());
            CreateIndex("dbo.Cursoes", "Participante_Id");
            CreateIndex("dbo.Usuarios", "Curso_Id");
            AddForeignKey("dbo.Usuarios", "Curso_Id", "dbo.Cursoes", "Id");
            AddForeignKey("dbo.Cursoes", "Participante_Id", "dbo.Participantes", "Id");
            DropTable("dbo.UsuarioCursoes");
        }
        
        public override void Down()
        {
            CreateTable(
                "dbo.UsuarioCursoes",
                c => new
                    {
                        Usuario_Id = c.Int(nullable: false),
                        Curso_Id = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.Usuario_Id, t.Curso_Id });
            
            DropForeignKey("dbo.ParticipanteCursoes", "ParticipanteId", "dbo.Participantes");
            DropForeignKey("dbo.Cursoes", "Participante_Id", "dbo.Participantes");
            DropForeignKey("dbo.ParticipanteCursoes", "CursoId", "dbo.Cursoes");
            DropForeignKey("dbo.Usuarios", "Curso_Id", "dbo.Cursoes");
            DropIndex("dbo.ParticipanteCursoes", new[] { "CursoId" });
            DropIndex("dbo.ParticipanteCursoes", new[] { "ParticipanteId" });
            DropIndex("dbo.Usuarios", new[] { "Curso_Id" });
            DropIndex("dbo.Cursoes", new[] { "Participante_Id" });
            DropColumn("dbo.Usuarios", "Curso_Id");
            DropColumn("dbo.Cursoes", "Participante_Id");
            DropTable("dbo.ParticipanteCursoes");
            CreateIndex("dbo.UsuarioCursoes", "Curso_Id");
            CreateIndex("dbo.UsuarioCursoes", "Usuario_Id");
            AddForeignKey("dbo.UsuarioCursoes", "Curso_Id", "dbo.Cursoes", "Id", cascadeDelete: true);
            AddForeignKey("dbo.UsuarioCursoes", "Usuario_Id", "dbo.Usuarios", "Id", cascadeDelete: true);
        }
    }
}
