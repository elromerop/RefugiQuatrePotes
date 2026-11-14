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
            var voluntariService = new VoluntariService(ctx);
            
            ctx.Database.EnsureDeleted();
            ctx.Database.EnsureCreated();

            var max = new Animal(
                "Max",
                Especie.GOS,
                false,
                new DateTime(2021, 3, 15),
                new DateTime(2024, 1, 10),
                24.5, false,
                null, 0,
                new Xip("900000000000001", new DateTime(2024, 1, 10)));
            ctx.Animal.Add(max);

            var luna = new Animal(
                "Luna",
                Especie.GOS,
                true,
                new DateTime(2019, 7, 22),
                new DateTime(2023, 5, 18),
                18.2, false,
                null, 0,
                new Xip("900000000000002", new DateTime(2023, 5, 18)));
            ctx.Animal.Add(luna);

            var rocky = new Animal(
                "Rocky",
                Especie.GOS,
                false,
                new DateTime(2022, 11, 3),
                new DateTime(2025, 2, 14),
                30.0, false,
                null, 0,
                new Xip("900000000000003", new DateTime(2025, 2, 14)));
            ctx.Animal.Add(rocky);
            

            var nala = new Animal(
                "Nala",
                Especie.GOS,
                true,
                new DateTime(2020, 5, 9),
                new DateTime(2024, 9, 2),
                12.8, false,
                null, 0,
                new Xip("900000000000004", new DateTime(2024, 9, 2)));
            ctx.Animal.Add(nala);
            
            var mimi = new Animal(
                "Mimi",
                Especie.GAT,
                true,
                new DateTime(2023, 2, 17),
                new DateTime(2024, 4, 20),
                3.5, false,
                null, 0,
                new Xip("900000000000005", new DateTime(2024, 4, 20)));
            ctx.Animal.Add(mimi);

            var simba = new Animal(
                "Simba",
                Especie.GAT,
                false,
                new DateTime(2021, 8, 30),
                new DateTime(2023, 11, 7),
                5.2, false,
                null, 0,
                new Xip("900000000000006", new DateTime(2023, 11, 7)));
            ctx.Animal.Add(simba);

            var nina = new Animal(
                "Nina",
                Especie.GAT,
                true,
                new DateTime(2024, 1, 12),
                new DateTime(2025, 6, 25),
                2.8, true,
                new DateTime(2026, 3, 2), 0,
                new Xip("900000000000007", new DateTime(2025, 6, 25)));
            ctx.Animal.Add(nina);
            
            var bunny = new Animal(
                "Bunny",
                Especie.CONILL,
                false,
                new DateTime(2022, 6, 5),
                new DateTime(2024, 7, 13),
                2.1, false,
                null, 0,
                new Xip("900000000000008", new DateTime(2024, 7, 13)));
            ctx.Animal.Add(bunny);
            
            var pioli = new Animal(
                "Piolí",
                Especie.AU,
                true,
                new DateTime(2023, 9, 19),
                new DateTime(2025, 1, 8),
                0.8, false,
                null, 0,
                new Xip("900000000000009", new DateTime(2025, 1, 8)));
            ctx.Animal.Add(pioli);

            var lucas = new Adoptant("12345678A", "Lucas", "Romero Pérez", true);
            ctx.Adoptant.Add(lucas);

            var jaime = new Adoptant("87654321Z", "Jaime", "Jimenez", false);
            ctx.Adoptant.Add(jaime);
            
            var kurapika = new Adoptant("19283746F", "Kurapika", "Kurta", true);
            ctx.Adoptant.Add(kurapika);

            var aina = new Voluntari("Aina", Torn.MATI);
            ctx.Voluntari.Add(aina);
            
            var joan = new Voluntari("Joan", Torn.TARDA);
            ctx.Voluntari.Add(joan);
            
            var nuria = new Voluntari("Núria", Torn.CAP_DE_SETMANA);
            ctx.Voluntari.Add(nuria);
            
            
            ctx.SaveChanges();
            
            Console.WriteLine("===| Animals Inserits |===");
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
            foreach (var animal in lucas.Animals)
            {
                Console.WriteLine(animal);
            }
            Console.WriteLine("===| Animal Retornat |===");
            Console.WriteLine(rocky);
            Console.WriteLine("");
            Console.WriteLine("===| Llistat adoptants |===");
            Console.WriteLine("===| (Min '0') |===");
            adoptantService.LlistatAdoptants(0);
            Console.WriteLine("===| (Min '2') |===");
            adoptantService.LlistatAdoptants(2);
            Console.WriteLine("");
            Console.WriteLine("===| Assignar Animals a Voluntaris |===");
            voluntariService.AssignarAnimal("jsdhfhdsjh", "fsjhdhjd");
            voluntariService.AssignarAnimal("Aina", "fsjhdhjd");
            voluntariService.AssignarAnimal("Aina", "900000000000007");
            voluntariService.AssignarAnimal("Aina", "900000000000001");
            voluntariService.AssignarAnimal("Aina", "900000000000001");
            voluntariService.AssignarAnimal("Aina", "900000000000002");
            voluntariService.AssignarAnimal("Aina", "900000000000003");
            voluntariService.AssignarAnimal("Aina", "900000000000004");
            voluntariService.AssignarAnimal("Aina", "900000000000005");
            voluntariService.AssignarAnimal("Joan", "900000000000005");
            Console.WriteLine("");
            Console.WriteLine("===| Desassignar Animals dels Voluntaris |===");
            voluntariService.DesassignarAnimal("jsdhfhdsjh", "fsjhdhjd");
            voluntariService.DesassignarAnimal("Aina", "fsjhdhjd");
            voluntariService.DesassignarAnimal("Aina", "900000000000005");
            voluntariService.DesassignarAnimal("Aina", "900000000000004");
            Console.WriteLine("===| Voluntaris de Rocky |===");
            foreach (var voluntari in rocky.Voluntaris)
            {
                Console.WriteLine(voluntari);
            }
            adoptantService.Adoptar("19283746F", "900000000000003");
            Console.WriteLine("===| Voluntaris de Rocky |===");
            foreach (var voluntari in rocky.Voluntaris)
            {
                Console.WriteLine(voluntari);
            }
            Console.WriteLine("");
            
            Console.WriteLine("===| Passejar |===");
            voluntariService.RegistrarPasseig("sdfsdf", "sfsdffsd", 10);
            voluntariService.RegistrarPasseig("Aina", "sdfdssd", 10);
            voluntariService.RegistrarPasseig("Joan", "900000000000007", 10);
            voluntariService.RegistrarPasseig("Joan", "900000000000005", 10);
            Console.WriteLine("Minuts Passeig d'Aina: "+aina.MinutsPasseig);
            voluntariService.RegistrarPasseig("Aina", "900000000000001", 10);
            Console.WriteLine("Minuts Passeig d'Aina: "+aina.MinutsPasseig);
            voluntariService.RegistrarPasseig("Aina", "900000000000001", 10);
            Console.WriteLine("Minuts Passeig d'Aina: "+aina.MinutsPasseig);
        }
    }
}