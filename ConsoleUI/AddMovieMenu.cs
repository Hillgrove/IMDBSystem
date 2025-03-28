using Data;
using Data.Models;

namespace ConsoleUI
{
    internal class AddMovieMenu
    {
        private static MovieRepository _repository = new();

        public static void Execute()
        {
            while (true)
            {
                Console.Clear();
                DisplayMenuHeader();

                Console.Write("Title Type: ");
                string titleType = Console.ReadLine() ?? "";

                Console.Write("Primary Title: ");
                string primaryTitle = Console.ReadLine() ?? "";

                Console.Write("Original Title: ");
                string originalTitle = Console.ReadLine() ?? "";

                Console.Write("Is Adult (true/false): ");
                bool isAdult = bool.TryParse(Console.ReadLine(), out bool result) && result;

                Console.Write("Start Year (optional): ");
                int? startYear = int.TryParse(Console.ReadLine(), out int sy) ? sy : null;

                Console.Write("End Year (optional): ");
                int? endYear = int.TryParse(Console.ReadLine(), out int ey) ? ey : null;

                Console.Write("Runtime Minutes (optional): ");
                int? runtimeMinutes = int.TryParse(Console.ReadLine(), out int rm) ? rm : null;

                Console.Write("Genres (comma-separated): ");
                List<string> genres = new List<string>((Console.ReadLine() ?? "").Split(","));

                Movie newMovie = new Movie
                {
                    TitleType = titleType,
                    PrimaryTitle = primaryTitle,
                    OriginalTitle = originalTitle,
                    IsAdult = isAdult,
                    StartYear = startYear,
                    EndYear = endYear,
                    RuntimeMinutes = runtimeMinutes
                };

                _repository.AddMovie(newMovie);

                Console.WriteLine("Movie successfully added to the database.");
                Console.WriteLine("Press any key to return to the main menu...");
                Console.ReadKey();
            }
        }

        private static void DisplayMenuHeader()
        {
            Console.WriteLine();
            Console.WriteLine("=========================================================");
            Console.WriteLine("                 Add Movie to Database                   ");
            Console.WriteLine("=========================================================");
            Console.WriteLine();
        }
    }
}
