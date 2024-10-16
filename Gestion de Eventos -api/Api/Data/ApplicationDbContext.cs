public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options) { }

    public DbSet<Usuario> Usuarios { get; set; }
    public DbSet<Evento> Eventos { get; set; }
    public DbSet<EventoUsuario> EventoUsuarios { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<EventoUsuario>()
            .HasKey(eu => new { eu.UsuarioId, eu.EventoId });

        modelBuilder.Entity<EventoUsuario>()
            .HasOne(eu => eu.Usuario)
            .WithMany(u => u.Eventos)
            .HasForeignKey(eu => eu.UsuarioId);

        modelBuilder.Entity<EventoUsuario>()
            .HasOne(eu => eu.Evento)
            .WithMany(e => e.Participantes)
            .HasForeignKey(eu => eu.EventoId);
    }
}
