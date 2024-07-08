using ApiEmpresa.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiEmpresa.Controllers
{
    [ApiController]
    [Route("api/Sueldos")]
    public class SueldosController:ControllerBase
    {
        private readonly ApplicationDbContext context;

        public SueldosController(ApplicationDbContext context)
        {
            this.context = context;
        }

        [HttpGet]
        public async Task<ActionResult<List<Sueldo>>> VerSueldos()
        {
            var sueldos = await context.ObtenerSueldos();
            return sueldos;
        }

        [HttpPost]
        public async Task<ActionResult> CrearSueldo([FromBody] Sueldo sueldo)
        {
            //var existeSueldo = await context.Sueldos.AnyAsync(x => x.Empleado == sueldo.Empleado);
            //if (existeSueldo)
            //{
            //    return NotFound("La clave ya existe intenta otra");
            //}

            var result = await context.CrearSueldo(sueldo);

            if (result > 0)
            {
                await context.SaveChangesAsync();
                return Ok("Se creo con exito");
            }
            else
            {
                return BadRequest("El tipo de dato no es correcto");
            }

        }
        [HttpPut]
        public async Task<ActionResult> ModificarSueldos( [FromBody] Sueldo sueldo)
        {
           
            var result = await context.ModificarSueldo(sueldo);

            if (result > 0)
            {
                await context.SaveChangesAsync();
                return Ok("Se modifico con exito");
            }
            else
            {
                return BadRequest("El tipo de dato no es correcto");
            }
        }
        [HttpDelete("{id}")]
        public async Task<ActionResult> BorrarSueldo(string id)
        {
            var existePago = await context.Sueldos.AnyAsync(x => x.Empleado == id);
            if (!existePago)
            {
                return NotFound("El pago no existe");
            }

            var result = await context.BorrarSueldo(id);

            if (result > 0)
            {
                return Ok("Se elimino de manera adecuada");
            }

            return Ok("Se elimino de manera adecuada");

        }
    }
}
