using RefugiQuatrePotes.Data;
using RefugiQuatrePotes.Models;

namespace RefugiQuatrePotes;

internal class Program
{
    static void Main(string[] args)
    {
        using (var ctx = new RefugiContext())
        {
            ctx.Database.EnsureDeleted();
            ctx.Database.EnsureCreated();

            var xip1 = new Xip("1234567890ABCDE");
            var animal1 = new Animal("Hola", Especie.GOS, true, DateTime.Now, 10);
            animal1.Xip = xip1;
            ctx.Animal.Add(animal1);
            ctx.Xip.Add(xip1);
            
            ctx.SaveChanges();
            
            var llista = ctx.Animal.ToList();
            foreach (var animal in llista)
            {
                Console.WriteLine(animal);
            }
        }
    }
}