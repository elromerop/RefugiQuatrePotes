using RefugiQuatrePotes.Data;
using RefugiQuatrePotes.Models;
using RefugiQuatrePotes.Services;

namespace RefugiQuatrePotes;

internal class Program
{
    static void Main(string[] args)
    {
        using (var ctx = new RefugiContext())
        {
            var animalService = new AnimalService(ctx);
            var adoptantService = new AdoptantService(ctx);
            
            ctx.Database.EnsureDeleted();
            ctx.Database.EnsureCreated();
            
            ctx.Animal.Add(new Animal(
                "Max",
                Especie.GOS,
                false,
                new DateTime(2021, 3, 15),
                new DateTime(2024, 1, 10),
                24.5, false,
                null, 0,
                new Xip("900000000000001", new DateTime(2024, 1, 10))));

            ctx.Animal.Add(new Animal(
                "Luna",
                Especie.GOS,
                true,
                new DateTime(2019, 7, 22),
                new DateTime(2023, 5, 18),
                18.2, false,
                null, 0,
                new Xip("900000000000002", new DateTime(2023, 5, 18))));

            ctx.Animal.Add(new Animal(
                "Rocky",
                Especie.GOS,
                false,
                new DateTime(2022, 11, 3),
                new DateTime(2025, 2, 14),
                30.0, false,
                null, 0,
                new Xip("900000000000003", new DateTime(2025, 2, 14))));

            ctx.Animal.Add(new Animal(
                "Nala",
                Especie.GOS,
                true,
                new DateTime(2020, 5, 9),
                new DateTime(2024, 9, 2),
                12.8, false,
                null, 0,
                new Xip("900000000000004", new DateTime(2024, 9, 2))));
            
            ctx.Animal.Add(new Animal(
                "Mimi",
                Especie.GAT,
                true,
                new DateTime(2023, 2, 17),
                new DateTime(2024, 4, 20),
                3.5, false,
                null, 0,
                new Xip("900000000000005", new DateTime(2024, 4, 20))));

            ctx.Animal.Add(new Animal(
                "Simba",
                Especie.GAT,
                false,
                new DateTime(2021, 8, 30),
                new DateTime(2023, 11, 7),
                5.2, false,
                null, 0,
                new Xip("900000000000006", new DateTime(2023, 11, 7))));

            ctx.Animal.Add(new Animal(
                "Nina",
                Especie.GAT,
                true,
                new DateTime(2024, 1, 12),
                new DateTime(2025, 6, 25),
                2.8, true,
                new DateTime(2026, 3, 2), 0,
                new Xip("900000000000007", new DateTime(2025, 6, 25))));
            
            ctx.Animal.Add(new Animal(
                "Bunny",
                Especie.CONILL,
                false,
                new DateTime(2022, 6, 5),
                new DateTime(2024, 7, 13),
                2.1, false,
                null, 0,
                new Xip("900000000000008", new DateTime(2024, 7, 13))));
            
            ctx.Animal.Add(new Animal(
                "Piolí",
                Especie.AU,
                true,
                new DateTime(2023, 9, 19),
                new DateTime(2025, 1, 8),
                0.8, false,
                null, 0,
                new Xip("900000000000009", new DateTime(2025, 1, 8))));

            ctx.Adoptant.Add(new Adoptant("12345678A", "Lucas", "Romero Pérez", true));
            ctx.Adoptant.Add(new Adoptant("87654321Z", "Jaime", "Jimenez", false));
            
            
            ctx.SaveChanges();
            
            Console.WriteLine("===| Dades Inserides |===");
            var llista = ctx.Animal.ToList();
            foreach (var animal in llista)
            {
                Console.WriteLine(animal+" | "+animal.Xip);
            }
            Console.WriteLine("");
            
            Console.WriteLine("===| Gats disponibles |===");
            foreach (var animal in animalService.GetDisponibles(Especie.GAT))
            {
                Console.WriteLine(animal);
            }
            Console.WriteLine("");
            
            Console.WriteLine("===| CercaPerXip |===");
            Console.WriteLine("(123) => "+animalService.CercaPerXip("123"));
            Console.WriteLine("(0123456789ABCDE) => "+animalService.CercaPerXip("0123456789ABCDE"));
            Console.WriteLine("(900034567800077) => "+animalService.CercaPerXip("900034567800077"));
            Console.WriteLine("(900000000000005) => "+animalService.CercaPerXip("900000000000005"));
            Console.WriteLine("");
            
            Console.WriteLine("===| CercaPerNom |===");
            Console.WriteLine("===| (lu) |===");
            foreach (var animal in animalService.CercaPerNom("lu"))
            {
                Console.WriteLine(animal);
            }
            Console.WriteLine("===| (LÍ) |===");
            foreach (var animal in animalService.CercaPerNom("LÍ"))
            {
                Console.WriteLine(animal);
            }
            Console.WriteLine("===| (iM) |===");
            foreach (var animal in animalService.CercaPerNom("iM"))
            {
                Console.WriteLine(animal);
            }
            Console.WriteLine("===| (abc123) |===");
            foreach (var animal in animalService.CercaPerNom("abc123"))
            {
                Console.WriteLine(animal);
            }
            
            Console.WriteLine("===| Adoptar |===");
            adoptantService.Adoptar("12345678A", "fdghdfyhrgfh");
            adoptantService.Adoptar("sdfsdfdfgdfg", "900000000000007");
            adoptantService.Adoptar("87654321Z", "900000000000001");
            adoptantService.Adoptar("12345678A", "900000000000007");
            adoptantService.Adoptar("12345678A", "900000000000008");
            adoptantService.Adoptar("12345678A", "900000000000009");
            adoptantService.Adoptar("12345678A", "900000000000003");
            adoptantService.Adoptar("12345678A", "900000000000004");
            Console.WriteLine("");
            
            Console.WriteLine("===| Retornar |===");
            adoptantService.Retornar("sdjlfhsdkjfh");
            adoptantService.Retornar("900000000000004");
            adoptantService.Retornar("900000000000003");
            Console.WriteLine("===| Animals de Lucas |===");
            foreach (var animal in ctx.Adoptant.FirstOrDefault(a => a.DNI == "12345678A").Animals)
            {
                Console.WriteLine(animal);
            }
            Console.WriteLine("===| Animal Retornat |===");
            Console.WriteLine(ctx.Animal.FirstOrDefault(a => a.Xip.Codi == "900000000000003"));
            Console.WriteLine("");
            Console.WriteLine("===| Llistat adoptants |===");
            Console.WriteLine("===| (Min '0') |===");
            adoptantService.LlistatAdoptants(0);
            Console.WriteLine("===| (Min '2') |===");
            adoptantService.LlistatAdoptants(2);
            Console.WriteLine("");
        }
    }
}