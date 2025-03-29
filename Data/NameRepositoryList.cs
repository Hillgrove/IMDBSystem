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

        private static readonly List<Name> _names = new List<Name>
        {
            new Name { PrimaryName = "Keanu Reeves", BirthYear = 1964 },
            new Name { PrimaryName = "Carrie-Anne Moss", BirthYear = 1967 },
            new Name { PrimaryName = "Laurence Fishburne", BirthYear = 1961 },
            new Name { PrimaryName = "Hugo Weaving", BirthYear = 1960 },
            new Name { PrimaryName = "Arnold Schwarzenegger", BirthYear = 1947 },
            new Name { PrimaryName = "Linda Hamilton", BirthYear = 1956 },
            new Name { PrimaryName = "Christian Bale", BirthYear = 1974 },
            new Name { PrimaryName = "Sam Worthington", BirthYear = 1976 },
            new Name { PrimaryName = "Leonardo DiCaprio", BirthYear = 1974 },
            new Name { PrimaryName = "Joseph Gordon-Levitt", BirthYear = 1981 },
            new Name { PrimaryName = "Tom Hardy", BirthYear = 1977 },
            new Name { PrimaryName = "Elliot Page", BirthYear = 1987 },
            new Name { PrimaryName = "Ken Watanabe", BirthYear = 1959 },
            new Name { PrimaryName = "Marion Cotillard", BirthYear = 1975 },
            new Name { PrimaryName = "Cillian Murphy", BirthYear = 1976 },
            new Name { PrimaryName = "Michael Caine", BirthYear = 1933 },
            new Name { PrimaryName = "Matthew McConaughey", BirthYear = 1969 },
            new Name { PrimaryName = "Anne Hathaway", BirthYear = 1982 },
            new Name { PrimaryName = "Jessica Chastain", BirthYear = 1977 },
            new Name { PrimaryName = "Casey Affleck", BirthYear = 1975 }
        };

        public List<Name> GetNames(string name)
        {
            return _names
                .Where(n => n.PrimaryName.Contains(name, StringComparison.OrdinalIgnoreCase))
                .OrderBy(n => n.PrimaryName)
                .ToList();
        }

        public void AddName(Name name)
        {
            _names.Add(name);
            Console.WriteLine("Name added successfully");
        }
    }
}
