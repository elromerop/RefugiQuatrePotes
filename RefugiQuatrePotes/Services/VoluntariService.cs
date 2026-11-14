using RefugiQuatrePotes.Data;
using RefugiQuatrePotes.Models;

namespace RefugiQuatrePotes.Services;

public class VoluntariService
{
    private RefugiContext _context;
        
    public VoluntariService(RefugiContext context)
    {
        _context = context;
    }

    public bool AssignarAnimal(string nomVoluntari, string codiXip)
    {
        var voluntari = _context.Voluntari.FirstOrDefault(v => v.Nom == nomVoluntari);

        if (voluntari == null)
        {
            Console.WriteLine($"[NO TROBAT] voluntari '{nomVoluntari}' no trobat");
            return false;
        }
        
        var animal = _context.Animal.FirstOrDefault(a => a.Xip.Codi == codiXip);

        if (animal == null)
        {
            Console.WriteLine($"[NO TROBAT] animal amb xip: '{codiXip}' no trobat");
            return false;
        }

        if (animal.EstaAdoptat)
        {
            Console.WriteLine($"[NO VÀLID] '{animal.Nom}' està adoptat");
            return false;
        }

        if (voluntari.Animals.Contains(animal))
        {
            Console.WriteLine($"[NO VÀLID] '{animal.Nom}' ja està assignat a '{nomVoluntari}'");
            return false;
        }

        if (voluntari.Animals.Count >= 4)
        {
            Console.WriteLine($"[NO VÀLID] '{nomVoluntari}' ja ha arribat al màxim d'animals");
            return false;
        }
        
        voluntari.Animals.Add(animal);
        
        _context.SaveChanges();
        
        Console.WriteLine($"[ASSIGNACIÓ] '{animal.Nom}' s'ha assignat a '{nomVoluntari}'");
        
        return true;
    }

    public bool DesassignarAnimal(string nomVoluntari, string codiXip)
    {
        var voluntari = _context.Voluntari.FirstOrDefault(v => v.Nom == nomVoluntari);

        if (voluntari == null)
        {
            Console.WriteLine($"[NO TROBAT] voluntari '{nomVoluntari}' no trobat");
            return false;
        }
        
        var animal = _context.Animal.FirstOrDefault(a => a.Xip.Codi == codiXip);

        if (animal == null)
        {
            Console.WriteLine($"[NO TROBAT] animal amb xip: '{codiXip}' no trobat");
            return false;
        }
        
        if (!voluntari.Animals.Contains(animal))
        {
            Console.WriteLine($"[NO VÀLID] '{nomVoluntari}' no té assignat a '{animal.Nom}'");
            return false;
        }

        voluntari.Animals.Remove(animal);
        
        Console.WriteLine($"[DESASSIGNACIÓ] '{animal.Nom}' s'ha desassignat de '{nomVoluntari}'");
        
        _context.SaveChanges();
        
        return true;
    }

    public bool RegistrarPasseig(string nomVoluntari, string codiXip, int minuts)
    {
        var voluntari = _context.Voluntari.FirstOrDefault(v => v.Nom == nomVoluntari);

        if (voluntari == null)
        {
            Console.WriteLine($"[NO TROBAT] voluntari '{nomVoluntari}' no trobat");
            return false;
        }
        
        var animal = _context.Animal.FirstOrDefault(a => a.Xip.Codi == codiXip);

        if (animal == null)
        {
            Console.WriteLine($"[NO TROBAT] animal amb xip: '{codiXip}' no trobat");
            return false;
        }
        
        if (!voluntari.Animals.Contains(animal))
        {
            Console.WriteLine($"[NO VÀLID] '{nomVoluntari}' no té assignat a '{animal.Nom}'");
            return false;
        }

        if (animal.Especie != Especie.GOS)
        {
            Console.WriteLine($"[NO VÀLID] '{animal.Nom}' no pot registrar un passeig perquè no és un gos");
            return false;
        }
        
        voluntari.MinutsPasseig += minuts;
        _context.SaveChanges();
        Console.WriteLine($"[PASSEIG] '{nomVoluntari}' ha tret a passejar a '{animal.Nom}' durant {minuts} minuts");
        return true;
    }
}