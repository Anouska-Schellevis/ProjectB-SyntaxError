static public class ReservationLogic
{

    static public ReservationModel GetById(int Id)
    {
        return ReservationAccess.GetById(Id);
    }

    static public List<ReservationModel> GetBarReservations()
    {
        return ReservationAccess.GetBarReservations();
    }

    static public void UpdateReservation(ReservationModel reservation)
    {
        ReservationAccess.Update(reservation);
    }

    static public void DeleteReservation(int id)

    {
        ReservationAccess.Delete(id);
    }

    static public void WriteReservation(ReservationModel reservation)
    {
        ReservationAccess.Write(reservation);
    }

    static public List<ReservationModel> GetReservationsByUserId(long userId)
    {
        return ReservationAccess.GetReservationsByUserId(userId);
    }

    static public List<long> GetReservedSeatsByShowId(long showId)
    {
        return ReservationAccess.GetReservedSeatsByShowId(showId);
    }
}




