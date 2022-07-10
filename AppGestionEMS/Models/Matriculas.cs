using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace AppGestionEMS.Models
{
    public class Matriculas
    {
        [Key]
        [Column(Order = 1)]
        public string AlumnoId { get; set; } // Clave Foranea
        public virtual ApplicationUser Alumno { get; set; } //Navigation Property
        [Key]
        [Column(Order = 2)]
        public int CursoId { get; set; } // Clave Foranea
        public virtual Cursos Curso { get; set; } //Navigation Property
        [Key]
        [Column(Order = 3)]
        public int GrupoId { get; set; } // Clave Foranea
        public virtual Grupo Grupo { get; set; } //Navigation Property
    }
}