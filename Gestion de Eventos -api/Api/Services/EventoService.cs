public class EventoService : IEventoService
{
    private readonly ApplicationDbContext _context;

    public EventoService(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<ServiceResponse<List<EventoDto>>> GetAllEventosAsync()
    {
        var eventos = await _context.Eventos.ToListAsync();
        var eventoDtos = eventos.Select(e => new EventoDto
        {
            Id = e.Id,
            Titulo = e.Titulo,
            Descripcion = e.Descripcion,
            Fecha = e.Fecha
        }).ToList();

        return new ServiceResponse<List<EventoDto>> { Data = eventoDtos };
    }

    public async Task<ServiceResponse<EventoDto>> CrearEventoAsync(EventoDto eventoDto)
    {
        if (eventoDto.Fecha <= DateTime.Now)
        {
            return new ServiceResponse<EventoDto> { Success = false, Message = "No se puede crear un evento en el pasado." };
        }

        var evento = new Evento
        {
            Titulo = eventoDto.Titulo,
            Descripcion = eventoDto.Descripcion,
            Fecha = eventoDto.Fecha
        };

        _context.Eventos.Add(evento);
        await _context.SaveChangesAsync();

        eventoDto.Id = evento.Id;

        return new ServiceResponse<EventoDto> { Data = eventoDto };
    }
}
