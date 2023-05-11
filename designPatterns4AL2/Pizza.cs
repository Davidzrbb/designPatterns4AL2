using designPatterns4AL2;

namespace designPatterns4AL2;


[Flags]
public enum Pizza {
    
    [PizzaAttribut("Régina", 8, new object[] {
        new object[]{"tomate", (double) 150, "g"},
        new object[]{"mozzarella", (double) 125, "g"},
        new object[]{"fromage râpé", (double) 100, "g"},
        new object[]{"jambon", (double) 2, "tranches"},
        new object[]{"champignons frais", (double) 4, ""},
        new object[]{"huile d'olive", (double) 2, "cuillères à soupe"}
    })] Pizza1 = 1,
    [PizzaAttribut("4 Saisons", 9, new object[] {
        new object[]{"tomate", (double) 150, "g"},
        new object[]{"mozzarella", (double) 125, "g"},
        new object[]{"jambon", (double) 2, "tranches"},
        new object[]{"champignons frais", (double) 100, "g"},
        new object[]{"poivron", 0.5, ""},
        new object[]{"olive", (double) 1, "poignée"}
    } )] Pizza2 = 2,
    [PizzaAttribut("Végétarienne", 7.50, new object[] {
        new object[]{"tomate", (double) 150, "g"},
        new object[]{"mozzarella", (double) 100, "g"},
        new object[]{"courgette", 0.5, ""},
        new object[]{"poivron jaune", (double) 1, ""},
        new object[]{"tomates cerises", (double) 6, ""},
        new object[]{"quelques olives", (double) 0, ""}
    } )] Pizza3 = 3
    
}

public class PizzaAttribut : Attribute {
    public string Nom { get; private set; }
    public double Prix { get; private set; }
    
    public List<Ingredient> Ingredients { get; private set; }

    public PizzaAttribut(string nom, double prix, object[] ingredients) {
        Nom = nom;
        Prix = prix;
        Ingredients = new List<Ingredient>();
        foreach (object[] ingredient in ingredients) {
            Ingredients.Add(new Ingredient((string) ingredient[0], (double) ingredient[1], (string) ingredient[2]));
        }
    }
}

public class Ingredient {
    public string Nom { get; set; }
    public double Quantite { get; set; }
    public string Unite { get; set; }

    public Ingredient(string nom, double quantite, string unite) {
        Nom = nom;
        Quantite = quantite;
        Unite = unite;
    }
}