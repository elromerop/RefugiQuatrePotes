using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;

namespace RefugiQuatrePotes.Models;

[Index(nameof(Nom), IsUnique = true)]
public class Voluntari
{ 
    public int VoluntariID { get; set; }
    [Required, MaxLength(50)]
    public string Nom { get; set; }
    [Required]
    public Torn Torn { get; set; }
    public int MinutsPasseig { get; set; }
    public virtual ICollection<Animal> Animals { get; set; }

    public Voluntari(string nom, Torn torn, int minutsPasseig)
    {
        Nom = nom;
        Torn = torn;
        MinutsPasseig = minutsPasseig;
        Animals = new HashSet<Animal>();
    }
    
    public Voluntari(string nom, Torn torn)
    {
        Nom = nom;
        Torn = torn;
        MinutsPasseig = 0;
        Animals = new HashSet<Animal>();
    }
    
    public Voluntari() {}
    
    public override string ToString() => 
        $"Voluntari (" +
        $"Nom = {Nom}, " +
        $"Torn = {Torn}, " +
        $"MinutsPasseig = {MinutsPasseig})";
}