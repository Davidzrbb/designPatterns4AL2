using System.Runtime.CompilerServices;
using System.Text.Json.Serialization.Metadata;
using designPatterns4AL2;

namespace designPatterns4AL2;

public abstract class Pizza
{
    public string Nom { get; set; }
    public double Prix { get; set; }
    public List<Ingredient> Ingredients { get; set; }

    public static Pizza Create(String pizzaName)
    {
        switch (pizzaName)
        {
            case "regina":
                return Regina.GetInstance();
            case "4saisons":
                return QuatreSaisons.GetInstance();
            case "vegetarienne":
                return Vegetarienne.GetInstance();
            default:
                return CreateCustomPizza(pizzaName);
        }
    }

    private static CustomPizza CreateCustomPizza(string pizzaName)
    {
        var ingredientsChoisis = new List<Ingredient>();
        var ingredientsCount = 0;
        
        var ingredientsDispo = new List<string>();
        ingredientsDispo.Add("tomate (100 g)");
        ingredientsDispo.Add("mozzarella (25 g)");
        ingredientsDispo.Add("courgette (0.5)");
        ingredientsDispo.Add("poivron jaune (1)");
        ingredientsDispo.Add("tomate cerise (4)");
        ingredientsDispo.Add("olives (4)");
        ingredientsDispo.Add("jambon (2 tranches)");
        ingredientsDispo.Add("champignons frais (4)");
        ingredientsDispo.Add("huile d'olive (2 cuillère à soupe)");
        ingredientsDispo.Add("fromage rapé (100 g)");
        
        Console.WriteLine("\nVeuillez choisir vos ingredients pour la pizza '" + pizzaName + "'");
        Console.WriteLine("Ingredients disponible : ");

        for (int i = 0; i < ingredientsDispo.Count; i++)
        {
            Console.WriteLine((i + 1) + " - " + ingredientsDispo[i]);
        }
        
        Console.WriteLine("0 - Fin");

        var choice = Console.ReadLine();

        while (choice != "0")
        {
            if (choice.Length < 1 || int.Parse(choice) < 0 || int.Parse(choice) > ingredientsDispo.Count)
            {
                Console.WriteLine("Veuillez entrer un chiffre entre 0 et " + ingredientsDispo.Count);
            }
            else
            {
                if (choice == "0")
                {
                    break;
                }
                
                switch (choice)
                {
                    case "1":
                        ingredientsChoisis = CheckIfContain(ingredientsChoisis, "tomate", 100, "g");
                        ingredientsCount++;
                        break;
                    case "2":
                        ingredientsChoisis = CheckIfContain(ingredientsChoisis, "mozzarella", 25, "g");
                        ingredientsCount++;
                        break;
                    case "3":
                        ingredientsChoisis = CheckIfContain(ingredientsChoisis, "courgette", 0.5, "");
                        ingredientsCount++;
                        break;
                    case "4":
                        ingredientsChoisis = CheckIfContain(ingredientsChoisis, "poivron jaune", 1, "");
                        ingredientsCount++;
                        break;
                    case "5":
                        ingredientsChoisis = CheckIfContain(ingredientsChoisis, "tomate cerise", 4, "");
                        ingredientsCount++;
                        break;
                    case "6":
                        ingredientsChoisis = CheckIfContain(ingredientsChoisis, "olives", 4, "");
                        ingredientsCount++;
                        break;
                    case "7":
                        ingredientsChoisis = CheckIfContain(ingredientsChoisis, "jambon", 2, "tranches");
                        ingredientsCount++;
                        break;
                    case "8":
                        ingredientsChoisis = CheckIfContain(ingredientsChoisis, "champignons frais", 4, "");
                        ingredientsCount++;
                        break;
                    case "9":
                        ingredientsChoisis = CheckIfContain(ingredientsChoisis, "huile d'olive", 2, "cuillères à soupe");
                        ingredientsCount++;
                        break;
                    case"10":
                        ingredientsChoisis = CheckIfContain(ingredientsChoisis, "fromage rapé", 100, "g");
                        ingredientsCount++;
                        break;
                }
            }
            
            choice = Console.ReadLine();

            
        }

        return new CustomPizza(pizzaName, ingredientsCount, ingredientsChoisis);
    }

    private static List<Ingredient> CheckIfContain(List<Ingredient> ingredients, string name, double quantite, string unite)
    {

        var found = false;
        
        foreach (var ingredient in ingredients)
        {
            if (ingredient.Name == name)
            {
                ingredient.Quantite += quantite;
                found = true;
            }
            
        }

        if (!found)
        {
            ingredients.Add(new Ingredient(name, quantite, unite));
        }
        
        return ingredients;
            
    }

