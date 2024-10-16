public class Usuario
{
    public int Id { get; set; }
    public string Nombre { get; set; }
    public string Email { get; set; }
    public string PasswordHash { get; set; }

    // Relación muchos a muchos
    public ICollection<EventoUsuario> Eventos { get; set; }
}