using Data;

namespace ConsoleUI
{
    internal static class MovieSearchMenu
    {
        private static int _pageSize = 10;
        private static int _currentPage = 0;
        private static List<string> _movies = new List<string>();

        public static void Execute()
        {
            while (true)
            {
                Console.Clear();
                DisplayMenuHeader();

                string wildcard = GetSearchInput();
                if (string.IsNullOrEmpty(wildcard))
                {
                    DisplayInvalidInputMessage();
                    continue;
                }

                _movies = MockMovieDatabase.GetMovies(wildcard);
                if (_movies.Count == 0)
                {
                    if (!HandleNoMoviesFound()) return;
                    continue;
                }

                _currentPage = 0;
                while (true)
                {
                    DisplayResultsPage(_movies, wildcard);
                    string? navChoice = GetNavigationInput();

                    bool shouldQuit = HandleNavigationChoice(navChoice);
                    if (shouldQuit)
                        return;

                    if (navChoice == "b")
                        break;
                }
            }
        }

        private static void DisplayMenuHeader()
        {
            Console.WriteLine();
            Console.WriteLine("=========================================================");
            Console.WriteLine("                  Search Movie By Title                  ");
            Console.WriteLine("=========================================================");
            Console.WriteLine();
        }

        private static string GetSearchInput()
        {
            Console.Write("Enter search parameter (e.g. \"The Godfather\"): ");
            return Console.ReadLine()?.Trim().ToLower() ?? "";
        }

        private static void DisplayInvalidInputMessage()
        {
            Console.WriteLine("\nInvalid input. Returning to menu...");
            Console.ReadKey();
        }

        private static bool HandleNoMoviesFound()
        {
            Console.WriteLine("\nNo movies found with the given title.");
            Console.WriteLine();
            Console.WriteLine("[R] Retry Search   [Q] Quit to Main Menu");
            string? retryChoice = Console.ReadLine()?.Trim().ToLower();

            return retryChoice != "q";
        }

        private static void DisplayResultsPage(List<string> movies, string wildcard)
        {
            Console.Clear();
            int startIndex = _currentPage * _pageSize;
            int endIndex = Math.Min(startIndex + _pageSize, movies.Count);

            Console.WriteLine();
            Console.WriteLine("=========================================================");
            Console.WriteLine($"                   Results for: \"{wildcard}\"                   ");
            Console.WriteLine($"                       Page {_currentPage + 1} of {Math.Ceiling((double)movies.Count / _pageSize)}");
            Console.WriteLine("=========================================================");
            Console.WriteLine();

            for (int i = startIndex; i < endIndex; i++)
            {
                Console.WriteLine($"[{i + 1}] {movies[i]}");
            }

            Console.WriteLine("\nPage Navigation:");
            Console.WriteLine("Enter the number to select a movie.");
            Console.WriteLine("[N] Next Page  [P] Previous Page  [B] Back to Search  [Q] Quit to Main Menu");
        }

        private static string? GetNavigationInput()
        {
            Console.Write("\nYour choice: ");
            return Console.ReadLine()?.Trim().ToLower();
        }

        private static bool HandleNavigationChoice(string navChoice)
        {
            if (int.TryParse(navChoice, out int selection))
            {
                if (selection > 0 && selection <= _movies.Count)
                {
                    HandleMovieSelection(selection);
                }
                else
                {
                    Console.WriteLine("\nInvalid selection.");
                    Console.ReadKey();
                }
                return false;
            }

            switch (navChoice)
            {
                case "n":
                    if ((_currentPage + 1) * _pageSize < _movies.Count) _currentPage++;
                    break;
                case "p":
                    if (_currentPage > 0) _currentPage--;
                    break;
                case "b":
                    return false;
                case "q":
                    return true;
                default:
                    Console.Write("\nInvalid choice. Press any key...");
                    Console.ReadKey();
                    break;
            }
            return false;
        }

        private static void HandleMovieSelection(int selection)
        {
                Console.WriteLine($"\nYou selected: {_movies[selection - 1]}");
                Console.Write("\nPress any key to return to the search menu...");
                Console.ReadKey();
        }
    }
}
