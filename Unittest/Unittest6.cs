[TestClass]
public class MovieTest
{
    [TestInitialize]
    public void TestInitialize()
    {
        MoviesAccess.ClearAllMovies();
    }

    [DataTestMethod]
    [DataRow(1, 99, "Actie", "In a world...", "Some Weird Action Movie", "Lil 'ol me", "2025-01-26 18:00")]
    [DataRow(1, 100, "Drama", "Hakuna Matata", "The Lion King", "Bayoncee", "2025-01-27 19:00")]
    [DataRow(1, 101, "Komedie", "Damn....", "Friday", "Ice Cube", "2025-01-28 20:00")]
    [DataRow(1, 102, "Familiefilm", "...sitting in a tree K-I-S-S-I-N-G", "Forgot The Title", " Forgot His Face", "2025-01-29 21:00")]
    public void WriteMovie(long Id, long Time_in_minutes, string Genre, string Description, string Title, string Director, string Release_date)
    {
        MoviesLogic.WriteMovie(new(Id, Time_in_minutes, Genre, Description, Title, Director, Release_date));

        var madefortest = MoviesLogic.GetById((int)Id);

        Assert.IsNotNull(madefortest, "Check for writemovie failed");
        Assert.AreEqual(Genre, madefortest.Genre, "Genre is different");
        Assert.AreEqual(Description, madefortest.Description, "Description is different");
        Assert.AreEqual(Title, madefortest.Title, "Title is different");
        Assert.AreEqual(Director, madefortest.Director, "Director is different");
        Assert.AreEqual(Release_date, madefortest.ReleaseDate, "Release date is different");
    }
}

[TestClass]
public class ShowTest
{

    [TestInitialize]
    public void TestInitialize()
    {
        ShowAccess.ClearAllShows();
    }

    [DataTestMethod]
    [DataRow(1, 1, 1, "2025-01-26 18:00")]
    [DataRow(1, 2, 2, "2025-01-27 19:00")]
    [DataRow(1, 3, 3, "2025-01-28 20:00")]
    [DataRow(1, 1, 3, "2025-01-30 21:00")]
    public void WriteShow(long Id, long Theatre_id, long Movie_id, string Date)
    {
        ShowLogic.WriteShow(new(Id, Theatre_id, Movie_id, Date));

        var madefortest = ShowLogic.GetByID((int)Id);

        Assert.IsNotNull(madefortest, "Check for writeshow failed");
        Assert.AreEqual(Theatre_id, madefortest.TheatreId, "theatre id is different");
        Assert.AreEqual(Movie_id, madefortest.MovieId, "Movie Id is the same");
        Assert.AreEqual(Date, madefortest.Date);
    }

    [DataTestMethod]
    [DataRow(1, 2, 4, "2025-01-30 22:00")]
    public static void Check20Min(long Id, long Theatre_id, long Movie_id, string Date)
    {
        int TimeInMinutes = 120;
        string time = Date.Split(" ")[1];
        DateTime starttime = DateTime.Parse(Date);
        TimeSpan endtime = TimeSpan.Parse(Show.GetEndTime(time, TimeInMinutes));
        TimeSpan timeItShouldBe = new TimeSpan(0, 20, 0);
        Assert.AreEqual(timeItShouldBe, endtime);
    }

    [DataTestMethod]
    [DataRow(1, 3, 5, "2025-01-31 10:00")]
    public static void CheckWholeWeek(long Id, long Theatre_id, long Movie_id, string Date)
    {
        ShowLogic.WriteShow(new(Id, Theatre_id, Movie_id, Date));
        Show.PlanForAWholeWeek(new ShowModel(Id, Theatre_id, Movie_id, Date));
        var madefortest1 = ShowLogic.GetByID(2);
        var madefortest2 = ShowLogic.GetByID(3);
        var madefortest3 = ShowLogic.GetByID(4);
        var madefortest4 = ShowLogic.GetByID(5);
        var madefortest5 = ShowLogic.GetByID(6);
        var madefortest6 = ShowLogic.GetByID(7);
        Assert.AreEqual(new ShowModel(2, 3, 5, "2025-02-01 10:00"), madefortest1);
        Assert.AreEqual(new ShowModel(3, 3, 5, "2025-02-02 10:00"), madefortest2);
        Assert.AreEqual(new ShowModel(4, 3, 5, "2025-02-03 10:00"), madefortest3);
        Assert.AreEqual(new ShowModel(5, 3, 5, "2025-02-04 10:00"), madefortest4);
        Assert.AreEqual(new ShowModel(6, 3, 5, "2025-02-05 10:00"), madefortest5);
        Assert.AreEqual(new ShowModel(7, 3, 5, "2025-02-06 10:00"), madefortest6);
    }
}