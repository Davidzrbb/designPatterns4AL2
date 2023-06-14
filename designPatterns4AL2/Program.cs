namespace designPatterns4AL2;

public static class MainClass
{
    //variable global total 
    public static double Total = 0;

    public static void Main(string[] args)
    {
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        Console.WriteLine("\nBienvenue chez Pizza Yolo ! \n");
        Console.WriteLine(
            "Veuillez choisir votre pizza en précisant le nombre. Exemple : 3 Vegetarienne, 2 Regina, 2 4 Saisons\n");

        DisplayMenu();

        while (true)
        {
            // on boucle pour que le programme n'est pas de fin 
            TakeCommand();
        }
    }

    private static void DisplayMenu()
    {
        Console.WriteLine(Pizza.Create("regina").Nom + ", " + Pizza.Create("vegetarienne").Nom + ", " +
                          Pizza.Create("4saisons").Nom);
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

        var pizzaAndCountArray = command.ToLower().Split(',');
        for (var i = 0; i < pizzaAndCountArray.Length; i++)
        {
            if (pizzaAndCountArray[i].Contains("4 saisons"))
            {
                pizzaAndCountArray[i] = pizzaAndCountArray[i].Replace("4 saisons", "4saisons");
            }
        }

        var countRegina = 0;
        var countVegetarienne = 0;
        var countQuatreSaisons = 0;

        foreach (var pizzaAndCount in pizzaAndCountArray)
        {
            var nameOrCount = pizzaAndCount.Trim().Split(' ');
            if (nameOrCount.Length < 2)
            {
                Console.WriteLine("Commande incomplète");
                return;
            }

            switch (nameOrCount[1])
            {
                case "regina":
                    countRegina += int.Parse(nameOrCount[0]);
                    break;
                case "vegetarienne":
                    countVegetarienne += int.Parse(nameOrCount[0]);
                    break;
                case "4saisons":
                    countQuatreSaisons += int.Parse(nameOrCount[0]);
                    break;
                default:
                    Console.WriteLine("La commande n'est pas valide");
                    return;
            }
        }

        var pizzaCommand = new PizzaCommandBuilder().withRegina(countRegina).withVegetarienne(countVegetarienne)
            .withQuatreSaisons(countQuatreSaisons).Build();
        Console.WriteLine("Regina " + pizzaCommand.Regina
                                    + " Vegetarienne " + pizzaCommand.Vegetarienne
                                    + " 4 Saisons " + pizzaCommand.QuatreSaisons);


        CreateFacture(pizzaCommand);
        DisplayRecette(pizzaCommand);
    }

    private static void DisplayFactureSection(int pizzaCount, Pizza pizza)
    {
        var pizzaName = pizza.Nom;
        //get the lenght of the longest pizza.Ingredients name
        var maxIngredientLength = pizza.Ingredients.Max(s => s.Name.Length);
        var text = pizzaCount + " " + pizzaName + " : " + pizzaCount + " * " + pizza.Prix + "€";
        Total += pizzaCount * pizza.Prix;
        foreach (var ingredient in pizza.Ingredients)
        {
            text += "\n" + ingredient.Name.PadRight(maxIngredientLength) + " " +
                    ingredient.Quantite * pizzaCount + " " + ingredient.Mesure;
        }

        Console.WriteLine("_________________________");
        Console.WriteLine(text);
    }

    private static void CreateFacture(PizzaCommand pizzaCommand)
    {
        Console.WriteLine("\nFacture : ");
        Total = 0.0;

        if (pizzaCommand.Regina > 0)
        {
            DisplayFactureSection(pizzaCommand.Regina, Pizza.Create("regina"));
        }

        if (pizzaCommand.QuatreSaisons > 0)
        {
            DisplayFactureSection(pizzaCommand.QuatreSaisons, Pizza.Create("4saisons"));
        }

        if (pizzaCommand.Vegetarienne > 0)
        {
            DisplayFactureSection(pizzaCommand.Vegetarienne, Pizza.Create("vegetarienne"));
        }

        Console.WriteLine("Prix total : " + Total + "€");
    }

    private static void DisplayRecette(PizzaCommand pizzaCommand)
    {
        Console.WriteLine("_________________________");
        if (pizzaCommand.Regina > 0)
        {
            DisplayRcetteSection(Pizza.Create("regina"));
        }

        if (pizzaCommand.QuatreSaisons > 0)
        {
            DisplayRcetteSection(Pizza.Create("4saisons"));
        }

        if (pizzaCommand.Vegetarienne > 0)
        {
            DisplayRcetteSection(Pizza.Create("vegetarienne"));
        }
    }

    private static void DisplayRcetteSection(Pizza pizza)
    {
        var pizzaName = pizza.Nom;

        Console.WriteLine("\nRecette de la pizza " + pizzaName + " : \n");
        Console.WriteLine("Préparer la pâte");
        foreach (var ingredient in pizza.Ingredients)
        {
            Console.WriteLine(
                "Ajouter " + ingredient.Name + " " + ingredient.Quantite + " " + ingredient.Mesure
            );
        }

        Console.WriteLine("Cuire la pizza");
    }
}