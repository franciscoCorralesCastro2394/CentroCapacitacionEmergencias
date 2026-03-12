namespace CentroCapacitacionEmergencias.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class RelacionCohortePart : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.ParticipanteCursoes", "CohorteID", c => c.Int(nullable: false));
            CreateIndex("dbo.ParticipanteCursoes", "CohorteID");
            AddForeignKey("dbo.ParticipanteCursoes", "CohorteID", "dbo.Cohortes", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.ParticipanteCursoes", "CohorteID", "dbo.Cohortes");
            DropIndex("dbo.ParticipanteCursoes", new[] { "CohorteID" });
            DropColumn("dbo.ParticipanteCursoes", "CohorteID");
        }
    }
}
