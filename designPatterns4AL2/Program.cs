namespace designPatterns4AL2;

public static class MainClass
{
    private static double _total = 0;

    public static void Main(string[] args)
    {
        var pizzaCommand = new PizzaCommand(0, 0, 0);
        Console.OutputEncoding = System.Text.Encoding.UTF8;
        while (true)
        {
            Console.WriteLine("\nBienvenue chez Pizza Yolo ! \n");
            Console.WriteLine("\nNous acceptons les fichier json, xml, text ou en ligne de commande !");
            Console.WriteLine("Veuillez choisir votre méthode de commande : ");
            Console.WriteLine("1 - Fichier");
            Console.WriteLine("2 - Ligne de commande");
            Console.Write("\nVotre choix : ");
            var choice = Console.ReadLine();
            switch (choice)
            {
                case "1":
                    Console.Write("\nChemin du fichier : ");
                    var path = Console.ReadLine();
                    if (path != null)
                        pizzaCommand = CommandParserFactoryUpload
                            .CreateCommandeParser(Path.GetExtension(path)).ParseCommand(path);
                    break;
                case "2":
                    pizzaCommand = CommandParserFactoryUpload
                        .CreateCommandeParser("cmd").ParseCommand("");
                    break;
                default:
                    Console.WriteLine("Choix invalide");
                    //continue permet de passer à la prochaine itération de la boucle while
                    continue;
            }

            if (pizzaCommand == null)
            {
                continue;
            }

            Console.Write("\nType de retour facture : json, txt, xml, cmd : ");
            var choiceOut = Console.ReadLine();
            if (choiceOut != null)
                CommandParserFactoryDownload
                    .CreateFactureParser(choiceOut).CreateFileFacture(pizzaCommand);
            //DisplayRecette(pizzaCommand);
        }
    }

    private static void CreateFacture(PizzaCommand pizzaCommand)
    {
        Console.WriteLine("\nFacture : ");
        _total = 0.0;

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

        Console.WriteLine("Prix total : " + _total + "€");
    }

    private static void DisplayFactureSection(int pizzaCount, Pizza pizza)
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

        Console.WriteLine("_________________________");
        Console.WriteLine(text);
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