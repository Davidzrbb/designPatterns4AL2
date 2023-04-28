using designPatterns4AL2;

namespace designPatterns4AL2;


[Flags]
public enum Pizza
{
    [PizzaAttribut("Régina", 8, new string[]
    {
        "150g de tomates",
        "125g de mozzarella",
        "100 g de fromage râpé",
        "2 tranches de jambon",
        "4 champignons frais",
        "2 cuillères à soupe d'huile d'olive"
    })] Pizza1 = 1,
    [PizzaAttribut("4 Saisons", 9, new string[]
    {
        "150g de tomates",
        "125g de mozzarella",
        "2 tranches de jambon",
        "100g de champignons frais",
        "0,5 poivron",
        "1 poignée d’olives"
    } )] Pizza2 = 2,
    [PizzaAttribut("Végétarienne", 7.50, new string[]
    {
        "150g de tomates",
        "100g de mozzarella",
        "0,5 courgette",
        "1 poivron jaune",
        "6 tomates cerises",
        "quelques olives"
    } )] Pizza3 = 3

}

public class PizzaAttribut : Attribute
{
    public string Nom { get; private set; }
    public double Prix { get; private set; }
    
    public string[] Ingredients { get; private set; }

    public PizzaAttribut(string nom, double prix, string[] ingredients)
    {
        Nom = nom;
        Prix = prix;
        Ingredients = ingredients;
    }
}