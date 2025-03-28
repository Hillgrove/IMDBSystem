using Data.Models;

namespace Data
{
    public class NameRepositoryList : INameRepository
    {
        #region Singleton Pattern
        private static readonly NameRepositoryList _instance = new();
        public static NameRepositoryList Instance { get => _instance; }
        private NameRepositoryList() { }
        #endregion

        private static readonly List<Name> _namesList = new List<Name>
        {
            new Name { Nconst = "nm0000206", PrimaryName = "Keanu Reeves", BirthYear = 1964 },
            new Name { Nconst = "nm0000209", PrimaryName = "Carrie-Anne Moss", BirthYear = 1967 },
            new Name { Nconst = "nm0000401", PrimaryName = "Laurence Fishburne", BirthYear = 1961 },
            new Name { Nconst = "nm0000694", PrimaryName = "Hugo Weaving", BirthYear = 1960 },
            new Name { Nconst = "nm0000216", PrimaryName = "Arnold Schwarzenegger", BirthYear = 1947 },
            new Name { Nconst = "nm0000157", PrimaryName = "Linda Hamilton", BirthYear = 1956 },
            new Name { Nconst = "nm0000288", PrimaryName = "Christian Bale", BirthYear = 1974 },
            new Name { Nconst = "nm0941777", PrimaryName = "Sam Worthington", BirthYear = 1976 },
            new Name { Nconst = "nm0000138", PrimaryName = "Leonardo DiCaprio", BirthYear = 1974 },
            new Name { Nconst = "nm0330687", PrimaryName = "Joseph Gordon-Levitt", BirthYear = 1981 },
            new Name { Nconst = "nm0362766", PrimaryName = "Tom Hardy", BirthYear = 1977 },
            new Name { Nconst = "nm0680983", PrimaryName = "Elliot Page", BirthYear = 1987 },
            new Name { Nconst = "nm0913822", PrimaryName = "Ken Watanabe", BirthYear = 1959 },
            new Name { Nconst = "nm0182839", PrimaryName = "Marion Cotillard", BirthYear = 1975 },
            new Name { Nconst = "nm0614165", PrimaryName = "Cillian Murphy", BirthYear = 1976 },
            new Name { Nconst = "nm0000323", PrimaryName = "Michael Caine", BirthYear = 1933 },
            new Name { Nconst = "nm0000190", PrimaryName = "Matthew McConaughey", BirthYear = 1969 },
            new Name { Nconst = "nm0004266", PrimaryName = "Anne Hathaway", BirthYear = 1982 },
            new Name { Nconst = "nm1567113", PrimaryName = "Jessica Chastain", BirthYear = 1977 },
            new Name { Nconst = "nm0000729", PrimaryName = "Casey Affleck", BirthYear = 1975 }
        };

        public List<Name> GetPersons(string name)
        {
            return _namesList
                .Where(n => n.PrimaryName.Contains(name, StringComparison.OrdinalIgnoreCase))
                .OrderBy(n => n.PrimaryName)
                .ToList();
        }
    }
}
