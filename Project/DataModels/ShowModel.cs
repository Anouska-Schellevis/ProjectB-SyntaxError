
public class ShowModel
{
    public Int64 Id { get; set; }
    public Int64 TheatreId { get; set; }
    public Int64 MovieId { get; set; }
    public string Date { get; set; }

    public ShowModel(Int64 id, Int64 theatre_id, Int64 movie_id, string date)
    {
        Id = id;
        TheatreId = theatre_id;
        MovieId = movie_id;
        Date = date;
    }


}




