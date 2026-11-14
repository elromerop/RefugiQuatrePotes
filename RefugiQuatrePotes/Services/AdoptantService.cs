using RefugiQuatrePotes.Data;
using RefugiQuatrePotes.Models;

namespace RefugiQuatrePotes.Services;

public class AdoptantService
{
    private RefugiContext _context;
        
    public AdoptantService(RefugiContext context)
    {
        _context = context;
    }

    public bool Adoptar(string dni, string codiXip)
    {
        var animal = _context.Animal.FirstOrDefault(a => a.Xip.Codi == codiXip);
        var adoptant = _context.Adoptant.FirstOrDefault(a => a.DNI == dni);

        if (animal == null)
        {
            Console.WriteLine($"[Adopció] animal amb xip de codi: '{codiXip}' no trobat");
            return false;
        }
        
        if (adoptant == null)
        {
            Console.WriteLine($"[Adopció] adoptant amb DNI: '{dni}' no trobat");
            return false;
        }

        if (adoptant.Animals.Count >= 3)
        {
            Console.WriteLine($"[Adopció] adoptant {adoptant.Nom} ha arribat al límit d'animals");
            return false;
        }

        if (animal.EstaAdoptat)
        {
            Console.WriteLine($"[Adopció] {animal.Nom} ja ha estat adoptat");
            return false;
        }
        
        if (animal.Especie == Especie.GOS && !adoptant.TeJardi)
        {
            Console.WriteLine($"[Adopció] {animal.Nom} és un gos i {adoptant.Nom} no en té jardi");
            return false;
        }

        animal.Adoptant = adoptant;
        animal.EstaAdoptat = true;
        animal.DataAdopcio = DateTime.Now;

        foreach (var voluntari in animal.Voluntaris)
        {
            voluntari.Animals.Remove(animal);
        }
        
        _context.SaveChanges();
        
        Console.WriteLine($"[Adopció] {adoptant.Nom} ha adoptat a {animal.Nom}");
        return true;
    }

    public bool Retornar(string codiXip)
    {
        var animal = _context.Animal.FirstOrDefault(a => a.Xip.Codi == codiXip);

        if (animal == null)
        {
            Console.WriteLine($"[Retornar] animal amb xip de codi: '{codiXip}' no trobat");
            return false;
        }

        if (!animal.EstaAdoptat)
        {
            Console.WriteLine($"[Retornar] {animal.Nom} no està adoptat");
            return false;
        }
        
        animal.Adoptant = null;
        animal.EstaAdoptat = false;
        animal.DataEntrada = DateTime.Now;
        animal.DataAdopcio = null;
        _context.SaveChanges();
        Console.WriteLine($"[Retornar] s'ha retornat a {animal.Nom}");
        return true;
    }

    public void LlistatAdoptants(int minAnimals)
    {
        foreach (var adoptant in _context.Adoptant
                     .Where(a => a.Animals.Count >= minAnimals)
                     .OrderByDescending(a => a.Animals.Count))
        {
            Console.WriteLine(adoptant+" - Animals: "+adoptant.Animals.Count);
        }
    }
    
}