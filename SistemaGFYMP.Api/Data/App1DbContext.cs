using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SistemaGFYMP.Modelos;

public class App1DbContext : DbContext
{
    public App1DbContext(DbContextOptions<App1DbContext> options)
        : base(options)
    {
    }

    public DbSet<SistemaGFYMP.Modelos.Conductor> Conductores { get; set; } = default!;

    public DbSet<SistemaGFYMP.Modelos.MantenimientoProgramado> MantenimientosProgramados { get; set; } = default!;

    public DbSet<SistemaGFYMP.Modelos.Taller> Talleres { get; set; } = default!;

public DbSet<SistemaGFYMP.Modelos.Usuario> Usuarios { get; set; } = default!;

public DbSet<SistemaGFYMP.Modelos.Camion> Camiones { get; set; } = default!;

}
