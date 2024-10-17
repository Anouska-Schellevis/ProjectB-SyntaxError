static public class MoviesLogic
{

    static public MoviesModel GetByTitle(string title)
    {
        return MoviesAccess.GetByTitle(title);
    }

    static public List<MoviesModel> GetAllMovies()
    {
        return MoviesAccess.GetAllMovies();
    }

    static public void UpdateMovie(MoviesModel movie)
    {
        movie.Genre = movie.Genre.Trim();
        movie.Description = movie.Description.Trim();
        movie.Title = movie.Title.Trim();
        movie.Director = movie.Director.Trim();

        MoviesAccess.Update(movie);
    }

    static public void DeleteMovie(int id)

    {
        MoviesAccess.Delete(id);
    }

    static public void WriteMovie(MoviesModel movie)
    {
        MoviesAccess.Write(movie);
    }
}




