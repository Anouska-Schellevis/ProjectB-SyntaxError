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
    public void GetAllSeats_FromDatabase()
    {
        List<SeatsModel> testSeats = new()
        {
            new(1, 5, 6, 2),
            new(2, 12, 3, 3),
            new(3, 9, 6, 1)
        };

        SeatsAccess.ClearAllSeats();

        foreach (SeatsModel testSeat in testSeats)
        {
            SeatsLogic.WriteSeat(testSeat);
        }

        int excpectedAmount = 2 + 3 + 1;

        var retrievedSeats = SeatsLogic.GetAllSeats();
        int actualAmount = retrievedSeats.Sum(testSeat => testSeat.Price);

        Assert.AreEqual(excpectedAmount, actualAmount, "error.");
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
