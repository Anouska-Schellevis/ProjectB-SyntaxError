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

    public static void DeleteMenuItem(int id)
    {
        MenuItemAccess.Delete(id);
    }

    public static void WriteMenuItem(MenuItem item)
    {
        MenuItemAccess.Write(item);
    }


}