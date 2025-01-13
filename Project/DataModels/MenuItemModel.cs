public class MenuItem
{
    public string Name { get; set; }
    public decimal Price { get; set; }
    public bool Type { get; set; } // false for food, true for drinks

    public MenuItem() { }
    public MenuItem(string name, decimal price, bool type)
    {
        Name = name;
        Price = price;
        Type = type;
    }
}