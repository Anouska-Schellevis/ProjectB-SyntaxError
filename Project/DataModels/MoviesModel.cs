public class MoviesModel
{
    public Int64 Id { get; set; }
    public Int64 TimeInMinutes { get; set; }
    public string Genre { get; set; }
    public string Description { get; set; }
    public string Title { get; set; }
    public string Director { get; set; }
    public string ReleaseDate { get; set; }

    public MoviesModel(Int64 id, Int64 time_in_minutes, string genre, string description, string title, string director, string release_date)
    {
        Id = id;
        TimeInMinutes = time_in_minutes;
        Genre = genre;
        Description = description;
        Title = title;
        Director = director;
        ReleaseDate = release_date;
    }


}



