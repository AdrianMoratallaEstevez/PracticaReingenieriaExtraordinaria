namespace AppGestionEMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AñadeNombreCurso : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cursos", "Nombre", c => c.String(nullable: false));
        }
        
        public override void Down()
        {
            DropColumn("dbo.Cursos", "Nombre");
        }
    }
}