    private sealed class Regina : Pizza
    {
        private static Regina? _instance;

        public static Regina GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Regina();
            }

            return _instance;
        }

        private Regina()
        {
            Nom = "Regina";
            Prix = 8;
            Ingredients = new List<Ingredient>
            {
                new Ingredient("tomate", 150, "g"),
                new Ingredient("mozzarella", 125, "g"),
                new Ingredient("fromage râpe", 100, "g"),
                new Ingredient("jambon", 2, "tranches"),
                new Ingredient("champignons frais", 4, ""),
                new Ingredient("huile d'olive", 2, "cuillères à soupe")
            };
        }
    }

    private sealed class CustomPizza : Pizza
    {

        public CustomPizza(String name, double prix, List<Ingredient> ingredients)
        {
            Nom = name;
            Prix = prix;
            Ingredients = ingredients;
        }
        
    }

    private sealed class QuatreSaisons : Pizza
    {
        private static QuatreSaisons? _instance;

        public static QuatreSaisons GetInstance()
        {
            if (_instance == null)
            {
                _instance = new QuatreSaisons();
            }

            return _instance;
        }

        private QuatreSaisons()
        {
            Nom = "4 Saisons";
            Prix = 9;
            Ingredients = new List<Ingredient>
            {
                new Ingredient("tomate", 150, "g"),
                new Ingredient("mozzarella", 125, "g"),
                new Ingredient("jambon", 2, "tranches"),
                new Ingredient("champignons frais", 4, ""),
                new Ingredient("huile d'olive", 2, "cuillères à soupe")
            };
        }
    }

    private sealed class Vegetarienne : Pizza
    {
        private static Vegetarienne? _instance;

        public static Vegetarienne GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Vegetarienne();
            }

            return _instance;
        }

        private Vegetarienne()
        {
            Nom = "Vegetarienne";
            Prix = 7.50;
            Ingredients = new List<Ingredient>
            {
                new Ingredient("tomate", 150, "g"),
                new Ingredient("mozzarella", 100, "g"),
                new Ingredient("courgette", 0.5, ""),
                new Ingredient("poivron jaune", 1, ""),
                new Ingredient("tomates cerises", 6, ""),
                new Ingredient("quelques olives", 0, "")
            };
        }
    }
}

public class PizzaBuilder
{
    private string? _name;
    private int? _tomate;
    private int? _mozzarella;
    private int? _courgette;
    private int? _poivronJaune;
    private int? _tomatesCerises;
    private int? _olives;
    private int? _jambon;
    private int? _champignonsFrais;
    private int? _huileDOlive;
    private int? _fromageRape;

    public PizzaBuilder WithName(String name)
    {
        _name = name;
        return this;
    }
    
    public PizzaBuilder WithTomate(int tomate)
    {
        _tomate = tomate;
        return this;
    }
    
    public PizzaBuilder WithMozzarella(int mozzarella)
    {
        _mozzarella = mozzarella;
        return this;
    }
    
    public PizzaBuilder WithCourgette(int courgette)
    {
        _courgette = courgette;
        return this;
    }
    
    public PizzaBuilder WithPoivronJaune(int poivronJaune)
    {
        _poivronJaune = poivronJaune;
        return this;
    }
    
    public PizzaBuilder WithTomatesCerises(int tomatesCerises)
    {
        _tomatesCerises = tomatesCerises;
        return this;
    }
    
    public PizzaBuilder WithOlives(int olives)
    {
        _olives = olives;
        return this;
    }
    
    public PizzaBuilder WithJambon(int jambon)
    {
        _jambon = jambon;
        return this;
    }
    
    public PizzaBuilder WithChampignonsFrais(int champignonsFrais)
    {
        _champignonsFrais = champignonsFrais;
        return this;
    }
    
    public PizzaBuilder WithHuileDOlive(int huileDOlive)
    {
        _huileDOlive = huileDOlive;
        return this;
    }
    
    public PizzaBuilder WithFromageRape(int fromageRape)
    {
        _fromageRape = fromageRape;
        return this;
    }
    
    public Pizza? Build()
    {
        if (_name == null && !_tomate.HasValue && ! _mozzarella.HasValue && ! _courgette.HasValue && ! _poivronJaune.HasValue && ! _tomatesCerises.HasValue && ! _olives.HasValue && ! _jambon.HasValue && ! _champignonsFrais.HasValue && ! _huileDOlive.HasValue && ! _fromageRape.HasValue)
        {
            throw new Exception("Aucune ingrédient n'a été ajouté à la pizza");
        }

        var name = _name;



        return Pizza.Create(_name);
    }

}
