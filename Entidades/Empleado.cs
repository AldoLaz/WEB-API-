using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace ApiEmpresa.Entidades
{
    public class Empleado
    {
       
        public string ClaveDeEmpleado { get; set; }
        [Required]
        [StringLength(40)]
        public string NombreEmpleado { get; set;}
        [Required]
        public DateTime FechaIngreso { get; set;}
        [Required]
        public DateTime FechaNacimiento { get; set; }
        public int Departamento { get; set;}
    }
}
