namespace AppGestionEMS.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class AñadeAsignacionDocente : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AsignacionDocentes",
                c => new
                    {
                        ProfesorId = c.String(nullable: false, maxLength: 128),
                        CursoId = c.Int(nullable: false),
                        GrupoId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.ProfesorId, t.CursoId, t.GrupoId })
                .ForeignKey("dbo.Cursos", t => t.CursoId, cascadeDelete: true)
                .ForeignKey("dbo.Grupoes", t => t.GrupoId, cascadeDelete: true)
                .ForeignKey("dbo.AspNetUsers", t => t.ProfesorId, cascadeDelete: true)
                .Index(t => t.ProfesorId)
                .Index(t => t.CursoId)
                .Index(t => t.GrupoId);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AsignacionDocentes", "ProfesorId", "dbo.AspNetUsers");
            DropForeignKey("dbo.AsignacionDocentes", "GrupoId", "dbo.Grupoes");
            DropForeignKey("dbo.AsignacionDocentes", "CursoId", "dbo.Cursos");
            DropIndex("dbo.AsignacionDocentes", new[] { "GrupoId" });
            DropIndex("dbo.AsignacionDocentes", new[] { "CursoId" });
            DropIndex("dbo.AsignacionDocentes", new[] { "ProfesorId" });
            DropTable("dbo.AsignacionDocentes");
        }
    }
}
