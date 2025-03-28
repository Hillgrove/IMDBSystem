using Data;
using Data.Models;

namespace ConsoleUI
{
    internal class AddMovieMenu
    {
        private readonly IMovieRepository _repository;

        public AddMovieMenu(IMovieRepository repository)
        {
            _repository = repository;
        }

        public void Execute()
        {
            while (true)
            {
                Console.Clear();
                DisplayMenuHeader();

                // Title Type Selection
                var titleType = GetTitleType();
                if (titleType == null)
                    continue;

                // Primary Title
                var primaryTitle = GetTitleInput("Primary Title");
                if (primaryTitle == null)
                    continue;

                // Original Title
                var originalTitle = GetTitleInput("Original Title");
                if (originalTitle == null)
                    continue;

                // Is Adult
                bool isAdult = GetAdultInput();

                // End Year
                int? endYear = GetOptionalYear("End Year");

                // Runtime Minutes
                int? runtimeMinutes = GetOptionalRuntime("Runtime Minutes");

                // Genre Selection
                var selectedGenres = SelectGenres();
                if (selectedGenres == null)
                    continue;

                // Confirm and Add Movie
                Title newMovie = new Title
                {
                    Type = titleType,
                    PrimaryTitle = primaryTitle,
                    OriginalTitle = originalTitle,
                    IsAdult = isAdult,
                    EndYear = endYear,
                    RuntimeMinutes = runtimeMinutes,
                    Genres = selectedGenres
                };

                Console.WriteLine("\nMovie to be added:");
                Console.WriteLine(newMovie);
                if (GetYesNoInput("Accept? [Y] Yes / [N] No"))
                {
                    _repository.AddMovie(newMovie);
                    Console.WriteLine("\nMovie added successfully!");
                }
                else
                {
                    Console.WriteLine("\nOperation cancelled.");
                }

                // Add Another or Go Back
                if (!GetYesNoInput("Add another movie? [Y] Yes / [N] No"))
                    return;
            }
        }

        private bool GetYesNoInput(string prompt)
        {
            Console.WriteLine(prompt);
            while (true)
            {
                var key = Console.ReadKey().Key;
                if (key == ConsoleKey.Y)
                    return true;
                if (key == ConsoleKey.N)
                    return false;
                Console.WriteLine("\nInvalid input. Please press 'Y' or 'N'.");
            }
        }

        private IMDBType? GetTitleType()
        {
            var types = _repository.GetTypes();
            while (true)
            {
                Console.WriteLine("Select Title Type:");
                for (int i = 0; i < types.Count; i++)
                {
                    Console.WriteLine($"[{i + 1}] {types[i].Name}");
                }
                Console.Write("Your choice: ");
                if (int.TryParse(Console.ReadLine(), out int choice) && choice >= 1 && choice <= types.Count)
                {
                    return types[choice - 1];
                }
                Console.WriteLine("Invalid choice. Please try again.");
            }
        }

        private string? GetTitleInput(string prompt)
        {
            Console.Write($"{prompt}: ");
            string input = Console.ReadLine()?.Trim() ?? "";
            if (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("Title cannot be empty. Press any key to retry...");
                Console.ReadKey();
                return null;
            }
            return input;
        }

        private bool GetAdultInput()
        {
            while (true)
            {
                Console.WriteLine("Is Adult (press [T] for True, [F] for False): ");
                ConsoleKey key = Console.ReadKey().Key;
                if (key == ConsoleKey.T) return true;
                if (key == ConsoleKey.F) return false;
                Console.WriteLine("\nInvalid input. Please press 'T' or 'F'.");
            }
        }

        private int? GetOptionalYear(string prompt)
        {
            Console.Write($"{prompt} (optional): ");
            if (int.TryParse(Console.ReadLine(), out int year))
                return year;
            return null;
        }

        private int? GetOptionalRuntime(string prompt)
        {
            Console.Write($"{prompt} (optional): ");
            if (int.TryParse(Console.ReadLine(), out int runtime) && runtime > 0)
                return runtime;
            return null;
        }

        private List<Genre>? SelectGenres()
        {
            var genres = _repository.GetGenres();
            Console.WriteLine("Select 1 to 3 Genres (comma-separated):");
            for (int i = 0; i < genres.Count; i++)
            {
                Console.WriteLine($"[{i + 1}] {genres[i].Name}");
            }
            Console.Write("Your choices: ");
            var genreChoices = Console.ReadLine()?.Split(",").Select(x => int.TryParse(x.Trim(), out int g) ? g : -1).Where(x => x > 0 && x <= genres.Count).Distinct().Take(3).ToList();
            if (genreChoices.Count == 0)
            {
                Console.WriteLine("Invalid genre choices. Press any key to retry...");
                Console.ReadKey();
                return null;
            }
            return genreChoices.Select(gc => genres[gc - 1]).ToList();
        }

        private void DisplayMenuHeader()
        {
            Console.WriteLine();
            Console.WriteLine("=========================================================");
            Console.WriteLine("                 Add Movie to Database                   ");
            Console.WriteLine("=========================================================");
            Console.WriteLine();
        }
    }
}
