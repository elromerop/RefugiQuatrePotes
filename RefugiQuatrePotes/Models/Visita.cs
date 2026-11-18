using System.ComponentModel.DataAnnotations;

namespace RefugiQuatrePotes.Models;

public class Visita
{
    public int VisitaID { get; set; }
    public DateTime Data { get; set; }
    public Motiu Motiu { get; set; }
    public int Cost { get; set; }
    [Required]
    public virtual Animal Animal { get; set; }

    public Visita(DateTime data, Motiu motiu, int cost, Animal animal)
    {
        Data = data;
        Motiu = motiu;
        Cost = cost;
        Animal = animal;
    }
    
    public Visita(Motiu motiu, int cost, Animal animal)
    {
        Data = DateTime.Now;
        Motiu = motiu;
        Cost = cost;
        Animal = animal;
    }

    public Visita() { }

    public override string ToString() =>
        $"Visita (" +
        $"Data = {Data}, " +
        $"Motiu = {Motiu}, " +
        $"Cost = {Cost}, " +
        $"Animal = {Animal.Nom})";
}