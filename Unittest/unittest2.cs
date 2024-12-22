namespace UnittestSeats;

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
public class SeatReservationTest
{
    [TestInitialize]
    public void TestInitialize()
    {
        ReservationAccess.ClearAllReservations();
    }

    [TestMethod]
    public void GetReservations_ShouldReturnAllReservations()
    {
        // Arrange
        List<ReservationModel> testReservations = new()
        {
            new(1L, false, 101, 201, 301, "Popcorn"),
            new(2L, true, 102, 202, 302, "Chips"),
            new(3L, false, 103, 203, 303, "Soda")
        };

        foreach (var reservation in testReservations)
        {
            ReservationLogic.WriteReservation(reservation);
        }

        long testUserId = 201; // Specify a user ID to retrieve reservations for

        // Act
        var allReservations = ReservationLogic.GetReservationsByUserId(testUserId).ToList();

        // Assert: Check the total count of reservations for the specified user
        var expectedReservations = testReservations.Where(r => r.UserId == testUserId).ToList();
        Assert.AreEqual(expectedReservations.Count, allReservations.Count, "The total number of reservations for the user is incorrect.");

        // Verify each reservation's details
        foreach (var testReservation in expectedReservations)
        {
            var retrieved = allReservations.Find(r => r.Id == testReservation.Id);
            Assert.IsNotNull(retrieved, $"Reservation with Id {testReservation.Id} was not found.");
            Assert.AreEqual(testReservation.SeatsId, retrieved.SeatsId, $"SeatsId mismatch for reservation {testReservation.Id}");
            Assert.AreEqual(testReservation.UserId, retrieved.UserId, $"UserId mismatch for reservation {testReservation.Id}");
            Assert.AreEqual(testReservation.ShowId, retrieved.ShowId, $"ShowId mismatch for reservation {testReservation.Id}");
            Assert.AreEqual(testReservation.Snacks, retrieved.Snacks, $"Snacks mismatch for reservation {testReservation.Id}");
        }
    }
}