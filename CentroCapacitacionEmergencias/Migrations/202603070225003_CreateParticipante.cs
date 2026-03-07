namespace CentroCapacitacionEmergencias.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class CreateParticipante : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Participantes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        TipoIdentificacion = c.String(nullable: false),
                        Identificacion = c.String(nullable: false),
                        NombreCompleto = c.String(nullable: false),
                        FechaNacimiento = c.DateTime(nullable: false),
                        Provincia = c.String(nullable: false),
                        Canton = c.String(nullable: false),
                        Distrito = c.String(nullable: false),
                        DetalleDireccion = c.String(nullable: false),
                        EstadoCivil = c.String(nullable: false),
                        Correo = c.String(nullable: false),
                        Telefono = c.String(),
                        DireccionResidencia = c.String(),
                        ContactoEmergencia = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Participantes");
        }
    }
}
