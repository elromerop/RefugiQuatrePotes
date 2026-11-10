using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RefugiQuatrePotes.Models;

[Index(nameof(DNI), IsUnique = true)]
public class Adoptant
{
    public int AdoptantID { get; set; }
    [MaxLength(9), Required]
    public string DNI { get; set; }
    [MaxLength(50), Required]
    public string Nom { get; set; }
    [MaxLength(100), Required]
    public string Cognoms { get; set; }
    [Required]
    public bool TeJardi { get; set; }
    [Required]
    public DateTime DataRegistre { get; set; }
    public virtual ICollection<Animal> Animals { get; set; }

    public Adoptant(string dni, string nom, string cognoms, bool teJardi, DateTime dataRegistre)
    {
        DNI = dni;
        Nom = nom;
        Cognoms = cognoms;
        TeJardi = teJardi;
        DataRegistre = dataRegistre;
        Animals = new HashSet<Animal>();
    }

    public Adoptant(string dni, string nom, string cognoms, bool teJardi)
    {
        DNI = dni;
        Nom = nom;
        Cognoms = cognoms;
        TeJardi = teJardi;
        DataRegistre = DateTime.Now;
        Animals = new HashSet<Animal>();
    }
    
    public Adoptant(){}
    
    public override string ToString() =>
        $"Adoptant (" +
        $"Nom = {Nom}, " +
        $"Cognoms = {Cognoms}, " +
        $"DNI = {DNI}, " +
        $"TeJardi = {TeJardi}, " +
        $"DataRegistre = {DataRegistre})";
}