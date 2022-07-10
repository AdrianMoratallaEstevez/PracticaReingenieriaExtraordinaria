using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace AppGestionEMS.Models
{
    public class Cursos
    {
        // Clave primaria para la Base de datos
        public int Id { get; set; }

        [Required]
        public string Nombre { get; set; }
        // Resto de atributos definidos por el 
        public ApplicationUser Creador { get; set; }
        /*
        public int GrupoId { get; set; } // Clave Foranea
        public virtual GrupoClases Grupo { get; set; } //Navigation Property
        */
    }
}