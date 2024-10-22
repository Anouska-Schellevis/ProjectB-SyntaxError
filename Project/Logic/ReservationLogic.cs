static public class ReservationLogic
{

    static public ReservationModel GetById(int Id)
    {
        return ReservationAccess.GetById(Id);
    }

    static public void UpdateMovie(ReservationModel reservation)
    {
        ReservationAccess.Update(reservation);
    }

    static public void DeleteMovie(int id)

    {
        ReservationAccess.Delete(id);
    }

    static public void WriteMovie(ReservationModel reservation)
    {
        ReservationAccess.Write(reservation);
    }
}




