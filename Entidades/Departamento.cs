using System.ComponentModel.DataAnnotations;

namespace ApiEmpresa.Entidades
{
    public class Departamento
    {
        [Required]
        public int ClaveDepartamento {  get; set; }
        [Required]
        [StringLength(50)]
        public string Descripcion { get; set; }
    }
}
