namespace CentroCapacitacionEmergencias.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class actualizacionCohortesCursos : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cohortes", "Archivado", c => c.Boolean());
            AddColumn("dbo.Cursoes", "Archivado", c => c.Boolean());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Cursoes", "Archivado");
            DropColumn("dbo.Cohortes", "Archivado");
        }
    }
}
