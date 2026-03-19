namespace CentroCapacitacionEmergencias.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class actualizacionEvaluacion : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.EvaluacionDestrezas", "InstructorId", c => c.Int(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.EvaluacionDestrezas", "InstructorId");
        }
    }
}
