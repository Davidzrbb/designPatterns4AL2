namespace designPatterns4AL2;

[Flags]
public enum Pizza
{
    [PizzaAttribut("Régina", 8)] Pizza1 = 1,
    [PizzaAttribut("4 Saisons", 9)] Pizza2 = 2,
    [PizzaAttribut("Végétarienne", 7.50)] Pizza3 = 3

}

public class PizzaAttribut : Attribute
{
    public string Nom { get; private set; }
    public double Prix { get; private set; }

    public PizzaAttribut(string nom, double prix)
    {
        Nom = nom;
        Prix = prix;
    }
}