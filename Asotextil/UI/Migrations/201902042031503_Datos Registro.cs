namespace UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class DatosRegistro : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.AspNetUsers", "Cedula", c => c.String(nullable: false));
            AddColumn("dbo.AspNetUsers", "Nombre", c => c.String(nullable: false));
            AddColumn("dbo.AspNetUsers", "Primer_Apellido", c => c.String(nullable: false));
            AddColumn("dbo.AspNetUsers", "Segundo_Apellido", c => c.String(nullable: false));
            AddColumn("dbo.AspNetUsers", "FechaNacimiento", c => c.DateTime(nullable: false));
            AddColumn("dbo.AspNetUsers", "FechaIngreso", c => c.DateTime(nullable: false));
            AddColumn("dbo.AspNetUsers", "Puesto", c => c.String(nullable: false));
            AddColumn("dbo.AspNetUsers", "Salario", c => c.Double());
            AddColumn("dbo.AspNetUsers", "FechaSalida", c => c.DateTime());
            AddColumn("dbo.AspNetUsers", "Estado", c => c.Boolean(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.AspNetUsers", "Estado");
            DropColumn("dbo.AspNetUsers", "FechaSalida");
            DropColumn("dbo.AspNetUsers", "Salario");
            DropColumn("dbo.AspNetUsers", "Puesto");
            DropColumn("dbo.AspNetUsers", "FechaIngreso");
            DropColumn("dbo.AspNetUsers", "FechaNacimiento");
            DropColumn("dbo.AspNetUsers", "Segundo_Apellido");
            DropColumn("dbo.AspNetUsers", "Primer_Apellido");
            DropColumn("dbo.AspNetUsers", "Nombre");
            DropColumn("dbo.AspNetUsers", "Cedula");
        }
    }
}
