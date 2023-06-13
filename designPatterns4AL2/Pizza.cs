using designPatterns4AL2;

namespace designPatterns4AL2;

public abstract class Pizza
{
    public String Nom { get; set; }
    public double Prix { get; set; }
    public List<Ingredient> Ingredients { get; set; }

    public static Pizza Create(String pizzaName)
    {
        switch (pizzaName)
        {
            case "Régina":
                return Regina.GetInstance();
            case "4 Saisons":
                return QuatreSaisons.GetInstance();
            case "Végétarienne":
                return Vegetarienne.GetInstance();
            default:
                return null;
        }
    }
    private sealed class Regina : Pizza
    {
        private static Regina Instance;
    
        public static Regina GetInstance()
        {
            if (Instance == null)
            {
                Instance = new Regina();
            }
            return Instance;
        }
    
        private Regina()
        {
            Nom = "Régina";
            Prix = 8;
            Ingredients = new List<Ingredient>
            {
                new Ingredient("tomate", 150, "g"),
                new Ingredient("mozzarella", 125, "g"),
                new Ingredient("fromage râpé", 100, "g"),
                new Ingredient("jambon", 2, "tranches"),
                new Ingredient("champignons frais", 4, ""),
                new Ingredient("huile d'olive", 2, "cuillères à soupe")
            };
        }
    }
    private sealed class QuatreSaisons : Pizza
    {
        private static QuatreSaisons Instance;
    
        public static QuatreSaisons GetInstance()
        {
            if (Instance == null)
            {
                Instance = new QuatreSaisons();
            }
            return Instance;
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
        private static Vegetarienne Instance;
    
        public static Vegetarienne GetInstance()
        {
            if (Instance == null)
            {
                Instance = new Vegetarienne();
            }
            return Instance;
        }
    
        private Vegetarienne()
        {
            Nom = "Végétarienne";
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

/*private class Regina : Pizza
{
    private static Regina Instance;
    
    public static Regina GetInstance()
    {
        if (Instance == null)
        {
            Instance = new Regina();
        }
        return Instance;
    }
    
    private Regina()
    {
        Nom = "Régina";
        Prix = 8;
        Ingredients = new List<Ingredient>
        {
            new Ingredient("tomate", 150, "g"),
            new Ingredient("mozzarella", 125, "g"),
            new Ingredient("fromage râpé", 100, "g"),
            new Ingredient("jambon", 2, "tranches"),
            new Ingredient("champignons frais", 4, ""),
            new Ingredient("huile d'olive", 2, "cuillères à soupe")
        };
    }
}

public sealed class QuatreSaisons : Pizza
{
    private static QuatreSaisons Instance;
    
    public static QuatreSaisons GetInstance()
    {
        if (Instance == null)
        {
            Instance = new QuatreSaisons();
        }
        return Instance;
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

public sealed class Vegetarienne : Pizza
{
    private static Vegetarienne Instance;
    
    public static Vegetarienne GetInstance()
    {
        if (Instance == null)
        {
            Instance = new Vegetarienne();
        }
        return Instance;
    }
    
    private Vegetarienne()
    {
        Nom = "Végétarienne";
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
}*/