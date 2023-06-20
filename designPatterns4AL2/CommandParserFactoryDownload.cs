using System.Xml.Serialization;
using designPatterns4AL2.models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace designPatterns4AL2;

public interface ICommandParserDownload
{
    void CreateFileFacture(PizzaCommand pizzaCommand);
}

public static class CommandParserFactoryDownload
{
    public static ICommandParserDownload CreateFactureParser(string typeFichier)
    {
        return typeFichier switch
        {
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

        facture.Factures = factureIngredients;
        return facture;
    }

    private class JsonCommandParser : ICommandParserDownload
    {
        public void CreateFileFacture(PizzaCommand pizzaCommand)
        {
            var jsonFacture = "";
            var pizzaQuantities = new Dictionary<string, int>
            {
                { "Regina", pizzaCommand.Regina },
                { "Vegetarienne", pizzaCommand.Vegetarienne },
                { "QuatreSaisons", pizzaCommand.QuatreSaisons }
            };

            jsonFacture += JsonConvert.SerializeObject(CreateFacture(pizzaCommand));
            var jsonObject = JObject.Parse(jsonFacture);
            var facturesArray = (JArray)jsonObject["Factures"]!;

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

            var savePath =
                "/Users/davidzerbib/RiderProjects/designPatterns4AL2/designPatterns4AL2/files/out/facture.json";

            File.WriteAllText(savePath, jsonObject.ToString());
        }
    }
}