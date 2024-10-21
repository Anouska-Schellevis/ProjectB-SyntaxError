static public class ShowLogic
{

    static public ShowModel GetByID(int id)
    {
        return ShowAccess.GetByID(id);
    }

    static public List<ShowModel> GetAllShows()
    {
        return ShowAccess.GetAllShows();
    }

    static public void UpdateShow(ShowModel show)
    {
        ShowAccess.Update(show);
    }

    static public void DeleteShow(int id)

    {
        ShowAccess.Delete(id);
    }

    static public void WriteShow(ShowModel show)
    {
        ShowAccess.Write(show);
    }
}




