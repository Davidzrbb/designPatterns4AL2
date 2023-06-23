using System.Xml.Serialization;
using designPatterns4AL2.models;
using Newtonsoft.Json;

namespace designPatterns4AL2;

public interface ICommandeParser
{
    PizzaCommand? ParseCommand(string path);
}

public class CommandParserFactoryUpload

{
    public static ICommandeParser CreateCommandeParser(string typeFichier)
    {
        return typeFichier switch
        {
            "cmd" => new CommandLineParser(),
            ".txt" => new TextCommandParser(),
            ".xml" => new XmlCommandParser(),
            ".json" => new JsonCommandParser(),
            _ => throw new ArgumentException("Type de fichier non pris en charge : " + typeFichier)
        };
    }

    private class JsonCommandParser : ICommandeParser
    {
        public PizzaCommand? ParseCommand(string path)
        {
            var contenuFichier = File.ReadAllText("../../../files/in/" + path);
            var pizzaCommandParseModel = JsonConvert.DeserializeObject<List<PizzaCommandParseModel>>(contenuFichier);
            return countPizza(pizzaCommandParseModel!);
        }
    }

    private class XmlCommandParser : ICommandeParser
    {
        public PizzaCommand? ParseCommand(string path)
        {
            var contenuFichier = File.ReadAllText("../../../files/in/" + path);
            var serializer = new XmlSerializer(typeof(Root));
            var reader = new StringReader(contenuFichier);
            var pizzaCommandParseModel = (Root)serializer.Deserialize(reader)!;
            return countPizza(pizzaCommandParseModel.Pizzas);
        }
    }

    private class TextCommandParser : ICommandeParser
    {
        public PizzaCommand? ParseCommand(string path)
        {
            var contenuFichier = File.ReadAllText("../../../files/in/" + path);
            var pizzaCommandParseModelArray = new List<PizzaCommandParseModel>();
            foreach (var line in contenuFichier.Split("\n"))
            {
                var pizzaCommandParseModel = new PizzaCommandParseModel();
                var split = line.Split(" ");
                pizzaCommandParseModel.Quantity = int.Parse(split[0].Trim());
                pizzaCommandParseModel.Name = split[1].Trim();
                pizzaCommandParseModelArray.Add(pizzaCommandParseModel);
            }

            return countPizza(pizzaCommandParseModelArray);
        }
    }

    private class CommandLineParser : ICommandeParser
    {
        public PizzaCommand? ParseCommand(string path = "")
        {
            Console.WriteLine(
                "Veuillez choisir votre pizza en précisant le nombre. Exemple : 3 Vegetarienne, 2 Regina, 2 4 Saisons\n");
            DisplayMenu();
            var listPizzaCommandParseModel = TakeCommand();
            return listPizzaCommandParseModel == null ? null : countPizza(listPizzaCommandParseModel);
        }

        private static void DisplayMenu()
        {
            Console.WriteLine(Pizza.Create("regina").Nom + ", " + Pizza.Create("vegetarienne").Nom + ", " +
                              Pizza.Create("4saisons").Nom + " ou autre nom pour une pizza personnalisée");
        }

        private static List<PizzaCommandParseModel>? TakeCommand()
        {
            Console.Write("\nVotre commande : ");
            var command = Console.ReadLine();
            if (command!.Trim() == "")
            {
                Console.WriteLine("Votre commande est vide");
                return null;
            }

            var pizzaAndCountArray = command.ToLower().Split(',');
            for (var i = 0; i < pizzaAndCountArray.Length; i++)
            {
                if (pizzaAndCountArray[i].Contains("4 saisons"))
                {
                    pizzaAndCountArray[i] = pizzaAndCountArray[i].Replace("4 saisons", "4saisons");
                }
            }

            var pizzaCommandParseModelArray = new List<PizzaCommandParseModel>();
            foreach (var pizzaAndCount in pizzaAndCountArray)
            {
                var nameOrCount = pizzaAndCount.Trim().Split(' ');
                if (nameOrCount.Length < 2)
                {
                    Console.WriteLine("Commande incomplète");
                    return null;
                }

                pizzaCommandParseModelArray.Add(new PizzaCommandParseModel(nameOrCount[1], int.Parse(nameOrCount[0])));
            }

            return pizzaCommandParseModelArray;
        }
    }

    private static PizzaCommand? countPizza(List<PizzaCommandParseModel> pizzaCommandParseModel)
    {
        var countRegina = 0;
        var countVegetarienne = 0;
        var countQuatreSaisons = 0;
        var customs = new List<PizzaCommandParseModel>();
        foreach (var pizza in pizzaCommandParseModel!)
        {
            switch (pizza.Name)
            {
                case "regina":
                    countRegina += pizza.Quantity;
                    break;
                case "vegetarienne":
                    countVegetarienne += pizza.Quantity;
                    break;
                case "4saisons":
                    countQuatreSaisons += pizza.Quantity;
                    break;
                default:
                    var found = false;
                    foreach (var custom in customs.Where(custom => custom.Name == pizza.Name))
                    {
                        custom.Quantity += pizza.Quantity;
                        found = true;
                    }

                    if (!found)
                    {
                        customs.Add(pizza);
                    }
                    
                    break;
            }
        }

        Console.WriteLine("Regina : " + countRegina
                                      + "\nVegetarienne : " + countVegetarienne
                                      + "\n4 Saisons : " + countQuatreSaisons);
        foreach (var pizza in customs)
        {
            Console.WriteLine(pizza.Name + " : " + pizza.Quantity);
        }
        
        return new PizzaCommandBuilder().WithRegina(countRegina).WithVegetarienne(countVegetarienne)
            .WithQuatreSaisons(countQuatreSaisons).WithCustom(customs).Build();
    }
}