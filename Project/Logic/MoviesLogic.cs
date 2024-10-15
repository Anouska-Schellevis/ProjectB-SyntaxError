using System.Collections.Concurrent;

public class MoviesLogic
{
    public MoviesLogic()
    {
        // Could do something here

    }

    public MoviesModel GetById(int id)
    {
        return MoviesAccess.GetById(id);
    }

    public MoviesModel GetByTitle(string title)
    {
        return MoviesAccess.GetByTitle(title);
    }

    public MoviesModel GetAllMovies()
    {
        return MoviesAccess.GetAllMovies();
    }

    public void UpdateMovie(MoviesModel movie)
    {
        movie.TimeInMinutes = Convert.ToInt32(movie.TimeInMinutes);
        movie.Genre = movie.Genre.Trim().ToUpper();
        movie.Description = movie.Description.Trim();
        movie.Title = movie.Genre.Trim().ToUpper();
        movie.Director = movie.Director.Trim().ToUpper();
        movie.ReleaseDate = movie.ReleaseDate.Trim().ToUpper();

        MoviesAccess.Update(movie);
    }
}




