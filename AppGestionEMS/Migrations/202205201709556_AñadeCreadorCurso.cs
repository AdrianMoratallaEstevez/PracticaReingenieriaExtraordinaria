namespace AppGestionEMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AñadeCreadorCurso : DbMigration
    {
        public override void Up()
        {
            AddColumn("dbo.Cursos", "Creador_Id", c => c.String(nullable: false, maxLength: 128));
            CreateIndex("dbo.Cursos", "Creador_Id");
            AddForeignKey("dbo.Cursos", "Creador_Id", "dbo.AspNetUsers", "Id", cascadeDelete: true);
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Cursos", "Creador_Id", "dbo.AspNetUsers");
            DropIndex("dbo.Cursos", new[] { "Creador_Id" });
            DropColumn("dbo.Cursos", "Creador_Id");
        }
    }
}
