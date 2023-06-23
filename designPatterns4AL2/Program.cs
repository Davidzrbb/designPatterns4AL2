namespace designPatterns4AL2;

public static class MainClass
{
    private static double _total = 0;

    public static void Main(string[] args)
    {
        var pizzaCommand = null as PizzaCommand;
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
                    {
                        try
                        {
                            pizzaCommand = CommandParserFactoryUpload
                                .CreateCommandeParser(Path.GetExtension(path)).ParseCommand(path);
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e.Message);
                            continue;
                        }
                        
                    }

                    break;
                case "2":
                    pizzaCommand = CommandParserFactoryUpload
                        .CreateCommandeParser("cmd").ParseCommand("");
                    break;
                default:
                    Console.WriteLine("Choix invalide");
                    continue;
            }

            if (pizzaCommand == null)
            {
                continue;
            }

            Console.Write("\nType de retour facture : json, txt, xml : ");
            var choiceOut = Console.ReadLine();
            if (choiceOut != null)
                CommandParserFactoryDownload
                    .CreateFactureParser(choiceOut).CreateFileFacture(pizzaCommand);
            DisplayRecette(pizzaCommand);
        }
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

        if (pizzaCommand.Customs.Count > 0)
        {
            foreach (var custom in pizzaCommand.Customs)
            {
                DisplayRcetteSection(custom.Key);
            }
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