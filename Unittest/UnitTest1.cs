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
}

[TestClass]
public class ReservationTest
{
    [TestMethod]
    public void GetAllBar_Reservation_FromDatabase()
    {

        List<ReservationModel> testReservations = new() {
            new(1, true, 3, 3, 4),
            new(2, false, 4, 4, 5),
            new(3, true, 5, 5, 6)
        };

        foreach (ReservationModel testReservation in testReservations)
        {
            ReservationLogic.WriteReservation(testReservation);
        }

        var barReservations = ReservationLogic.GetBarReservations();

        Assert.AreEqual(2, barReservations.Count);
    }
}

[TestClass]
public class ShowTest
{
    //test
}

[TestClass]
public class ReservationTest
{
    //test
}