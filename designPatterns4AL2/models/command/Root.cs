using System.Xml.Serialization;

namespace designPatterns4AL2.models;

[XmlRoot("command")]
public class Root
{
    public Root(List<PizzaCommandParseModel> pizzas)
    {
        Pizzas = pizzas;
    }

    private Root()
    {
    }

    [XmlElement("pizza")] public List<PizzaCommandParseModel> Pizzas { get; set; }
}