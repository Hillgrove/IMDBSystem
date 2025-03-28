using Data.Models;

namespace Data
{
    public class MovieRepository
    {
        private List<Movie> _movies = new List<Movie>
        {
            new Movie { TitleType = "Movie", PrimaryTitle = "The Matrix", OriginalTitle = "The Matrix", IsAdult = false, StartYear = 1999, EndYear = null, RuntimeMinutes = 136 },
            new Movie { TitleType = "Movie", PrimaryTitle = "The Matrix Reloaded", OriginalTitle = "The Matrix Reloaded", IsAdult = false, StartYear = 2003, EndYear = null, RuntimeMinutes = 138 },
            new Movie { TitleType = "Movie", PrimaryTitle = "The Matrix Revolutions", OriginalTitle = "The Matrix Revolutions", IsAdult = false, StartYear = 2003, EndYear = null, RuntimeMinutes = 129 },
            new Movie { TitleType = "Movie", PrimaryTitle = "The Matrix Resurrections", OriginalTitle = "The Matrix Resurrections", IsAdult = false, StartYear = 2021, EndYear = null, RuntimeMinutes = 148 },
            new Movie { TitleType = "Movie", PrimaryTitle = "The Terminator", OriginalTitle = "The Terminator", IsAdult = false, StartYear = 1984, EndYear = null, RuntimeMinutes = 107 },
            new Movie { TitleType = "Movie", PrimaryTitle = "Terminator 2: Judgment Day", OriginalTitle = "Terminator 2: Judgment Day", IsAdult = false, StartYear = 1991, EndYear = null, RuntimeMinutes = 137 },
            new Movie { TitleType = "Movie", PrimaryTitle = "Terminator 3: Rise of the Machines", OriginalTitle = "Terminator 3: Rise of the Machines", IsAdult = false, StartYear = 2003, EndYear = null, RuntimeMinutes = 109 },
            new Movie { TitleType = "Movie", PrimaryTitle = "Terminator Salvation", OriginalTitle = "Terminator Salvation", IsAdult = false, StartYear = 2009, EndYear = null, RuntimeMinutes = 115 },
            new Movie { TitleType = "Movie", PrimaryTitle = "Terminator Genisys", OriginalTitle = "Terminator Genisys", IsAdult = false, StartYear = 2015, EndYear = null, RuntimeMinutes = 126 },
            new Movie { TitleType = "Movie", PrimaryTitle = "Terminator: Dark Fate", OriginalTitle = "Terminator: Dark Fate", IsAdult = false, StartYear = 2019, EndYear = null, RuntimeMinutes = 128 },
            new Movie { TitleType = "Movie", PrimaryTitle = "Inception", OriginalTitle = "Inception", IsAdult = false, StartYear = 2010, EndYear = null, RuntimeMinutes = 148 },
            new Movie { TitleType = "Movie", PrimaryTitle = "Interstellar", OriginalTitle = "Interstellar", IsAdult = false, StartYear = 2014, EndYear = null, RuntimeMinutes = 169 },
            new Movie { TitleType = "Movie", PrimaryTitle = "Dunkirk", OriginalTitle = "Dunkirk", IsAdult = false, StartYear = 2017, EndYear = null, RuntimeMinutes = 106 },
            new Movie { TitleType = "Movie", PrimaryTitle = "The Dark Knight", OriginalTitle = "The Dark Knight", IsAdult = false, StartYear = 2008, EndYear = null, RuntimeMinutes = 152 },
            new Movie { TitleType = "Movie", PrimaryTitle = "Batman Begins", OriginalTitle = "Batman Begins", IsAdult = false, StartYear = 2005, EndYear = null, RuntimeMinutes = 140 },
            new Movie { TitleType = "Movie", PrimaryTitle = "The Dark Knight Rises", OriginalTitle = "The Dark Knight Rises", IsAdult = false, StartYear = 2012, EndYear = null, RuntimeMinutes = 164 },
            new Movie { TitleType = "Movie", PrimaryTitle = "Memento", OriginalTitle = "Memento", IsAdult = false, StartYear = 2000, EndYear = null, RuntimeMinutes = 113 },
            new Movie { TitleType = "Movie", PrimaryTitle = "Tenet", OriginalTitle = "Tenet", IsAdult = false, StartYear = 2020, EndYear = null, RuntimeMinutes = 150 },
            new Movie { TitleType = "Movie", PrimaryTitle = "Insomnia", OriginalTitle = "Insomnia", IsAdult = false, StartYear = 2002, EndYear = null, RuntimeMinutes = 118 },
            new Movie { TitleType = "Movie", PrimaryTitle = "Following", OriginalTitle = "Following", IsAdult = false, StartYear = 1998, EndYear = null, RuntimeMinutes = 69 }
        };

        public void AddMovie(Movie movie)
        {
            _movies.Add(movie);
            Console.WriteLine("Movie added successfully");
        }

        public List<Movie> GetMovies(string title)
        {
            return _movies
                .Where(m => m.PrimaryTitle.Contains(title, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
    }
}




