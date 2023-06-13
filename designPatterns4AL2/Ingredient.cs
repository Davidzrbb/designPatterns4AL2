namespace designPatterns4AL2;

public class Ingredient
{
    public static String Name { get; set; }
    public static double Quantite { get; set; }
    public static String Mesure { get; set; }

    public Ingredient (String name, double quantite, String mesure)
    {
        Name = name;
        Quantite = quantite;
        Mesure = mesure;
    }
}