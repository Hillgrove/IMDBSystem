namespace Data
{
    public static class MockMovieDatabase
    {
        private static List<string> _movieList = new List<string>
        {
            "The Matrix (1999)",
            "The Matrix Reloaded (2003)",
            "The Matrix Revolutions (2003)",
            "The Matrix Resurrections (2021)",
            "The Terminator (1984)",
            "Terminator 2: Judgment Day (1991)",
            "Terminator 3: Rise of the Machines (2003)",
            "Terminator Salvation (2009)",
            "Terminator Genisys (2015)",
            "Terminator: Dark Fate (2019)",
            "Inception (2010)",
            "Interstellar (2014)",
            "Dunkirk (2017)",
            "The Dark Knight (2008)",
            "Batman Begins (2005)",
            "The Dark Knight Rises (2012)",
            "Memento (2000)",
            "Tenet (2020)",
            "Insomnia (2002)",
            "Following (1998)"
        };

        public static List<string> GetMovies(string title)
        {
            return _movieList
                .Where(m => m.Contains(title, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }
    }
}
