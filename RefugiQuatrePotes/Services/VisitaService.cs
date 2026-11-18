using RefugiQuatrePotes.Data;
using RefugiQuatrePotes.Models;

namespace RefugiQuatrePotes.Services;

public class VisitaService
{
    private RefugiContext _context;
        
    public VisitaService(RefugiContext context)
    {
        _context = context;
    }
    
    public void GenerarVisites(int quantes)
    {
        var animals = _context.Animal.ToList();
        for (int i = 0; i < quantes; i++)
        {
            var r = new Random();

            var animal = animals[r.Next(animals.Count)];

            var entrada = animal.DataEntrada;
            var maxData = animal.EstaAdoptat && animal.DataAdopcio != null ? 
                    (DateTime) animal.DataAdopcio : 
                    DateTime.Now;
            
            int diesEntreDates = (maxData - entrada).Days;
            
            var data = DateTime.Today.AddDays(-r.Next(0, diesEntreDates));

            var motiu = (Motiu)r.Next(Enum.GetValues<Motiu>().Length);

            int preu = 0;
            
            switch (motiu)
            {
                case Motiu.REVISIO: preu = r.Next(30, 61); break;
                case Motiu.VACUNA : preu = r.Next(25, 51); break;
                case Motiu.FERIDA : preu = r.Next(60, 151); break;
                case Motiu.CIRURGIA : preu = r.Next(250, 601); break;
                default: preu = 0; break;
            }
            
            var visita = new Visita(data, motiu, preu, animal);
            
            Console.WriteLine(visita);
            _context.Visita.Add(visita);
        }

        _context.SaveChanges();
    }

    public void EstadistiquesPerMotiu()
    {
        var stats = _context.Visita
            .GroupBy(v => v.Motiu)
            .Select(g => new
            {
                Motiu = g.Key,
                Total = g.Count(),
                CostTotal = g.Sum(vg => vg.Cost),
                CostMitja = g.Average(vg => vg.Cost).ToString("#.00")
            })
            .OrderByDescending(o => o.CostTotal);

        foreach (var s in stats)
        {
            Console.WriteLine($"[{s.Motiu}] " +
                              $"Total: '{s.Total}', " +
                              $"Cost Total: '{s.CostTotal}$', " +
                              $"Cost Mitjà: '{s.CostMitja}$'");
        }
    }

    public void EstadistiquesPerMes()
    {
        var stats = _context.Visita
            .GroupBy(v => new { Mes = v.Data.Month, Any = v.Data.Year })
            .Select(g => new
            {
                AnyMes = g.Key,
                Visites = g.Count(),
                CostTotal = g.Sum(vg => vg.Cost),
            })
            .OrderByDescending(o => o.AnyMes.Any)
            .ThenByDescending(o => o.AnyMes.Mes);

        foreach (var s in stats)
        {
            Console.WriteLine($"[{s.AnyMes.Mes}/{s.AnyMes.Any}] " +
                              $"Visites: '{s.Visites}', " +
                              $"Cost Total: '{s.CostTotal}$'");
        }
    }
}