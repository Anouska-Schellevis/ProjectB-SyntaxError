public class ReservationModel
{
    public Int64 Id { get; set; }
    public bool Bar { get; set; }
    public int SeatsId { get; set; }
    public int UserId { get; set; }
    public int MovieId { get; set; }
    public ReservationModel() {}

    public ReservationModel(Int64 id, bool bar, int seatsId, int userId, int movieId)
    {
        Id = id;
        Bar = bar;
        SeatsId = seatsId;
        UserId = userId;
        MovieId = movieId;
    }


}



