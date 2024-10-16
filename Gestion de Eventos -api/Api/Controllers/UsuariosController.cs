[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly EventManagementDbContext _context;

    public AuthController(EventManagementDbContext context)
    {
        _context = context;
    }

    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] Usuario user)
    {
        if (_context.Usuarios.Any(u => u.Email == user.Email))
            return BadRequest("Email already exists");

        _context.Usuarios.Add(user);
        await _context.SaveChangesAsync();
        return Ok("User registered successfully");
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginDto loginDto)
    {
        var user = _context.Usuarios.SingleOrDefault(u => u.Email == loginDto.Email && u.Password == loginDto.Password);
        if (user == null) return Unauthorized("Invalid login");

        var token = GenerateJwtToken(user);
        return Ok(new { token });
    }

    private string GenerateJwtToken(Usuario user)
    {
        // JWT token generation logic here
    }
}
