using System.Dynamic;

namespace Unittest;

[TestClass]
public class SeatsTest
{
    // Id, RowNumber, ColumnNumber, Price
    [DataRow(1, 5, 8, 150)]
    [DataRow(2, 7, 3, 200)]
    [DataRow(3, 12, 6, 100)]
    [DataTestMethod]
    public void Write_Seats(Int64 Id, int RowNumber, int ColumnNumber, int Price)
    {
        SeatsLogic.WriteSeat(new(Id, RowNumber, ColumnNumber, Price));

        var actual = SeatsLogic.GetById((int)Id);

        Assert.IsNotNull(actual);
    }
    [TestMethod]
    public void Update_Seats()
    {
        
    }
    [TestMethod]
    public void Delete_Seats()
    {
        
    }


}

[TestClass]
public class ReservationTest
{
    [TestMethod]
    public void Write_Reservation()
    {

    }
    [TestMethod]
    public void Update_Reservation()
    {
        
    }
    [TestMethod]
    public void Delete_Reservation()
    {
        
    }
}