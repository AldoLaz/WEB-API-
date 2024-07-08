using System.Data.SqlTypes;

namespace ApiEmpresa.Entidades
{
    public class Sueldo
    {
        public decimal SueldoEmpleado { get; set; }
        public string FormaDePago { get; set; }
        public string Empleado {  get; set; }
    }
}
