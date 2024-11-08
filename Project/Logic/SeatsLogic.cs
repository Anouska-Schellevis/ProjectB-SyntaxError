static public class SeatsLogic
{

    static public SeatsModel GetById(int Id)
    {
        return SeatsAccess.GetById(Id);
    }

    static public void UpdateSeat(SeatsModel seat)
    {
        SeatsAccess.Update(seat);
    }

    static public void DeleteSeat(int id)

    {
        SeatsAccess.Delete(id);
    }

    static public void WriteSeat(SeatsModel seat)
    {
        SeatsAccess.Write(seat);
    }
}




