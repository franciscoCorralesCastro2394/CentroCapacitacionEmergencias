namespace CentroCapacitacionEmergencias.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CrearTablaDestrezasCursosEva : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.CursoDestrezas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        CursoId = c.Int(nullable: false),
                        DestrezaId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cursoes", t => t.CursoId, cascadeDelete: true)
                .ForeignKey("dbo.Destrezas", t => t.DestrezaId, cascadeDelete: true)
                .Index(t => t.CursoId)
                .Index(t => t.DestrezaId);
            
            CreateTable(
                "dbo.Destrezas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Titulo = c.String(),
                        Descripcion = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EvaluacionDestrezas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ParticipanteId = c.Int(nullable: false),
                        CursoId = c.Int(nullable: false),
                        DestrezaId = c.Int(nullable: false),
                        TiempoRespuesta = c.Int(nullable: false),
                        PuntajeFinal = c.Int(nullable: false),
                        FechaRegistro = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cursoes", t => t.CursoId, cascadeDelete: true)
                .ForeignKey("dbo.Destrezas", t => t.DestrezaId, cascadeDelete: true)
                .ForeignKey("dbo.Participantes", t => t.ParticipanteId, cascadeDelete: true)
                .Index(t => t.ParticipanteId)
                .Index(t => t.CursoId)
                .Index(t => t.DestrezaId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.EvaluacionDestrezas", "ParticipanteId", "dbo.Participantes");
            DropForeignKey("dbo.EvaluacionDestrezas", "DestrezaId", "dbo.Destrezas");
            DropForeignKey("dbo.EvaluacionDestrezas", "CursoId", "dbo.Cursoes");
            DropForeignKey("dbo.CursoDestrezas", "DestrezaId", "dbo.Destrezas");
            DropForeignKey("dbo.CursoDestrezas", "CursoId", "dbo.Cursoes");
            DropIndex("dbo.EvaluacionDestrezas", new[] { "DestrezaId" });
            DropIndex("dbo.EvaluacionDestrezas", new[] { "CursoId" });
            DropIndex("dbo.EvaluacionDestrezas", new[] { "ParticipanteId" });
            DropIndex("dbo.CursoDestrezas", new[] { "DestrezaId" });
            DropIndex("dbo.CursoDestrezas", new[] { "CursoId" });
            DropTable("dbo.EvaluacionDestrezas");
            DropTable("dbo.Destrezas");
            DropTable("dbo.CursoDestrezas");
        }
    }
}
