using System.Globalization;
using System.Xml;
using System.Xml.Serialization;
using designPatterns4AL2.models;
using Newtonsoft.Json.Linq;

namespace designPatterns4AL2;

public interface ICommandParserDownload
{
    void CreateFileFacture(PizzaCommand pizzaCommand);
}

public static class CommandParserFactoryDownload
{
    private const string SavePath = "D:/DP4AL2/designPatterns4AL2/files/out/";

    public static ICommandParserDownload CreateFactureParser(string typeFichier)
    {
        return typeFichier switch
        {
            "txt" => new TxtCommandParser(),
            "xml" => new XmlCommandParser(),
            "json" => new JsonCommandParser(),
            _ => throw new ArgumentException("Type de fichier non pris en charge : " + typeFichier)
        };
    }

    private static Facture CreateFacture(PizzaCommand pizzaCommand)
    {
        var factureIngredients = new List<FactureIngredients>();
        var facture = new Facture();
        if (pizzaCommand.Regina > 0)
        {
            var pizza = Pizza.Create("regina");
            factureIngredients.Add(new FactureIngredients(pizza.Nom, pizza.Ingredients));
            facture.Prix += pizza.Prix * pizzaCommand.Regina;
        }

        if (pizzaCommand.QuatreSaisons > 0)
        {
            var pizza = Pizza.Create("quatreSaisons");
            factureIngredients.Add(new FactureIngredients(pizza.Nom, pizza.Ingredients));
            facture.Prix += pizza.Prix * pizzaCommand.QuatreSaisons;
        }

        if (pizzaCommand.Vegetarienne > 0)
        {
            var pizza = Pizza.Create("vegetarienne");
            factureIngredients.Add(new FactureIngredients(pizza.Nom, pizza.Ingredients));
            facture.Prix += pizza.Prix * pizzaCommand.Vegetarienne;
        }

        if (pizzaCommand.Customs.Count() > 0)
        {
            foreach (var pizza in pizzaCommand.Customs)
            {
                factureIngredients.Add(new FactureIngredients(pizza.Key.Nom, pizza.Key.Ingredients));
                facture.Prix += pizza.Key.Prix;
            }
        }

        facture.Factures = factureIngredients;
        return facture;
    }

    private class JsonCommandParser : ICommandParserDownload
    {
        public void CreateFileFacture(PizzaCommand pizzaCommand)
        {
            var jsonObject = JObject.FromObject(CreateFacture(pizzaCommand));
            var facturesArray = (JArray)jsonObject["Factures"]!;
            MultiplateQuantiteByPizzaCommandCount(facturesArray, pizzaCommand);
            File.WriteAllText(SavePath + "facture.json", jsonObject.ToString());
        }

        private void MultiplateQuantiteByPizzaCommandCount(JArray facturesArray, PizzaCommand pizzaCommand)
        {
            var pizzaQuantities = new Dictionary<string, int>
            {
                { "Regina", pizzaCommand.Regina },
                { "Vegetarienne", pizzaCommand.Vegetarienne },
                { "QuatreSaisons", pizzaCommand.QuatreSaisons }
            };

            if (pizzaCommand.Customs.Count > 0)
            {
                foreach (var pizza in pizzaCommand.Customs)
                {
                    pizzaQuantities.Add(pizza.Key.Nom, pizza.Value);
                }
            }
            
            foreach (var factureIngredient in facturesArray)
            {
                var pizzaName = factureIngredient["PizzaName"]!.ToString();
                if (!pizzaQuantities.TryGetValue(pizzaName, out var quantity)) continue;
                var ingredientsArray = (JArray)factureIngredient["Ingredients"]!;
                foreach (var ingredient in ingredientsArray)
                {
                    ingredient["Quantite"] = (int)ingredient["Quantite"]! * quantity;
                }
            }
        }
    }

    private class XmlCommandParser : ICommandParserDownload
    {
        public void CreateFileFacture(PizzaCommand pizzaCommand)
        {
            var pizzaQuantities = new Dictionary<string, double>
            {
                { "Regina", pizzaCommand.Regina },
                { "Vegetarienne", pizzaCommand.Vegetarienne },
                { "QuatreSaisons", pizzaCommand.QuatreSaisons }
            };

            if (pizzaCommand.Customs.Count > 0)
            {
                foreach(var pizza in pizzaCommand.Customs)
                {
                    pizzaQuantities.Add(pizza.Key.Nom, pizza.Value);
                }
            }

            var xsSubmit = new XmlSerializer(typeof(Facture));
            var xml = "";

            using (var sww = new StringWriter())
            {
                using (var writer = XmlWriter.Create(sww))
                {
                    xsSubmit.Serialize(writer, CreateFacture(pizzaCommand));
                    xml = sww.ToString(); // Your XML
                }
            }


            var xmlDoc = new XmlDocument();
            xmlDoc.LoadXml(xml);
            var facture = xmlDoc.GetElementsByTagName("Factures");
            //Factures
            var factures = facture[0]!.ChildNodes;
            Console.WriteLine("Facture" + factures.Count);
            foreach (XmlNode node in factures)
            {
                var pizzaName = node["PizzaName"]!.InnerText;
                var ingredients = node["Ingredients"];
                foreach (XmlNode ingredient in ingredients)
                {
                    if (!pizzaQuantities.TryGetValue(pizzaName, out var quantity)) continue;
                    var quantite = ingredient["Quantite"];
                    var quantiteDouble = Convert.ToDouble(quantite?.InnerText, CultureInfo.InvariantCulture);
                    quantiteDouble *= quantity;
                    quantite.InnerText = quantiteDouble.ToString();
                }
            }


            xmlDoc.Save(SavePath + "facture.xml");
        }
    }

    private class TxtCommandParser : ICommandParserDownload
    {
        private static double _total = 0;

        public void CreateFileFacture(PizzaCommand pizzaCommand)
        {
            _total = 0.0;
            var text = "Facture : \n";
            if (pizzaCommand.Regina > 0)
            {
                text += DisplayFactureSection(pizzaCommand.Regina, Pizza.Create("regina"));
            }

            if (pizzaCommand.QuatreSaisons > 0)
            {
                text += DisplayFactureSection(pizzaCommand.QuatreSaisons, Pizza.Create("4saisons"));
            }

            if (pizzaCommand.Vegetarienne > 0)
            {
                text += DisplayFactureSection(pizzaCommand.Vegetarienne, Pizza.Create("vegetarienne"));
            }

            if (pizzaCommand.Customs.Count > 0)
            {
                foreach (var custom in pizzaCommand.Customs)
                {
                    text += DisplayFactureSection(custom.Value, custom.Key);
                }
            }

            text += "Total : " + _total + "€";

//download the file to the path

            File.WriteAllText( SavePath + "facture.txt", text);
        }

        private static string DisplayFactureSection(int pizzaCount, Pizza pizza)
        {
            var pizzaName = pizza.Nom;
            //get the lenght of the longest pizza.Ingredients name
            var maxIngredientLength = pizza.Ingredients.Max(s => s.Name.Length);
            var text = pizzaCount + " " + pizzaName + " : " + pizzaCount + " * " + pizza.Prix + "€";
            _total += pizzaCount * pizza.Prix;
            foreach (var ingredient in pizza.Ingredients)
            {
                text += "\n" + ingredient.Name.PadRight(maxIngredientLength) + " " +
                        ingredient.Quantite * pizzaCount + " " + ingredient.Mesure;
            }

            text += "\n_________________________\n";
            return text;
        }
    }
}