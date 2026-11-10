using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RefugiQuatrePotes.Models;

[Index(nameof(Xip), IsUnique = true)]
public class Animal
{
    public int AnimalID { get; set; }
    [MaxLength(50), Required] 
    public string Nom { get; set; }
    [Required]
    public Especie Especie { get; set; }
    [Required]
    public bool EsFemella { get; set; }
    [Required] 
    public DateTime DataNaixement { get; set; }
    [Required]
    public DateTime DataEntrada { get; set; }
    [Range(0.1f,100f)]
    public double Pes { get; set; }
    public bool EstaAdoptat { get; set; }
    public DateTime? DataAdopcio { get; set; }
    [Precision(8, 2)]
    public decimal QuotaAdopcio { get; set; }
    [Required]
    public virtual Xip Xip { get; set; }
    public virtual Adoptant? Adoptant { get; set; }

    public Animal(string nom, Especie especie, bool esFemella, DateTime dataNaixement, DateTime dataEntrada, double pes, bool estaAdoptat, DateTime? dataAdopcio, decimal quotaAdopcio, Xip xip)
    {
        Nom = nom;
        Especie = especie;
        EsFemella = esFemella;
        DataNaixement = dataNaixement;
        DataEntrada = dataEntrada;
        Pes = pes;
        EstaAdoptat = estaAdoptat;
        DataAdopcio = dataAdopcio;
        QuotaAdopcio = quotaAdopcio;
        Xip = xip;
    }

    public Animal(string nom, Especie especie, bool esFemella, DateTime dataNaixement, double pes)
    {
        Nom = nom;
        Especie = especie;
        EsFemella = esFemella;
        DataNaixement = dataNaixement;
        DataEntrada = DateTime.Now;
        Pes = pes;
        EstaAdoptat = false;
        DataAdopcio = null;
        QuotaAdopcio = 0;
        Xip = null;
    }
    
    public Animal(){}

    public override string ToString() =>
        $"Animal (" +
        $"Nom = {Nom}, " +
        $"Especie = {Especie}, " +
        $"EsFemella = {EsFemella}, " +
        $"DataNaixement = {DataNaixement}, " +
        $"DataEntrada = {DataEntrada}, " +
        $"Pes = {Pes}, " +
        $"EstaAdoptat = {EstaAdoptat}, " +
        $"DataAdopcio = {DataAdopcio}, " +
        $"QuotaAdopcio = {QuotaAdopcio})";
}