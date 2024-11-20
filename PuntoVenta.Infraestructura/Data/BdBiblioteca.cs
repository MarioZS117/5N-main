using Microsoft.EntityFrameworkCore;
using PuntoVenta.Models.Response;
public class BDBiblioteca:DbContext{
    public virtual DbSet<Usuarios> Usuarios {get;set;} //Identifica el nombre de la tabla en la bd, debe tener el mismo nombre y la misma estructura
    public virtual DbSet<Libros> Libros {get;set;}
    public virtual DbSet<Copias> Copias {get;set;}
    public virtual DbSet<Autores> Autores {get;set;}
    public virtual DbSet<Clasificaciones> Clasificaciones { get; set; }
    public virtual DbSet<Generos> Generos { get; set; }
    public virtual DbSet<Empleados> Empleados{ get; set; }
    public virtual DbSet<Prestamo> Prestamo{ get; set; }
    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer(Configuraciones.CadenaConexion, op =>
            {
                op.EnableRetryOnFailure(5, new TimeSpan(0, 0, 10), null);
            });
        }
        base.OnConfiguring(optionsBuilder);
    }
}