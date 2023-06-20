namespace designPatterns4AL2.models;

public class Facture
{
    public List<FactureIngredients> Factures { get; set; } = new();
    public double Prix { get; set; } = 0;

    public Facture()
    {
    }

    public Facture(List<FactureIngredients> factures, double prix)
    {
        Factures = factures;
        Prix = prix;
    }
}