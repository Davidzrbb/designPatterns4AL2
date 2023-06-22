namespace designPatterns4AL2;

public class Ingredient
{
    public  String Name { get; set; }
    public  double Quantite { get; set; }
    public  String Mesure { get; set; }
    public Ingredient()
    {
        // Parameterless constructor
    }
    public Ingredient (String name, double quantite, String mesure)
    {
        Name = name;
        Quantite = quantite;
        Mesure = mesure;
    }
}