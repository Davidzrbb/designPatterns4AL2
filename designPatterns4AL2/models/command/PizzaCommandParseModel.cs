using System.Xml.Serialization;

namespace designPatterns4AL2.models;

public class PizzaCommandParseModel
{
    public PizzaCommandParseModel(string name, int quantity)
    {
        Name = name;
        Quantity = quantity;
    }

    //oblige de faire un constructor vide pour le deserialize du XML
    public PizzaCommandParseModel()
    {
    }

    [XmlElement("name")] public string Name { get; set; }
    [XmlElement("quantity")] public int Quantity { get; set; }
}