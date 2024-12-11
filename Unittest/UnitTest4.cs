using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnittestSnacks;

[TestClass]
public class SnackTest
{
    [TestInitialize]
    public void TestInitialize()
    {
        MenuItemAccess.ClearMenuItems();
    }

    [DataRow("Popcorn", 2.50, false)]
    [DataRow("Soda", 1.75, true)]
    [DataRow("Auto drop", 1.99, false)]
    [DataTestMethod]
    public void Write_Snack(string snackName, double snackPrice, bool snackType)
    {
        MenuItem newSnack = new MenuItem
        {
            Name = snackName,
            Price = (decimal)snackPrice,
            Type = snackType
        };

        MenuItemLogic.WriteMenuItem(newSnack);

        var actual = MenuItemLogic.GetByName(snackName);

        Assert.IsNotNull(actual);
        Assert.AreEqual(snackName, actual.Name);
        Assert.AreEqual(snackPrice, (double)actual.Price, 0.001);
        Assert.AreEqual(snackType, actual.Type);
    }

    [TestMethod]
    public void Write_Menu_DuplicateName()
    {
        var snack = new MenuItem
        {
            Name = "Popcorn",
            Price = 2.50m,
            Type = false
        };

        MenuItemLogic.WriteMenuItem(snack);

        var duplicateSnack = new MenuItem
        {
            Name = "Popcorn",
            Price = 2.50m,
            Type = false
        };

        try
        {
            MenuItemLogic.WriteMenuItem(duplicateSnack);
            Assert.Fail("An exception should have been thrown due to duplicate snack name.");
        }
        catch (Exception ex)
        {
            Assert.AreEqual("A snack with this name already exists.", ex.Message);
        }
    }

    [TestMethod]
    public void Delete_Snack_UnitTest()
    {
        var snack = new MenuItem
        {
            Name = "Popcorn",
            Price = 2.50m,
            Type = false
        };

        MenuItemLogic.WriteMenuItem(snack);

        MenuItemLogic.DeleteMenuItem(snack);

        var deletedSnack = MenuItemLogic.GetByName("Popcorn");
        Assert.IsNull(deletedSnack);
    }

    [TestMethod]
    public void Delete_Snack_SystemTest()
    {
        var snack = new MenuItem
        {
            Name = "Popcorn",
            Price = 2.50m,
            Type = false
        };

        MenuItemLogic.WriteMenuItem(snack);

        var actualSnack = MenuItemLogic.GetByName("Popcorn");
        Assert.IsNotNull(actualSnack);

        MenuItemLogic.DeleteMenuItem(snack);

        var deletedSnack = MenuItemLogic.GetByName("Popcorn");
        Assert.IsNull(deletedSnack);
    }
}
