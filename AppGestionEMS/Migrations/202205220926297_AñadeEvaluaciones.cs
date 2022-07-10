namespace AppGestionEMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AñadeEvaluaciones : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Evaluaciones",
                c => new
                    {
                        AlumnoId = c.String(nullable: false, maxLength: 128),
                        CursoId = c.Int(nullable: false),
                        nota = c.Single(nullable: false),
                    })
                .PrimaryKey(t => new { t.AlumnoId, t.CursoId })
                .ForeignKey("dbo.AspNetUsers", t => t.AlumnoId, cascadeDelete: true)
                .ForeignKey("dbo.Cursos", t => t.CursoId, cascadeDelete: true)
                .Index(t => t.AlumnoId)
                .Index(t => t.CursoId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Evaluaciones", "CursoId", "dbo.Cursos");
            DropForeignKey("dbo.Evaluaciones", "AlumnoId", "dbo.AspNetUsers");
            DropIndex("dbo.Evaluaciones", new[] { "CursoId" });
            DropIndex("dbo.Evaluaciones", new[] { "AlumnoId" });
            DropTable("dbo.Evaluaciones");
        }
    }
}
