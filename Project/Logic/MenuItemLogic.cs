public static class MenuItemLogic
{
    public static MenuItem GetByName(string name)
    {
        return MenuItemAccess.GetByName(name);
    }

    public static MenuItem GetById(int id)
    {
        return MenuItemAccess.GetById(id);
    }

    public static List<MenuItem> GetAllMenuItems()
    {
        return MenuItemAccess.GetAllMenuItems();
    }

    public static void UpdateMenuItem(MenuItem item)
    {
        MenuItemAccess.Update(item);
    }

    public static void DeleteMenuItem(MenuItem item)
    {
        MenuItemAccess.Delete(item);
    }

    public static void WriteMenuItem(MenuItem item)
    {
        var existingItems = MenuItemLogic.GetAllMenuItems();

        foreach (var existingItem in existingItems)
        {
            if (existingItem.Name.ToLower() == item.Name.ToLower())
            {
                throw new InvalidOperationException("A snack with this name already exists.");
            }
        }

        MenuItemAccess.Write(item);
    }



}