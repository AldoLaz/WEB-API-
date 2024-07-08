using ApiEmpresa.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ApiEmpresa.Controllers
{
    [ApiController]
    [Route("api/departamentos")]
    public class DepartamentosController:ControllerBase
    {
        private readonly ApplicationDbContext context;

        public DepartamentosController(ApplicationDbContext context)
        {
            this.context = context;
        }


        [HttpGet]
        public async Task<ActionResult<List<Departamento>>> VerDepartamentos()
        {
            var departamentos = await context.ObtenerDepartamentos();
            return departamentos;
        }

        [HttpPost]
        public async Task<ActionResult> CrearDepartamento([FromBody] Departamento departamento)
        {
            var existeDepartamento = await context.Departamentos.AnyAsync(x => x.ClaveDepartamento == departamento.ClaveDepartamento);
            if (existeDepartamento)
            {
                return NotFound("La clave ya existe intenta otra");
            }

            var result = await context.CrearDepartamento(departamento);

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
        public async Task<ActionResult> ModificarDepartamento(int id, [FromBody] Departamento departamento)
        {
            if (id == departamento.ClaveDepartamento)
            {
                return NotFound("No se encontro el departamento");
            }

            var result = await context.ModificarDepartamento(departamento);

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
        public async Task<IActionResult> BorrarEmpleado(int id)
        {
            var existeEmpleado = await context.Departamentos.AnyAsync(x => x.ClaveDepartamento == id);
            if (!existeEmpleado)
            {
                return NotFound("El Departamento no existe");
            }

            var result = await context.BorrarDepartamento(id);

            if (result > 0)
            {
                return Ok("Se elimino de manera adecuada");
            }

            return Ok("Se elimino de manera adecuada");

        }
    }
}
