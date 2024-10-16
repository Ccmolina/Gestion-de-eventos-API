public class Evento
{
    public int Id { get; set; }
    public string Titulo { get; set; }
    public string Descripcion { get; set; }
    public DateTime Fecha { get; set; }

    // Relación muchos a muchos
    public ICollection<EventoUsuario> Participantes { get; set; }
}