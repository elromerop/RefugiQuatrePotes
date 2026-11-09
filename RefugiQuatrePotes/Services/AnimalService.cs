using Microsoft.EntityFrameworkCore;
using RefugiQuatrePotes.Data;
using RefugiQuatrePotes.Models;

namespace RefugiQuatrePotes.Services;

public class AnimalService
{
    private RefugiContext _context;
        
    public AnimalService(RefugiContext context)
    {
        _context = context;
    }

    public List<Animal> GetDisponibles(Especie especie)
    {
        return _context.Animal
            .Where(a => a.Especie == especie && !a.EstaAdoptat)
            .OrderBy(a => a.DataEntrada)
            .ToList();
    }

    public Animal? CercaPerXip(string codiXip)
    {
        if (codiXip.Length != 15 || !codiXip.All(char.IsDigit))
        {
            Console.WriteLine($"[NO VÀLID] {codiXip} no vàlid");
            return null;
        }
        
        var animal = _context.Animal.FirstOrDefault(a => a.Xip.Codi == codiXip);

        if (animal == null)
        {
            Console.WriteLine($"[NO TROBAT] {codiXip} no trobat");
            return null;
        }
        
        return animal;
    }

    public List<Animal> CercaPerNom(string text)
    {
        text = text.ToLower();
        
        return _context.Animal
            .Where(a => a.Nom.ToLower().Contains(text))
            .ToList();
    }
}