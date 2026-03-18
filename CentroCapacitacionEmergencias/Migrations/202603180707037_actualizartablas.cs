namespace CentroCapacitacionEmergencias.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class actualizartablas : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.PuntoControlDestrezas", "Destreza_Id", "dbo.Destrezas");
            DropForeignKey("dbo.PuntoControlDestrezas", "Punto_Id", "dbo.PuntoControls");
            DropIndex("dbo.PuntoControlDestrezas", new[] { "Destreza_Id" });
            DropIndex("dbo.PuntoControlDestrezas", new[] { "Punto_Id" });
            DropColumn("dbo.PuntoControlDestrezas", "Destreza_Id");
            DropColumn("dbo.PuntoControlDestrezas", "Punto_Id");
        }
        
        public override void Down()
        {
            AddColumn("dbo.PuntoControlDestrezas", "Punto_Id", c => c.Int());
            AddColumn("dbo.PuntoControlDestrezas", "Destreza_Id", c => c.Int());
            CreateIndex("dbo.PuntoControlDestrezas", "Punto_Id");
            CreateIndex("dbo.PuntoControlDestrezas", "Destreza_Id");
            AddForeignKey("dbo.PuntoControlDestrezas", "Punto_Id", "dbo.PuntoControls", "Id");
            AddForeignKey("dbo.PuntoControlDestrezas", "Destreza_Id", "dbo.Destrezas", "Id");
        }
    }
}
