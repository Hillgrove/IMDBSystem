using Data;
using Data.Models;

namespace ConsoleUI
{
    internal class AddTitleMenu
    {
        private readonly IMovieRepository _repository;

        public AddTitleMenu(IMovieRepository repository)
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

                // Start Tear
                int startYear = GetStartYear("Start Year");

                // End Year
                int? endYear = GetOptionalEndYear("End Year", startYear);

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

                Console.WriteLine("Movie to be added:");
                Console.WriteLine(newMovie);
                if (GetYesNoInput("Accept? [Y] Yes / [N] No"))
                {
                    _repository.AddMovie(newMovie);
                    Console.Clear();
                    DisplayMenuHeader();
                    Console.WriteLine("Movie added successfully!");
                }
                else
                {
                    Console.Clear();
                    DisplayMenuHeader();
                    Console.WriteLine("Operation cancelled.");
                }

                // Add Another or Go Back
                if (!GetYesNoInput("Add another movie? [Y] Yes / [N] No"))
                    return;
            }
        }

        private bool GetYesNoInput(string prompt)
        {
            Console.Write(prompt);
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
                    Console.Clear();
                    DisplayMenuHeader();
                    return types[choice - 1];
                }

                Console.Clear();
                DisplayMenuHeader();
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
            Console.Clear();
            DisplayMenuHeader();
            return input;
        }

        private bool GetAdultInput()
        {
            while (true)
            {
                Console.Write("Is Adult (press [T] for True, [F] for False): ");
                ConsoleKey key = Console.ReadKey().Key;
                if (key == ConsoleKey.T || key == ConsoleKey.F)
                {
                    Console.Clear();
                    DisplayMenuHeader();
                    return key == ConsoleKey.T;
                }

                Console.Clear();
                DisplayMenuHeader();
                Console.WriteLine("Invalid input. Please press 'T' or 'F'.");
            }
        }

        private int GetStartYear(string prompt)
        {
            while (true)
            {
                Console.Write($"{prompt}: ");
                if (int.TryParse(Console.ReadLine(), out int year) && year >= 1800 && year <= DateTime.Now.Year)
                {
                    Console.Clear();
                    DisplayMenuHeader();
                    return year;
                }

                Console.Clear();
                DisplayMenuHeader();
                Console.WriteLine("Invalid year. Please enter a value between 1800 and the current year.");
            }
        }

        private int? GetOptionalEndYear(string prompt, int startYear)
        {
            while (true)
            {
                Console.Write($"{prompt} (optional, must be >= {startYear}): ");
                string? input = Console.ReadLine()?.Trim();

                if (string.IsNullOrEmpty(input))
                {
                    Console.Clear();
                    DisplayMenuHeader();
                    return null;
                }

                if (int.TryParse(input, out int year) && year >= startYear)
                {
                    Console.Clear();
                    DisplayMenuHeader();
                    return year;
                }

                Console.Clear();
                DisplayMenuHeader();
                Console.WriteLine($"Invalid input. Please enter a valid year (>= {startYear}) or leave empty.");
            }
        }

        private int? GetOptionalRuntime(string prompt)
        {
            while (true)
            {
                Console.Write($"{prompt} (optional): ");
                string? input = Console.ReadLine()?.Trim();

                if (string.IsNullOrEmpty(input))
                {
                    Console.Clear();
                    DisplayMenuHeader();
                    return null;
                }

                if (int.TryParse(input, out int year))
                {
                    Console.Clear();
                    DisplayMenuHeader();
                    return year;
                }

                Console.Clear();
                DisplayMenuHeader();
                Console.WriteLine("\nInvalid input. Please enter a valid runtime or leave empty.");
            }
        }

        private List<Genre>? SelectGenres()
        {
            var genres = _repository.GetGenres();
            var selectedGenres = new List<Genre>();

            for (int selectionCount = 1; selectionCount <= 3; selectionCount++)
            {
                Console.Clear();
                DisplayMenuHeader();

                Console.WriteLine($"Select Genre {selectionCount} (or type 'C' to continue):");
                for (int i = 0; i < genres.Count; i++)
                {
                    Console.WriteLine($"[{i + 1}] {genres[i].Name}");
                }

                Console.Write("\nYour choice: ");
                string? input = Console.ReadLine()?.Trim().ToLower();

                if (input == "c")
                {
                    if (selectedGenres.Count > 0)
                        break;

                    Console.WriteLine("You must select at least one genre before continuing.");
                    Console.Write("Press any key to retry...");
                    Console.ReadKey();
                    continue;
                }

                if (int.TryParse(input, out int choice) && choice >= 1 && choice <= genres.Count)
                {
                    var genre = genres[choice - 1];

                    // Prevent duplicate selection
                    if (!selectedGenres.Contains(genre))
                    {
                        selectedGenres.Add(genre);
                    }
                    else
                    {
                        Console.Write("Genre already selected. Press any key to retry...");
                        Console.ReadKey();
                        selectionCount--; // Retry the same genre slot
                        continue;
                    }

                    // Allow continuing after selecting at least one genre
                    if (selectionCount == 3 || selectedGenres.Count == 3)
                        break;
                }
                else
                {
                    Console.WriteLine("Invalid genre choice. Press any key to retry...");
                    Console.ReadKey();
                    selectionCount--; // Retry the same genre slot
                }
            }

            Console.Clear();
            DisplayMenuHeader();
            return selectedGenres;
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
