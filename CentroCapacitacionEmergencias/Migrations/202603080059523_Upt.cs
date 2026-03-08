namespace CentroCapacitacionEmergencias.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Upt : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Participantes", "Estado", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Participantes", "Estado");
        }
    }
}
