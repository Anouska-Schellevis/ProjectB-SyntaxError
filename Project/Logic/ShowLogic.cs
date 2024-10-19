static public class ShowLogic
{

    // static public ShowModel GetByTitle(string title)
    // {
    //     return ShowAccess.GetByTitle(title);
    // }

    static public List<ShowModel> GetAllShows()
    {
        return ShowAccess.GetAllShows();
    }

    static public void UpdateShow(ShowModel show)
    {
        show.StartTime = show.StartTime.Trim();

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




