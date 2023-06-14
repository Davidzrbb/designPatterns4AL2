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
                return null;
        }
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