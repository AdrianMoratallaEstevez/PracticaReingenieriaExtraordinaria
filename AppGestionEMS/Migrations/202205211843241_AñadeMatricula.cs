namespace AppGestionEMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AñadeMatricula : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Matriculas",
                c => new
                    {
                        AlumnoId = c.String(nullable: false, maxLength: 128),
                        CursoId = c.Int(nullable: false),
                        GrupoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.AlumnoId, t.CursoId, t.GrupoId })
                .ForeignKey("dbo.AspNetUsers", t => t.AlumnoId, cascadeDelete: true)
                .ForeignKey("dbo.Cursos", t => t.CursoId, cascadeDelete: true)
                .ForeignKey("dbo.Grupoes", t => t.GrupoId, cascadeDelete: true)
                .Index(t => t.AlumnoId)
                .Index(t => t.CursoId)
                .Index(t => t.GrupoId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.Matriculas", "GrupoId", "dbo.Grupoes");
            DropForeignKey("dbo.Matriculas", "CursoId", "dbo.Cursos");
            DropForeignKey("dbo.Matriculas", "AlumnoId", "dbo.AspNetUsers");
            DropIndex("dbo.Matriculas", new[] { "GrupoId" });
            DropIndex("dbo.Matriculas", new[] { "CursoId" });
            DropIndex("dbo.Matriculas", new[] { "AlumnoId" });
            DropTable("dbo.Matriculas");
        }
    }
}
