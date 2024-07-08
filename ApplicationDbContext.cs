using ApiEmpresa.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace ApiEmpresa
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Empleado> Empleados { get; set; }
        public DbSet<Departamento> Departamentos { get; set; }
        public DbSet<Sueldo> Sueldos { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Empleado>().HasNoKey();
            modelBuilder.Entity<Departamento>().HasNoKey();
            modelBuilder.Entity<Sueldo>().HasNoKey();
        }
        public async Task<List<Empleado>> ObtenerEmpleados()
        {
            return await Empleados.FromSqlRaw("EXEC dbo.Ver_Empleados").ToListAsync();
        }
        public async Task<List<Departamento>> ObtenerDepartamentos()
        {
            return await Departamentos.FromSqlRaw("EXEC dbo.Ver_Departamentos").ToListAsync();
        }
        public async Task<List<Sueldo>> ObtenerSueldos()
        {
            return await Sueldos.FromSqlRaw("EXEC dbo.Ver_Sueldos").ToListAsync();
        }
        public async Task<int> ModificarEmpleado(Empleado empleado)
        {
            var claveParam = new SqlParameter("@ClaveDeEmpleado", empleado.ClaveDeEmpleado);
            var nombreParam = new SqlParameter("@NombreEmpleado", empleado.NombreEmpleado);
            var fechaIngresoParam = new SqlParameter("@FechaIngreso", empleado.FechaIngreso);
            var fechaNacimientoParam = new SqlParameter("@FechaNacimiento", empleado.FechaNacimiento);
            var departamentoParam = new SqlParameter("@Departamento", empleado.Departamento);

            return await Database.ExecuteSqlRawAsync(
                "EXEC Modificar_Empleado @ClaveDeEmpleado, @NombreEmpleado, @FechaIngreso, @FechaNacimiento, @Departamento",
                claveParam, nombreParam, fechaIngresoParam, fechaNacimientoParam, departamentoParam);
        }
        public async Task<int> ModificarDepartamento(Departamento departamento)
        {
            var claveParam = new SqlParameter("@ClaveDepartamento", departamento.ClaveDepartamento);
            var nombreParam = new SqlParameter("@Descripcion", departamento.Descripcion);
           

            return await Database.ExecuteSqlRawAsync(
                "EXEC Modificar_Departamento @ClaveDepartamento, @Descripcion",
                claveParam, nombreParam);
        }
        public async Task<int> ModificarSueldo(Sueldo sueldo)
        {
            var sueldoParam = new SqlParameter("@SueldoEmpleado", sueldo.SueldoEmpleado);
            var FormaPagoParam = new SqlParameter("@FormaDePago", sueldo.FormaDePago);
            var empleadoParam = new SqlParameter("@Empleado", sueldo.Empleado);


            return await Database.ExecuteSqlRawAsync(
                "EXEC Modificar_Sueldo @SueldoEmpleado,@FormaDePago, @Empleado ",
                 sueldoParam, FormaPagoParam, empleadoParam);
        }

        public async Task<int> CrearEmpleado(Empleado empleado)
        {
            var claveParam = new SqlParameter("@ClaveDeEmpleado", empleado.ClaveDeEmpleado);
            var nombreParam = new SqlParameter("@NombreEmpleado", empleado.NombreEmpleado);
            var fechaIngresoParam = new SqlParameter("@FechaIngreso", empleado.FechaIngreso);
            var fechaNacimientoParam = new SqlParameter("@FechaNacimiento", empleado.FechaNacimiento);
            var departamentoParam = new SqlParameter("@Departamento", empleado.Departamento);

            return await Database.ExecuteSqlRawAsync(
                "EXEC Crear_Empleado @ClaveDeEmpleado, @NombreEmpleado, @FechaIngreso, @FechaNacimiento, @Departamento",
                claveParam, nombreParam, fechaIngresoParam, fechaNacimientoParam, departamentoParam);

        }

        public async Task<int> CrearDepartamento(Departamento departamento)
        {
            var claveParam = new SqlParameter("@ClaveDepartamento", departamento.ClaveDepartamento);
            var nombreParam = new SqlParameter("@Descripcion", departamento.Descripcion);
           

            return await Database.ExecuteSqlRawAsync(
                "EXEC Crear_Departamento @ClaveDepartamento, @Descripcion",
                claveParam, nombreParam);

        }
        public async Task<int> CrearSueldo(Sueldo sueldo)
        {
            var sueldoParam = new SqlParameter("@SueldoEmpleado", sueldo.SueldoEmpleado);
            var FormaPagoParam = new SqlParameter("@FormaDePago", sueldo.FormaDePago);
            var empleadoParam = new SqlParameter("@Empleado", sueldo.Empleado);


            return await Database.ExecuteSqlRawAsync(
                "EXEC Crear_Sueldo @SueldoEmpleado, @FormaDePago,@Empleado",
                sueldoParam, FormaPagoParam, empleadoParam);

        }

        public async Task<int> BorrarEmpleado(string claveDeEmpleado)
        {
            var claveParam = new SqlParameter("@ClaveDeEmpleado", claveDeEmpleado);
            return await Database.ExecuteSqlRawAsync("EXEC Eliminar_Empleado @ClaveDeEmpleado", claveParam);
        }
        public async Task<int> BorrarDepartamento(int claveDepartamento)
        {
            var claveParam = new SqlParameter("@ClaveDepartamento", claveDepartamento);
            return await Database.ExecuteSqlRawAsync("EXEC Eliminar_Departamento @ClaveDepartamento", claveParam);
        }
        public async Task<int> BorrarSueldo(string Empleado)
        {
            var claveParam = new SqlParameter("@Empleado", Empleado);
            return await Database.ExecuteSqlRawAsync("EXEC Eliminar_Sueldo @Empleado", claveParam);
        }

    }
}
