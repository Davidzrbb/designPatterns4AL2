namespace designPatterns4AL2;

public static class MainClass
{
    public static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.WriteLine("\nBienvenue chez Pizza Yolo ! \n");
        Console.WriteLine("Veuillez choisir votre pizza en précisant le nombre. Exemple : 4 Régina\n");

        DisplayMenu();
        CreateFacture(new PizzaCommandBuilder().withRegina(1).withVegetarienne(2).withQuatreSaisons(0).Build());
        /*while (true)
        {
            // on boucle pour que le programme n'est pas de fin 
            //TakeCommand();

        }*/
    }

    private static void DisplayMenu()
    {
        Console.WriteLine(Pizza.Create("Régina").Nom + ", "+Pizza.Create("Végétarienne").Nom+", "+Pizza.Create("4 Saisons").Nom);

    }

    private static void DisplayFactureSection(int pizzaCount, Pizza pizza)
    {
        var pizzaName = pizza.Nom;
        var maxLengthPizza = 12;
        var text = pizzaCount + " " + pizzaName + " : " + pizzaCount + " * " + pizza.Prix + "€";
        foreach (Ingredient ingredient in pizza.Ingredients)
        {
            text += "\n" + ingredient.Name + " " + ingredient.Quantite*pizzaCount + " " + ingredient.Mesure;
        }

        Console.WriteLine(text);
    }

    private static void CreateFacture(PizzaCommand pizzaCommand)
    {
        Console.WriteLine("\nFacture : ");
        var total = 0.0;

        if (pizzaCommand.Regina > 0)
        {
            DisplayFactureSection(pizzaCommand.Regina, Pizza.Create("Régina"));
        }

        if (pizzaCommand.QuatreSaisons > 0)
        {
            DisplayFactureSection(pizzaCommand.QuatreSaisons, Pizza.Create("4 Saisons"));
        }

        if (pizzaCommand.Vegetarienne > 0)
        {
            DisplayFactureSection(pizzaCommand.Vegetarienne, Pizza.Create("Végétarienne"));
        }
        
        Console.WriteLine("Prix total : " + total + "€");
    }
}

// Méthode pour afficher la recette
    /*public static void DisplayRecette(List<PizzaCommand> pizzaCommands)
    {
        foreach (var pizzaCommand in pizzaCommands)
        {
            var pizzaName = pizzaCommand.Name.Trim();
            var pizzaCount = pizzaCommand.Count;
            foreach (Pizza pizza in Enum.GetValues(typeof(Pizza)))
            {
                var fieldInfo = typeof(Pizza).GetField(pizza.ToString());
                var attribut = (PizzaAttribut)Attribute.GetCustomAttribute(fieldInfo, typeof(PizzaAttribut));
                if (attribut.Nom.Equals(pizzaName))
                {
                    Console.WriteLine("\nRecette de la pizza " + pizzaName + " : \n");
                    var pate = (pizzaName == "Régina") ? "pâte épaisse" : "pâte fine";
                    Console.WriteLine("Déroulez une " + pate);
                    Console.WriteLine("Ajoutez " + attribut.Ingredients[0].Quantite + attribut.Ingredients[0].Unite +
                                      " de sauce " + attribut.Ingredients[0].Nom);
                    for (int i = 1; i < attribut.Ingredients.Count; i++)
                    {
                        Console.WriteLine("Ajoutez " + attribut.Ingredients[i].Quantite + " " +
                                          attribut.Ingredients[i].Unite + " de " + attribut.Ingredients[i].Nom);
                    }

                    Console.WriteLine("Enfournez pendant 15 minutes à 210°C");
                }
            }
        }
    }

    private static void TakeCommand()
    {
        Console.Write("\nVotre commande : ");
        var command = Console.ReadLine();
        if (command!.Trim() == "")
        {
            Console.WriteLine("Votre commande est vide");
            return;
        }

        var pizzaAndCountArray = command!.Split(',');
        var pizzaCommandsList = new List<PizzaCommand>();
        foreach (var pizzaAndCount in pizzaAndCountArray)
        {
            // Extraire le nombre et le nom de chaque pizza
            var nameOrCount = pizzaAndCount.Trim().Split(' ');
            var pizzaCommand = new PizzaCommand();
            try
            {
                pizzaCommand.Count = int.Parse(nameOrCount[0]);
            }
            catch (Exception e)
            {
                Console.WriteLine("Le nombre de pizza n'est pas valide");
                return;
            }

            if (nameOrCount.Length < 2)
            {
                Console.WriteLine("Commande incomplète");
                return;
            }

            for (var i = 0; i < nameOrCount.Length; i++)
            {
                if (i != 0)
                {
                    pizzaCommand.Name += nameOrCount[i] + " ";
                }
            }

            Console.WriteLine(pizzaCommand.Name);
            if (!checkNamePizza(pizzaCommand.Name.Trim()))
            {
                return;
            }

            pizzaCommandsList.Add(pizzaCommand);
        }

        pizzaCommandsList = AddCountSamePizza(pizzaCommandsList);
        // Afficher le nombre et le nom de chaque pizza
        foreach (var pizzaCommandList in pizzaCommandsList)
        {
            Console.WriteLine(pizzaCommandList.Count + ": " + pizzaCommandList.Name);
        }

        CreateFacture(pizzaCommandsList);
        DisplayRecette(pizzaCommandsList);
    }

    private static List<PizzaCommand> AddCountSamePizza(List<PizzaCommand> pizzaCommands)
    {
        // on crée une liste de pizzaCommand qui va contenir les pizzaCommand sans doublons et on additionne les count des meme pizza 
        var pizzaCommandsWithoutDuplicate = new List<PizzaCommand>();
        foreach (var pizzaCommand in pizzaCommands)
        {
            var pizzaCommandWithoutDuplicate = pizzaCommandsWithoutDuplicate.Find(p => p.Name == pizzaCommand.Name);
            if (pizzaCommandWithoutDuplicate != null)
            {
                pizzaCommandWithoutDuplicate.Count += pizzaCommand.Count;
            }
            else
            {
                pizzaCommandsWithoutDuplicate.Add(pizzaCommand);
            }
        }

        return pizzaCommandsWithoutDuplicate;
    }

    private static Boolean checkNamePizza(string pizzaName)
    {
        bool pizzaExists = false;
        foreach (var value in Enum.GetValues(typeof(Pizza)))
        {
            var memberInfo = typeof(Pizza).GetMember(value.ToString());
            var attribut = memberInfo[0].GetCustomAttributes(typeof(PizzaAttribut), false)
                .SingleOrDefault() as PizzaAttribut;

            if (attribut != null && attribut.Nom.Equals(pizzaName, StringComparison.OrdinalIgnoreCase))
            {
                pizzaExists = true;
                break;
            }
        }

        if (!pizzaExists)
        {
            Console.WriteLine("La pizza " + pizzaName + " n'existe pas");
            return false;
        }

        return true;
    }
}*/