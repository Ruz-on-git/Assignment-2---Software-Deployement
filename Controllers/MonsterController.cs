using Assignment_2.Data;
using Assignment_2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace Assignment_2.Controllers;

[Authorize]
public class MonstersController : Controller
{
    private readonly ApplicationDbContext _context;

    public MonstersController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var monsters = await _context.Monsters
            .Where(m => m.UserId == userId)
            .ToListAsync();

        return View(monsters);
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var monster = await _context.Monsters
            .FirstOrDefaultAsync(m => m.Id == id && m.UserId == userId);

        if (monster == null)
        {
            return NotFound();
        }

        return View(monster);
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(Monster monster)
    {
        if (ModelState.IsValid)
        {
            monster.UserId = User.FindFirstValue(ClaimTypes.NameIdentifier)!;
            monster.CurrentHealth = monster.MaxHealth;

            _context.Monsters.Add(monster);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        return View(monster);
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var monster = await _context.Monsters
            .FirstOrDefaultAsync(m => m.Id == id && m.UserId == userId);

        if (monster == null)
        {
            return NotFound();
        }

        return View(monster);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, Monster monster)
    {
        if (id != monster.Id)
        {
            return NotFound();
        }

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var existingMonster = await _context.Monsters
            .FirstOrDefaultAsync(m => m.Id == id && m.UserId == userId);

        if (existingMonster == null)
        {
            return NotFound();
        }

        if (!ModelState.IsValid)
        {
            return View(monster);
        }

        existingMonster.Name = monster.Name;
        existingMonster.Type = monster.Type;
        existingMonster.ChallengeRating = monster.ChallengeRating;
        existingMonster.MaxHealth = monster.MaxHealth;
        existingMonster.CurrentHealth = monster.CurrentHealth;
        existingMonster.ArmorClass = monster.ArmorClass;
        existingMonster.Speed = monster.Speed;

        existingMonster.Strength = monster.Strength;
        existingMonster.Dexterity = monster.Dexterity;
        existingMonster.Constitution = monster.Constitution;
        existingMonster.Intelligence = monster.Intelligence;
        existingMonster.Wisdom = monster.Wisdom;
        existingMonster.Charisma = monster.Charisma;

        existingMonster.Description = monster.Description;

        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var monster = await _context.Monsters
            .FirstOrDefaultAsync(m => m.Id == id && m.UserId == userId);

        if (monster == null)
        {
            return NotFound();
        }

        return View(monster);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

        var monster = await _context.Monsters
            .FirstOrDefaultAsync(m => m.Id == id && m.UserId == userId);

        if (monster == null)
        {
            return NotFound();
        }

        _context.Monsters.Remove(monster);
        await _context.SaveChangesAsync();

        return RedirectToAction(nameof(Index));
    }
}