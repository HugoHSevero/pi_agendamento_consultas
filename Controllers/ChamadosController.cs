using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using Agendamentos.Data;

public class ChamadosController : Controller
{
    private readonly ApplicationDbContext _context;

    public ChamadosController(ApplicationDbContext context)
    {
        _context = context;
    }

    // LISTA
    public async Task<IActionResult> Index()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
        
        var chamados = await _context.Chamados
            .Where(c => c.UsuarioId == userId)
            .ToListAsync();

        return View(chamados);
    }
    
    // LISTA TODOS OS CHAMADOS
    public async Task<IActionResult> Backlog()
    {
        if (!User.IsInRole("Admin"))
            return Unauthorized();

        var chamados = await _context.Chamados
            .Include(c => c.Usuario)
            .ToListAsync();

        return View(chamados);
    }

    // CREATE (GET)
    public IActionResult Create()
    {
        return View();
    }

    // CREATE (POST)
    [HttpPost]
    public async Task<IActionResult> Create(Chamado chamado)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        chamado.UsuarioId = userId;
        chamado.Status = StatusChamado.Aberto;
        chamado.DataCriacao = DateTime.Now;

        _context.Add(chamado);
        await _context.SaveChangesAsync();

        return RedirectToAction("Index");
    }
    
    public async Task<IActionResult> Details(int id)
    {
        var chamado = await _context.Chamados
            .Include(c => c.Usuario)
            .FirstOrDefaultAsync(c => c.Id == id);

        if (chamado == null)
            return NotFound();

        return View(chamado);
    }
    
    [HttpPost]
    public async Task<IActionResult> AlterarStatus(int id, StatusChamado status)
    {
        var chamado = await _context.Chamados.FindAsync(id);

        if (chamado == null)
            return NotFound();

        chamado.Status = status;

        await _context.SaveChangesAsync();

        return RedirectToAction("Details", new { id = id });
    }
}