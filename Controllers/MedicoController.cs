using Agendamentos.Data;
using Agendamentos.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Agendamentos.Controllers;

[Authorize(Roles = "Admin")]
public class MedicoController : Controller
{
   private readonly ApplicationDbContext _context;
   
   public MedicoController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index(string search)
    {
        var medicos = _context.Medicos.AsQueryable();
        if (!string.IsNullOrEmpty(search))
        {
            medicos = medicos.Where(m => m.Nome.Contains(search) || m.Especialidade.Contains(search));
        }
        return View(await medicos.ToListAsync());
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public async Task<IActionResult> Create(Medico medico)
    {
        if (ModelState.IsValid)
        {
            medico.DataCriacao = DateTime.Now;
            _context.Add(medico);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(medico);
    }

    [HttpGet]
    public async Task<IActionResult> Edit(int id)
    {
        var medico = await _context.Medicos.FindAsync(id);
        if (medico == null) return NotFound();
        
        return View(medico);
    }

    [HttpPost]
    public async Task<IActionResult> Edit(Medico medico)
    {
        if (ModelState.IsValid)
        {
            _context.Update(medico);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(medico);
    }

    public async Task<IActionResult> ToggleAtivo(int id)
    {
        var medico = await _context.Medicos.FindAsync(id);
        if (medico == null) return NotFound();

        medico.Ativo = !medico.Ativo;

        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }
    
    public async Task<IActionResult> Delete(int id)
    {
        var medico = await _context.Medicos.FindAsync(id);

        if (medico == null) return NotFound();

        if (medico.Ativo)
        {
            ViewBag.Erro = "Médico deve ser inativado antes de excluir.";
            return View("Index", medico);
        }

        _context.Medicos.Remove(medico);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }
}