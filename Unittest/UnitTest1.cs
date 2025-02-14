namespace UnittestReservation;

[TestClass]
public class SeatsTest
{
    [TestInitialize]
    public void TestInitialize()
    {
        SeatsAccess.ClearAllSeats();
    }

    // Id, RowNumber, ColumnNumber, Price
    [DataRow(1, 5, 6, 12.5)]
    [DataRow(1, 12, 3, 15.0)]
    [DataRow(1, 9, 6, 10.0)]
    [DataTestMethod]
    public void Write_Seats(long Id, int RowNumber, int ColumnNumber, double Price)
    {
        SeatsLogic.WriteSeat(new(Id, RowNumber, ColumnNumber, Convert.ToDecimal(Price)));

        var actual = SeatsLogic.GetById((int)Id);

        Assert.IsNotNull(actual);
    }

    [TestMethod]
    public void GetAllSeats_FromDatabase()
    {
        List<SeatsModel> testSeats = new()
        {
            new(1, 5, 6, 12.5m),
            new(2, 12, 3, 15.0m),
            new(3, 9, 6, 10.0m)
        };

        foreach (SeatsModel testSeat in testSeats)
        {
            SeatsLogic.WriteSeat(testSeat);
        }

        decimal excpectedAmount = 12.5m + 15.0m + 10.0m;

        var retrievedSeats = SeatsLogic.GetAllSeats();
        decimal actualAmount = retrievedSeats.Sum(testSeat => testSeat.Price);

        Assert.AreEqual(excpectedAmount, actualAmount, "error.");
    }
}

[TestClass]
public class ReservationTest
{
    [TestInitialize]
    public void TestInitialize()
    {
        ReservationAccess.ClearAllReservations();
    }

    [TestMethod]
    public void GetBarReservations_FromDatabase()
    {
        // Arrange
        List<ReservationModel> testReservations = new() {
            new(1, true, 3, 3, 4, ""),
            new(2, false, 4, 4, 5, ""),
            new(3, true, 5, 5, 6, "")
        };

        foreach (ReservationModel testReservation in testReservations)
        {
            ReservationLogic.WriteReservation(testReservation);
        }

        // Act
        var barReservations = ReservationLogic.GetBarReservations();

        // Assert
        Assert.AreEqual(2, barReservations.Count, "The count of bar reservations should match the expected value.");
        Assert.IsTrue(barReservations.All(r => r.Bar), "All returned reservations should be bar reservations.");
    }

    // [TestMethod]
    // public void GetAllSeats_FromDatabase()
    // {
    //     List<SeatsModel> testSeats = new()
    //     {
    //         new(1, 5, 6, 12.5m),
    //         new(2, 12, 3, 15.0m),
    //         new(3, 9, 6, 10.0m)
    //     };

    //     foreach (SeatsModel testSeat in testSeats)
    //     {
    //         SeatsLogic.WriteSeat(testSeat);
    //     }

    //     decimal excpectedAmount = 12.5m + 15.0m + 10.0m;

    //     var retrievedSeats = SeatsLogic.GetAllSeats();
    //     decimal actualAmount = retrievedSeats.Sum(testSeat => testSeat.Price);

    //     Assert.AreEqual(excpectedAmount, actualAmount, "error.");
    // }
}