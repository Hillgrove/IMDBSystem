using Data.Models;

namespace Data
{
    public class TitleRepositoryList : ITitleRepository
    {
        #region Singleton Pattern
        private static readonly TitleRepositoryList _instance = new();
        public static TitleRepositoryList Instance { get => _instance; }
        private TitleRepositoryList() { }
        #endregion

        private static readonly List<IMDBType> _types = new List<IMDBType>
        {
            new IMDBType { Id = 1, Name = "movie" },
            new IMDBType { Id = 2, Name = "short" },
            new IMDBType { Id = 3, Name = "tvEpisode" },
            new IMDBType { Id = 4, Name = "tvMiniSeries" },
            new IMDBType { Id = 5, Name = "tvMovie" },
            new IMDBType { Id = 6, Name = "tvPilot" },
            new IMDBType { Id = 7, Name = "tvSeries" },
            new IMDBType { Id = 8, Name = "tvShort" },
            new IMDBType { Id = 9, Name = "tvSpecial" },
            new IMDBType { Id = 10, Name = "video" },
            new IMDBType { Id = 11, Name = "videoGame" }
        };

        private static readonly List<Genre> _genres = new List<Genre>
        {
            new Genre { Id = 1, Name = "Action" },
            new Genre { Id = 2, Name = "Adventure" },
            new Genre { Id = 3, Name = "Drama" },
            new Genre { Id = 4, Name = "Sci-Fi" },
            new Genre { Id = 5, Name = "Thriller" },
            new Genre { Id = 6, Name = "Mystery" },
            new Genre { Id = 7, Name = "War" },
            new Genre { Id = 8, Name = "Crime" },
            new Genre { Id = 9, Name = "Romance" },
            new Genre { Id = 10, Name = "Fantasy" },
            new Genre { Id = 11, Name = "Horror" },
            new Genre { Id = 12, Name = "Comedy" }
        };

