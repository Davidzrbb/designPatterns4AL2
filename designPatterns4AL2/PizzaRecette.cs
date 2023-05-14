namespace designPatterns4AL2;

public class PizzaRecette {
    // Propriétés de la recette
    public string Nom { get; set; }
    public string Pate { get; set; }
    public Ingredient Sauce { get; set; }
    public List<Ingredient> Ingrédients { get; set; }
    
    public PizzaRecette(string name, string pate, Ingredient sauce, List<Ingredient> ingredients) {
        Nom = name;
        Pate = pate;
        Sauce = sauce;
        Ingrédients = ingredients;
    }
}