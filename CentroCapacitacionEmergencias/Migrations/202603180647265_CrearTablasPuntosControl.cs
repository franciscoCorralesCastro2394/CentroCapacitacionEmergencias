namespace CentroCapacitacionEmergencias.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CrearTablasPuntosControl : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.PuntoControlDestrezas",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        IdPunto = c.Int(nullable: false),
                        IdDestreza = c.Int(nullable: false),
                        Destreza_Id = c.Int(),
                        Punto_Id = c.Int(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Destrezas", t => t.Destreza_Id)
                .ForeignKey("dbo.PuntoControls", t => t.Punto_Id)
                .Index(t => t.Destreza_Id)
                .Index(t => t.Punto_Id);
            
            CreateTable(
                "dbo.PuntoControls",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Descripcion = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            AddColumn("dbo.EvaluacionDestrezas", "TieneFallaCritica", c => c.Boolean(nullable: false));
            AddColumn("dbo.EvaluacionDestrezas", "AprobadoPorInstructor", c => c.Boolean(nullable: false));
            AddColumn("dbo.EvaluacionDestrezas", "InstructorNombre", c => c.String());
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.PuntoControlDestrezas", "Punto_Id", "dbo.PuntoControls");
            DropForeignKey("dbo.PuntoControlDestrezas", "Destreza_Id", "dbo.Destrezas");
            DropIndex("dbo.PuntoControlDestrezas", new[] { "Punto_Id" });
            DropIndex("dbo.PuntoControlDestrezas", new[] { "Destreza_Id" });
            DropColumn("dbo.EvaluacionDestrezas", "InstructorNombre");
            DropColumn("dbo.EvaluacionDestrezas", "AprobadoPorInstructor");
            DropColumn("dbo.EvaluacionDestrezas", "TieneFallaCritica");
            DropTable("dbo.PuntoControls");
            DropTable("dbo.PuntoControlDestrezas");
        }
    }
}
