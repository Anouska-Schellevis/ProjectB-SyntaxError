[TestClass]
public class MovieTest
{
    [DataTestMethod]
    [DataRow(13, 99, "Actie", "In a world...", "Some Weird Action Movie", "Lil 'ol me", "2025-01-26 18:00")]
    [DataRow(14, 100, "Drama", "Hakuna Matata","The Lion King", "Bayoncee", "2025-01-27 19:00")]
    [DataRow(15, 101, "Komedie","Damn....", "Friday", "Ice Cube", "2025-01-28 20:00")]
    [DataRow(16, 102, "Familiefilm", "...sitting in a tree K-I-S-S-I-N-G", "Forgot The Title", " Forgot His Face", "2025-01-29 21:00")]
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
    [DataTestMethod]
    [DataRow(64, 1, 1, "2025-01-26 18:00")]
    [DataRow(65, 2, 2, "2025-01-27 19:00")]
    [DataRow(66, 3, 3, "2025-01-28 20:00")]
    [DataRow(67, 1, 3, "2025-01-29 21:00")]
    public void WriteShow(long Id, long Theatre_id, long Movie_id, string Date)
    {
        ShowLogic.WriteShow(new(Id, Theatre_id, Movie_id, Date));

        var madefortest = ShowLogic.GetByID((int)Id);

        Assert.IsNotNull(madefortest, "Check for writeshow failed");
        Assert.AreEqual(Theatre_id, madefortest.TheatreId, "theatre id is different");
        Assert.AreEqual(Movie_id, madefortest.MovieId, "Movie Id is the same");
        Assert.AreEqual(Date, madefortest.Date);
    }

    // [DataTestMethod]
    // [DataRow(68, 2, 4, "2025-01-30 22:00")]
    // public static void Check20Min(long Id, long Theatre_id, long Movie_id, string Date)
    // {
    //     // DateTime starttime = DateTime.Parse(Date);
    //     // DateTime endtime = ;
    // }
}