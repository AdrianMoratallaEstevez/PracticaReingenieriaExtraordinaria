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

    }
}