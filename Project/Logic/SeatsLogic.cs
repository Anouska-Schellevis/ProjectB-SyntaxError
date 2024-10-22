static public class SeatsLogic
{

    static public SeatsModel GetById(int Id)
    {
        return SeatsAccess.GetById(Id);
    }

    static public void UpdateMovie(SeatsModel seat)
    {
        SeatsAccess.Update(seat);
    }

    static public void DeleteMovie(int id)

    {
        SeatsAccess.Delete(id);
    }

    static public void WriteMovie(SeatsModel seat)
    {
        SeatsAccess.Write(seat);
    }
}




