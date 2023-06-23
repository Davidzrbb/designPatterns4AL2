using designPatterns4AL2.models;

namespace designPatterns4AL2;

public class PizzaCommandBuilder
{
    private int? _regina;
    private int? _quatreSaisons;
    private int? _vegetarienne;
    private List<PizzaCommandParseModel>? _customs;

    public PizzaCommandBuilder WithRegina(int regina)
    {
        _regina = regina;
        return this;
    }
    
    public PizzaCommandBuilder WithQuatreSaisons(int quatreSaisons)
    {
        _quatreSaisons = quatreSaisons;
        return this;
    } 
    
    public PizzaCommandBuilder WithVegetarienne(int vegetarienne)
    {
        _vegetarienne = vegetarienne;
        return this;
    }
    
    public PizzaCommandBuilder WithCustom(List<PizzaCommandParseModel> customs)
    {
        _customs = customs;
        return this;
    }
    
    public PizzaCommand? Build()
    {
        if (!_regina.HasValue && !_quatreSaisons.HasValue && !_vegetarienne.HasValue && _customs == null)
            throw new Exception("Aucune pizza n'a été commandée");
        return new PizzaCommand(_regina, _quatreSaisons, _vegetarienne, _customs);
    }
}

public class PizzaCommand
{
    public int Regina { get; }
    public int QuatreSaisons { get; }
    public int Vegetarienne { get; }
    public Dictionary<Pizza, int> Customs { get; }
    

    public PizzaCommand(int? regina, int? quatreSaisons, int? vegetarienne, List<PizzaCommandParseModel>? customs)
    {   
        Regina = !regina.HasValue ? 0 : regina.Value;
        QuatreSaisons = !quatreSaisons.HasValue ? 0 : quatreSaisons.Value;
        Vegetarienne = !vegetarienne.HasValue ? 0 : vegetarienne.Value;
        
        Customs = new Dictionary<Pizza, int>();

        foreach (var custom in customs)
        {
            Customs.Add(Pizza.Create(custom.Name), custom.Quantity);
        }
    }
}