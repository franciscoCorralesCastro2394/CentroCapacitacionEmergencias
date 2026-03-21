namespace CentroCapacitacionEmergencias.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class actualizacionCursos : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cursoes", "maxInstructores", c => c.Int());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Cursoes", "maxInstructores");
        }
    }
}
