using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace AppGestionEMS.Models
{
    // Para agregar datos de perfil del usuario, agregue más propiedades a su clase ApplicationUser. Visite https://go.microsoft.com/fwlink/?LinkID=317594 para obtener más información.
    public class ApplicationUser : IdentityUser
    {
     
        public string Nombre
        {
            get;
            set;
        }
        public string Apellido
        {
            get;
            set;
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Tenga en cuenta que el valor de authenticationType debe coincidir con el definido en CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Agregar aquí notificaciones personalizadas de usuario
            return userIdentity;
        }
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public System.Data.Entity.DbSet<AppGestionEMS.Models.Cursos> Cursos { get; set; }

        public System.Data.Entity.DbSet<AppGestionEMS.Models.Grupo> Grupoes { get; set; }

        public System.Data.Entity.DbSet<AppGestionEMS.Models.AsignacionDocente> AsignacionDocentes { get; set; }

        public System.Data.Entity.DbSet<AppGestionEMS.Models.Matriculas> Matriculas { get; set; }

        public System.Data.Entity.DbSet<AppGestionEMS.Models.Evaluaciones> Evaluaciones { get; set; }
    }
}