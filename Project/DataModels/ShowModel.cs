public class ShowModel
{
    public Int64 Id { get; set; }
    public int TheatreId { get; set; }
    public int MovieId { get; set; }
    public string Date { get; set; }
    public ShowModel() {}

    public ShowModel(Int64 id, int theatreId, int movieId, string date)
    {
        Id = id;
        TheatreId = theatreId;
        MovieId = movieId;
        Date = date;
    }


}



