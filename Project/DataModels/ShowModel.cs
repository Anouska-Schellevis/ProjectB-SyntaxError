public class ShowModel
{
    public Int64 Id { get; set; }
    public int TheaterId { get; set; }
    public int MovieId { get; set; }
    public string Date { get; set; }
    public ShowModel() {}

    public ShowModel(Int64 id, int theaterId, int movieId, string date)
    {
        Id = id;
        TheaterId = theaterId;
        MovieId = movieId;
        Date = date;
    }


}



