using Microsoft.VisualBasic;

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

    static public bool DoesNotContain(string to_contain, string what_to_contain)
    {
        if (to_contain.Contains(what_to_contain))
        {
            return false;
        }
        else
        {
            return true;
        }
    }
}