        private static readonly List<Title> _titles = new List<Title>
        {
            new Title { Type = _types[0], PrimaryTitle = "The Matrix", OriginalTitle = "The Matrix", IsAdult = false, StartYear = 1999, RuntimeMinutes = 136, Genres = new List<Genre> { _genres[0], _genres[3] } },
            new Title { Type = _types[0], PrimaryTitle = "The Matrix Reloaded", OriginalTitle = "The Matrix Reloaded", IsAdult = false, StartYear = 2003, RuntimeMinutes = 138, Genres = new List<Genre> { _genres[0], _genres[3] } },
            new Title { Type = _types[0], PrimaryTitle = "The Matrix Revolutions", OriginalTitle = "The Matrix Revolutions", IsAdult = false, StartYear = 2003, RuntimeMinutes = 129, Genres = new List<Genre> { _genres[0], _genres[3] } },
            new Title { Type = _types[0], PrimaryTitle = "The Matrix Resurrections", OriginalTitle = "The Matrix Resurrections", IsAdult = false, StartYear = 2021, RuntimeMinutes = 148, Genres = new List<Genre> { _genres[0], _genres[3] } },
            new Title { Type = _types[0], PrimaryTitle = "The Terminator", OriginalTitle = "The Terminator", IsAdult = false, StartYear = 1984, RuntimeMinutes = 107, Genres = new List<Genre> { _genres[0], _genres[4] } },
            new Title { Type = _types[0], PrimaryTitle = "Terminator 2: Judgment Day", OriginalTitle = "Terminator 2: Judgment Day", IsAdult = false, StartYear = 1991, RuntimeMinutes = 137, Genres = new List<Genre> { _genres[0], _genres[3] } },
            new Title { Type = _types[0], PrimaryTitle = "Terminator 3: Rise of the Machines", OriginalTitle = "Terminator 3: Rise of the Machines", IsAdult = false, StartYear = 2003, RuntimeMinutes = 109, Genres = new List<Genre> { _genres[0], _genres[3] } },
            new Title { Type = _types[0], PrimaryTitle = "Terminator Salvation", OriginalTitle = "Terminator Salvation", IsAdult = false, StartYear = 2009, RuntimeMinutes = 115, Genres = new List<Genre> { _genres[0], _genres[3] } },
            new Title { Type = _types[0], PrimaryTitle = "Terminator Genisys", OriginalTitle = "Terminator Genisys", IsAdult = false, StartYear = 2015, RuntimeMinutes = 126, Genres = new List<Genre> { _genres[0], _genres[3] } },
            new Title { Type = _types[0], PrimaryTitle = "Terminator: Dark Fate", OriginalTitle = "Terminator: Dark Fate", IsAdult = false, StartYear = 2019, RuntimeMinutes = 128, Genres = new List<Genre> { _genres[0], _genres[3] } },
            new Title { Type = _types[0], PrimaryTitle = "Inception", OriginalTitle = "Inception", IsAdult = false, StartYear = 2010, RuntimeMinutes = 148, Genres = new List<Genre> { _genres[0], _genres[1], _genres[6] } },
            new Title { Type = _types[0], PrimaryTitle = "Interstellar", OriginalTitle = "Interstellar", IsAdult = false, StartYear = 2014, RuntimeMinutes = 169, Genres = new List<Genre> { _genres[1], _genres[3], _genres[6] } },
            new Title { Type = _types[0], PrimaryTitle = "Dunkirk", OriginalTitle = "Dunkirk", IsAdult = false, StartYear = 2017, RuntimeMinutes = 106, Genres = new List<Genre> { _genres[2], _genres[6] } },
            new Title { Type = _types[0], PrimaryTitle = "The Dark Knight", OriginalTitle = "The Dark Knight", IsAdult = false, StartYear = 2008, RuntimeMinutes = 152, Genres = new List<Genre> { _genres[0], _genres[8] } },
            new Title { Type = _types[0], PrimaryTitle = "Batman Begins", OriginalTitle = "Batman Begins", IsAdult = false, StartYear = 2005, RuntimeMinutes = 140, Genres = new List<Genre> { _genres[0], _genres[8] } },
            new Title { Type = _types[0], PrimaryTitle = "The Dark Knight Rises", OriginalTitle = "The Dark Knight Rises", IsAdult = false, StartYear = 2012, RuntimeMinutes = 164, Genres = new List<Genre> { _genres[0], _genres[8] } },
            new Title { Type = _types[0], PrimaryTitle = "Memento", OriginalTitle = "Memento", IsAdult = false, StartYear = 2000, RuntimeMinutes = 113, Genres = new List<Genre> { _genres[4], _genres[5] } },
            new Title { Type = _types[0], PrimaryTitle = "Tenet", OriginalTitle = "Tenet", IsAdult = false, StartYear = 2020, RuntimeMinutes = 150, Genres = new List<Genre> { _genres[0], _genres[5] } },
            new Title { Type = _types[0], PrimaryTitle = "Insomnia", OriginalTitle = "Insomnia", IsAdult = false, StartYear = 2002, RuntimeMinutes = 118, Genres = new List<Genre> { _genres[2], _genres[5] } },
            new Title { Type = _types[0], PrimaryTitle = "Following", OriginalTitle = "Following", IsAdult = false, StartYear = 1998, RuntimeMinutes = 69, Genres = new List<Genre> { _genres[2], _genres[5] } },
            new Title { Type = _types[0], PrimaryTitle = "The Incredibly Long and Unnecessarily Overcomplicated Title That Somehow Got Approved by the Studio and Somehow Managed to Fit on the Movie Poster Without Causing an Absolute Design Disaster", OriginalTitle = "The Longest Movie Title Ever", IsAdult = false, StartYear = 2025, RuntimeMinutes = 150, Genres = new List<Genre> { _genres[0], _genres[3], _genres[9] } },

        };


        public void AddTitle(Title title)
        {
            _titles.Add(title);
            Console.WriteLine("Title added successfully");
        }

        public List<Title> GetTitles(string title)
        {
            return _titles
                .Where(m => m.PrimaryTitle.Contains(title, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public List<IMDBType> GetTypes()
        {
            return _types;
        }

        public List<Genre> GetGenres()
        {
            return _genres;
        }

        public void UpdateTitle(Title original, Title updated)
        {
            int index = _titles.IndexOf(original);
            if (index >= 0)
                _titles[index] = updated;
        }

        public void DeleteTitle(Title title)
        {
            _titles.Remove(title);
        }
    }
}




