using Data.Models;

namespace Data
{
    public interface IMovieRepository
    {
        void AddMovie(Title movie);
        List<Genre> GetGenres();
        List<Title> GetMovies(string title);
        List<IMDBType> GetTypes();
    }
}