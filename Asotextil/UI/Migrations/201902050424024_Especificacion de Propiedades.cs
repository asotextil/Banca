namespace UI.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class EspecificaciondePropiedades : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.AspNetUsers", "Cedula", c => c.String(nullable: false, maxLength: 11));
            AlterColumn("dbo.AspNetUsers", "Nombre", c => c.String(nullable: false, maxLength: 40));
            AlterColumn("dbo.AspNetUsers", "Primer_Apellido", c => c.String(nullable: false, maxLength: 40));
            AlterColumn("dbo.AspNetUsers", "Segundo_Apellido", c => c.String(nullable: false, maxLength: 40));
            AlterColumn("dbo.AspNetUsers", "Puesto", c => c.String(nullable: false, maxLength: 40));
        }
        
        public override void Down()
        {
            AlterColumn("dbo.AspNetUsers", "Puesto", c => c.String(nullable: false));
            AlterColumn("dbo.AspNetUsers", "Segundo_Apellido", c => c.String(nullable: false));
            AlterColumn("dbo.AspNetUsers", "Primer_Apellido", c => c.String(nullable: false));
            AlterColumn("dbo.AspNetUsers", "Nombre", c => c.String(nullable: false));
            AlterColumn("dbo.AspNetUsers", "Cedula", c => c.String(nullable: false));
        }
    }
}
