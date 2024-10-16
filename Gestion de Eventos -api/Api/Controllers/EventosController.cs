[Authorize]
[ApiController]
[Route("api/[controller]")]
public class EventosController : ControllerBase
{
    private readonly EventManagementDbContext _context;

    public EventosController(EventManagementDbContext context)
    {
        _context = context;
    }

    [HttpPost]
    public async Task<IActionResult> CreateEvento([FromBody] Evento evento)
    {
        if (evento.Fecha < DateTime.Now)
            return BadRequest("Cannot create an event in the past");

        _context.Eventos.Add(evento);
        await _context.SaveChangesAsync();
        return Ok(evento);
    }

    [HttpPost("{eventoId}/join")]
    public async Task<IActionResult> JoinEvento(int eventoId)
    {
        var evento = await _context.Eventos.FindAsync(eventoId);
        if (evento == null || evento.Fecha < DateTime.Now)
            return BadRequest("Cannot join an event that has already passed");

        var userId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value);
        var user = await _context.Usuarios.FindAsync(userId);
        if (user == null)
            return Unauthorized();

        if (_context.EventoUsuarios.Any(eu => eu.EventoId == eventoId && eu.UsuarioId == userId))
            return BadRequest("User already joined");

        _context.EventoUsuarios.Add(new EventoUsuario { EventoId = eventoId, UsuarioId = userId });
        await _context.SaveChangesAsync();
        return Ok("Joined successfully");
    }

    // Otros métodos (Get, Update, Delete) para la gestión de eventos...
}


