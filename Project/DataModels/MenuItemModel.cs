public class MenuItem
{
    public string OldName { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public bool Type { get; set; } // false for food, true for drinks

    public MenuItem(string name, decimal price, bool type)
    {
        OldName = name;
        Name = name;
        Price = price;
        Type = type;
    }
}