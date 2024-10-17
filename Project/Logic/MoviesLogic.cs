static public class MoviesLogic
{

    static public MoviesModel GetByTitle(string title)
    {
        return MoviesAccess.GetByTitle(title);
    }

    static public MoviesModel GetAllMovies()
    {
        return MoviesAccess.GetAllMovies();
    }

    static public void UpdateMovie(MoviesModel movie)
    {
        movie.TimeInMinutes = Convert.ToInt32(movie.TimeInMinutes);
        movie.Genre = movie.Genre.Trim().ToUpper();
        movie.Description = movie.Description.Trim();
        movie.Title = movie.Genre.Trim().ToUpper();
        movie.Director = movie.Director.Trim().ToUpper();
        movie.ReleaseDate = movie.ReleaseDate.Trim().ToUpper();

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




