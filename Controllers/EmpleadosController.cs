using ApiEmpresa.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiEmpresa.Controllers
{
    [ApiController]
    [Route("api/Empleados")]
    public class EmpleadosController : ControllerBase
    {
        private readonly ApplicationDbContext context;

        public EmpleadosController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Empleado>>>VerEmpleados()
        {
            var empleados = await context.ObtenerEmpleados();
            return empleados;
        }

        [HttpPost]
        public async Task<ActionResult> CrearEmpleado([FromBody] Empleado empleado)
        {
            var existeEmpleado = await context.Empleados.AnyAsync(x => x.ClaveDeEmpleado == empleado.ClaveDeEmpleado);
            if (existeEmpleado )
            {
                return NotFound("La clave ya existe intenta otra");
            }

            var result = await context.CrearEmpleado(empleado);

            if (result > 0)
            {
                await context.SaveChangesAsync();
                return Ok("Se creo con exito");
            }
            else
            {
                return BadRequest("El tipo de dato no es correcto, Asegurate de poner las fechas en el formato yyyy/mm/dd");
            }

        }



        [HttpPut]
        public async Task<ActionResult> ModificarEmpleado(string id, [FromBody] Empleado empleado)
        {
            if (id != empleado.ClaveDeEmpleado)
            {
                return NotFound("No se encontro el empleado");
            }

            var result = await context.ModificarEmpleado(empleado);

            if (result > 0)
            {
                await context.SaveChangesAsync();
                return Ok("Se modifico con exito");
            }
            else
            {
                return BadRequest("El tipo de dato no es correcto, Asegurate de poner las fechas en el formato yyyy/mm/dd");
            }
        }
        [HttpDelete("{id}")]
        public async Task<IActionResult> BorrarEmpleado(string id)
        {
            var existeEmpleado = await context.Empleados.AnyAsync(x => x.ClaveDeEmpleado == id);
            if (!existeEmpleado)
            {
                return NotFound("El empleado no existe");
            }

            var result = await context.BorrarEmpleado(id);

            if (result > 0)
            {
                return Ok("Se elimino de manera adecuada");
            }

            return Ok("Se elimino de manera adecuada");

        }
    }
}
