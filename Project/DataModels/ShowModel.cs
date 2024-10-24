
public class ShowModel
{
    public Int64 Id { get; set; }
    public Int64 TheatreId { get; set; }
    public Int64 MovieId { get; set; }
    public string Date { get; set; }

<<<<<<< HEAD
=======
    //public ShowModel() { }
>>>>>>> 1a2c47d5e9d8eb777f4ca348b1db91d3b9379bf2
    public ShowModel(Int64 id, Int64 theatre_id, Int64 movie_id, string date)
    {
        Id = id;
        TheatreId = theatre_id;
        MovieId = movie_id;
        Date = date;
    }


}




