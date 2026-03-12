namespace CentroCapacitacionEmergencias.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RelacionPartiCohorte : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.ParticipanteCohortes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        ParticipanteId = c.Int(nullable: false),
                        CohorteID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.Cohortes", t => t.CohorteID, cascadeDelete: true)
                .ForeignKey("dbo.Participantes", t => t.ParticipanteId, cascadeDelete: true)
                .Index(t => t.ParticipanteId)
                .Index(t => t.CohorteID);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ParticipanteCohortes", "ParticipanteId", "dbo.Participantes");
            DropForeignKey("dbo.ParticipanteCohortes", "CohorteID", "dbo.Cohortes");
            DropIndex("dbo.ParticipanteCohortes", new[] { "CohorteID" });
            DropIndex("dbo.ParticipanteCohortes", new[] { "ParticipanteId" });
            DropTable("dbo.ParticipanteCohortes");
        }
    }
}
