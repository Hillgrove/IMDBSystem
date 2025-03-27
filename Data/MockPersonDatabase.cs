namespace Data
{
    public static class MockPersonDatabase
    {
        private static List<string> _personList = new List<string>
        {
            "Keanu Reeves",
            "Carrie-Anne Moss",
            "Laurence Fishburne",
            "Hugo Weaving",
            "Arnold Schwarzenegger",
            "Linda Hamilton",
            "Christian Bale",
            "Sam Worthington",
            "Leonardo DiCaprio",
            "Joseph Gordon-Levitt",
            "Tom Hardy",
            "Ellen Page",
            "Ken Watanabe",
            "Marion Cotillard",
            "Cillian Murphy",
            "Michael Caine",
            "Matthew McConaughey",
            "Anne Hathaway",
            "Jessica Chastain",
            "Casey Affleck"
        };

        public static List<string> GetPersons(string name)
        {
            return _personList
                .Where(p => p.Contains(name, StringComparison.OrdinalIgnoreCase))
                .OrderBy(p => p)
                .ToList();
        }
    }
}
