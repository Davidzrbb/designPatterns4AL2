namespace designPatterns4AL2.models;

public class FactureIngredients
{
    public string PizzaName { get; set; }
    public List<Ingredient> Ingredients { get; set; } = new();

    public FactureIngredients(string pizzaName, List<Ingredient> ingredients)
    {
        PizzaName = pizzaName;
        Ingredients = ingredients;
    }
}