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
        TakeCommand();
        /*while (true)
        {
            // on boucle pour que le programme n'est pas de fin 
            //TakeCommand();

        }*/
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
        /*
    DisplayRecette(pizzaCommandsList);*/
    }

    private static void DisplayFactureSection(int pizzaCount, Pizza pizza)
    {
        var pizzaName = pizza.Nom;
        var maxLengthPizza = 12;
        var text = pizzaCount + " " + pizzaName + " : " + pizzaCount + " * " + pizza.Prix + "€";
        Total += pizzaCount * pizza.Prix;
        foreach (var ingredient in pizza.Ingredients)
        {
            text += "\n" + ingredient.Name.PadRight(maxLengthPizza) + " " + ingredient.Quantite * pizzaCount + " " + ingredient.Mesure;
        }
        Console.WriteLine("_________________________");
        Console.WriteLine(text);
        
        /*private static void DisplayFacture(List<PizzaCommand> pizzaCommands) {
            Console.WriteLine("\nFacture : ");
            var total = 0.0;
        
            foreach (var pizzaCmd in pizzaCommands) {
                var pizzaName = pizzaCmd.Name.Trim();
                var pizzaCount = pizzaCmd.Count;
                var maxLengthPizza = pizzaCommands.Max(s => s.Name.Length);
                foreach (Pizza pizza in Enum.GetValues(typeof(Pizza))) {
                    var fieldInfo = typeof(Pizza).GetField(pizza.ToString());
                    var attribut = (PizzaAttribut)Attribute.GetCustomAttribute(fieldInfo, typeof(PizzaAttribut));
                
                    if (attribut.Nom.Equals(pizzaName)) {
                        Console.WriteLine("_________________________");
                        Console.WriteLine(pizzaCount + " " + pizzaName.PadRight(maxLengthPizza) + " : " + pizzaCount + " * " + attribut.Prix + "€");
                        total += pizzaCount * attribut.Prix;   
                        var maxLengthIngredient = attribut.Ingredients.Max(s => s.Nom.Length);
                        foreach (var ingredient in attribut.Ingredients) {
                            var price = ingredient.Quantite != 0 ? (ingredient.Quantite * pizzaCount).ToString() : "";
                            Console.WriteLine(ingredient.Nom.PadRight(maxLengthIngredient) + " " + price +
                                              " " + ingredient.Unite);
                        }
                    }
                }
            }
            Console.WriteLine("Prix total : " + total + "€");
        }*/
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
*/