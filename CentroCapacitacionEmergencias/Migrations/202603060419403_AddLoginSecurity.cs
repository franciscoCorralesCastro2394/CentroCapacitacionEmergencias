namespace CentroCapacitacionEmergencias.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AddLoginSecurity : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Usuarios", "IntentosFallidos", c => c.Int(nullable: false));
            AddColumn("dbo.Usuarios", "BloqueadoHasta", c => c.DateTime());
        }
        
        public override void Down()
        {
            DropColumn("dbo.Usuarios", "BloqueadoHasta");
            DropColumn("dbo.Usuarios", "IntentosFallidos");
        }
    }
}
