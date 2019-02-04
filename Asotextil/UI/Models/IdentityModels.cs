using System;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.ComponentModel.DataAnnotations;

namespace UI.Models
{
    // Para agregar datos de perfil del usuario, agregue más propiedades a su clase ApplicationUser. Visite https://go.microsoft.com/fwlink/?LinkID=317594 para obtener más información.
    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Tenga en cuenta que el valor de authenticationType debe coincidir con el definido en CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Agregar aquí notificaciones personalizadas de usuario
            return userIdentity;
        }
        [Required]
        [StringLength(11, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2}.", MinimumLength = 9)]
        public String Cedula { get; set; }

        [Required]
        [StringLength(40, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2}.", MinimumLength = 2)]
        public String Nombre { get; set; }

        [Required]
        [Display(Name = "Primer Apellido")]
        [StringLength(40, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2}.", MinimumLength = 2)]
        public String Primer_Apellido { get; set; }

        [Required]
        [Display(Name = "Segundo Apellido")]
        [StringLength(40, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2}.", MinimumLength = 2)]
        public String Segundo_Apellido { get; set; }

        [Required]
        [Display(Name = "Fecha Nacimiento")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [UIHint("DateTimePicker")]
        public DateTime FechaNacimiento { get; set; }

        [Required]
        [Display(Name = "Fecha Ingreso")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [UIHint("DateTimePicker")]
        public DateTime FechaIngreso { get; set; }

        [Required]
        [StringLength(40, ErrorMessage = "El número de caracteres de {0} debe ser al menos {2}.", MinimumLength = 2)]
        public String Puesto { get; set; }


        public Double? Salario { get; set; }

        [Display(Name = "Fecha Salida")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:MM/dd/yyyy}", ApplyFormatInEditMode = true)]
        [UIHint("DateTimePicker")]
        public DateTime? FechaSalida { get; set; }

        public Boolean Estado { get; set; }
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
    }
}