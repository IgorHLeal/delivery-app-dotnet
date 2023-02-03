using DeliveryApp.Context;
using DeliveryApp.Models;
using DeliveryApp.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace DeliveryApp.Repositories;
public class LancheRepository : ILancheRepository
{
    private readonly AppDbContext _context;

    public LancheRepository(AppDbContext context)
    {
        _context = context;
    }

    public IEnumerable<Lanche> Lanches => _context.Lanches.Include(c => c.Categoria);

    public IEnumerable<Lanche> LanchesPreferidos => _context.Lanches.Where(p => p.IsLanchePreferido).Include(c => c.Categoria);

    public Lanche GetLancheById(int lancheId)
    {
        return _context.Lanches.FirstOrDefault(lanche => lanche.LancheId == lancheId);
    }
}
