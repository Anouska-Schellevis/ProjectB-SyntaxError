public class MoviesModel
{
    public Int64 Id { get; set; }
    public int TimeInMinutes { get; set; }
    public string Genre { get; set; }
    public string Description { get; set; }
    public string Title { get; set; }
    public string Director { get; set; }
    public string ReleaseDate { get; set; }

    public MoviesModel(Int64 id, int timeInMinutes, string genre, string description, string title, string director, string releaseDate)
    {
        Id = id;
        TimeInMinutes = timeInMinutes;
        Genre = genre;
        Description = description;
        Title = title;
        Director = director;
        ReleaseDate = releaseDate;
    }


}



