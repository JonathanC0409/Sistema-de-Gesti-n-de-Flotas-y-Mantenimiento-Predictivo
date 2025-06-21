using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SistemaLogs.Modelos;

public class App2DbContext : DbContext
{
    public App2DbContext(DbContextOptions<App2DbContext> options)
        : base(options)
    {
    }

    public DbSet<SistemaLogs.Modelos.LogAuditoria> LogsAuditorias { get; set; } = default!;

public DbSet<SistemaLogs.Modelos.AlertaMantenimiento> AlertasMantenimientos { get; set; } = default!;

public DbSet<SistemaLogs.Modelos.LecturaSensor> LecturasSensores { get; set; } = default!;
}
