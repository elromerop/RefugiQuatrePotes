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

    public List<Animal> GetTots()
    {
        return _context.Animal.ToList();
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

    public bool CanviarMare(string codiXipCria, string? codiXipMare)
    {
        var cria = _context.Animal.FirstOrDefault(a => a.Xip.Codi == codiXipCria);
        
        if (cria == null)
        {
            Console.WriteLine($"[NO TROBAT] cria amb xip '{codiXipCria}' no trobada");
            return false;
        }

        if (codiXipMare != null)
        {
            var mare = _context.Animal.FirstOrDefault(a => a.Xip.Codi == codiXipMare);

            if (mare == null)
            {
                Console.WriteLine($"[NO TROBAT] mare amb xip '{codiXipMare}' no trobada");
                return false;
            }

            if (!mare.EsFemella)
            {
                Console.WriteLine($"[NO VÀLID] {mare.Nom} no es una famella");
                return false;
            }

            if (mare == cria.Mare)
            {
                Console.WriteLine($"[NO VÀLID] {mare.Nom} ja és la mare de {cria.Nom}");
                return false;
            }

            cria.Mare = mare;
            
            Console.WriteLine($"[MARE CANVIADA] {mare.Nom} és la nova mare de {cria.Nom}");
            _context.SaveChanges();
            return true;
        }



        if (cria.Mare == null)
        {
            Console.WriteLine($"[NO VÀLID] {cria.Nom} ja no té mare");
            return false;
        }
        
        cria.Mare = null;
        
        Console.WriteLine($"[MARE CANVIADA] {cria.Nom} es queda sense mare");
        _context.SaveChanges();
        return true;
    }

    public bool RegistrarCria(string codiXipMare, Animal cria)
    {
        if (_context.Animal.FirstOrDefault(a => a.Xip.Codi == cria.Xip.Codi) != null)
        {
            Console.WriteLine($"[NO VÀLID] ja existeix una cria amb el xip '{cria.Xip.Codi}'");
            return false;
        }
        
        _context.Add(cria);
        _context.SaveChanges();
        
        var mare = _context.Animal.FirstOrDefault(a => a.Xip.Codi == codiXipMare);

        if (mare == null)
        {
            Console.WriteLine($"[NO TROBAT] mare amb xip '{codiXipMare}' no trobada");
            return false;
        }
        
        if (!mare.EsFemella)
        {
            Console.WriteLine($"[NO VÀLID] la mare '{mare.Nom}' no és una famella");
            return false;
        }
        
        if (cria.Especie != mare.Especie)
        {
            Console.WriteLine($"[NO VÀLID] la cria '{cria.Nom}' no és de la mateixa espècie que la mare '{mare.Nom}'");
            return false;
        }
        
        if (mare.DataNaixement > cria.DataNaixement)
        {
            Console.WriteLine($"[NO VÀLID] la cria '{cria.Nom}' no pot haver nèixer abans que la mare '{mare.Nom}'");
            return false;
        }

        cria.Mare = mare;
        Console.WriteLine($"[CRIA REGISTRADA] la cria '{cria.Nom}' ha estat registrada amb la mare '{mare.Nom}'");
        _context.SaveChanges();
        return true;
    }

    public List<Animal> Descendents(string codiXip)
    {
        var animal = _context.Animal.FirstOrDefault(a => a.Xip.Codi == codiXip);

        if (animal == null)
        {
            Console.WriteLine($"[NO TROBAT] xip '{codiXip}' no trobat");
            return new List<Animal>();
        }
        
        HashSet<Animal> descendents = new();

        foreach (var cria in animal.Cries)
        {
            if (descendents.Contains(cria)) continue;
            
            descendents.Add(cria);
            var descendentsCria = Descendents(cria.Xip.Codi);
            if (descendentsCria != null && descendentsCria.Count > 0)
            {
                foreach (var a in descendentsCria) descendents.Add(a);
            }
        }

        return descendents.ToList();
    }
}