public class ShowModel
{
    public Int64 Id { get; set; }
    public int TheaterId { get; set; }
    public int MovieId { get; set; }
    public int ReservationId { get; set; }
    public string StartTime { get; set; }
    public ShowModel() {}

    public ShowModel(Int64 id, int theaterId, int movieId, int reservationId, string startTime)
    {
        Id = id;
        TheaterId = theaterId;
        MovieId = movieId;
        ReservationId = reservationId;
        StartTime = startTime;
    }


}



