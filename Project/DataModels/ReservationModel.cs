public class ReservationModel
{
    public Int64 Id { get; set; }
    public bool Bar { get; set; }
    public int SeatsId { get; set; }
    public int UserId { get; set; }
    public int ShowId { get; set; }
    public string Snacks { get; set; }
    public ReservationModel() { }

    public ReservationModel(Int64 id, bool bar, int seatsId, int userId, int showId, string snacks)
    {
        Id = id;
        Bar = bar;
        SeatsId = seatsId;
        UserId = userId;
        ShowId = showId;
        Snacks = snacks;
    }
}