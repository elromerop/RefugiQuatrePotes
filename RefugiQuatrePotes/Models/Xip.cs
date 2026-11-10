using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace RefugiQuatrePotes.Models;

[Index(nameof(Codi), IsUnique = true)]
public class Xip
{
    public int XipID { get; set; }
    [Required, StringLength(15, MinimumLength = 15)]
    public string Codi { get; set; }
    [Required]
    public DateTime DataImplantacio { get; set; }
    public virtual Animal Animal { get; set; }

    public Xip(string codi, DateTime dataImplantacio)
    {
        Codi = codi;
        DataImplantacio = dataImplantacio;
    }

    public Xip(string codi)
    {
        Codi = codi;
        DataImplantacio = DateTime.Now;
    }
    public Xip(){}

    public override string ToString() => 
        $"Xip (" +
        $"Codi = {Codi}, " +
        $"DataImplantacio = {DataImplantacio})";
}