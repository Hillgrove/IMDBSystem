using Data;
using Data.Models;

namespace ConsoleUI
{
    internal class PersonSearchMenu
    {
        private int _pageSize = 10;
        private int _currentPage = 0;
        private List<Name> _names = new List<Name>();

        private readonly INameRepository _repository;

        public PersonSearchMenu(INameRepository repository)
        {
            _repository = repository;
        }

        public void Execute()
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

                _names = _repository.GetPersons(wildcard);
                if (_names.Count == 0)
                {
                    if (!HandleNoPersonsFound()) return;
                    continue;
                }

                _currentPage = 0;
                while (true)
                {
                    DisplayResultsPage(_names, wildcard);
                    string? navChoice = GetNavigationInput();

                    bool shouldQuit = HandleNavigationChoice(navChoice);
                    if (shouldQuit)
                        return;

                    if (navChoice == "b")
                        break;
                }
            }
        }

        private void DisplayMenuHeader()
        {
            Console.WriteLine();
            Console.WriteLine("=========================================================");
            Console.WriteLine("                       Search Persons                    ");
            Console.WriteLine("=========================================================");
            Console.WriteLine();
        }

        private string GetSearchInput()
        {
            Console.Write("Enter search parameter (e.g. \"Keanu\"): ");
            return Console.ReadLine()?.Trim().ToLower() ?? "";
        }

        private void DisplayInvalidInputMessage()
        {
            Console.WriteLine("\nInvalid input. Returning to menu...");
            Console.ReadKey();
        }

        private bool HandleNoPersonsFound()
        {
            Console.WriteLine("\nNo persons found with the given name.");
            Console.WriteLine();
            Console.WriteLine("[R] Retry Search   [Q] Quit to Main Menu");
            string? retryChoice = Console.ReadLine()?.Trim().ToLower();

            return retryChoice != "q";
        }

        private void DisplayResultsPage(List<Name> persons, string wildcard)
        {
            Console.Clear();
            int startIndex = _currentPage * _pageSize;
            int endIndex = Math.Min(startIndex + _pageSize, persons.Count);

            Console.WriteLine();
            Console.WriteLine("=========================================================");
            Console.WriteLine($"                   Results for: \"{wildcard}\"                   ");
            Console.WriteLine($"                       Page {_currentPage + 1} of {Math.Ceiling((double)persons.Count / _pageSize)}");
            Console.WriteLine("=========================================================");
            Console.WriteLine();

            for (int i = startIndex; i < endIndex; i++)
            {
                Console.WriteLine($"[{i + 1}] {persons[i].PrimaryName}");
            }

            Console.WriteLine("\nPage Navigation:");
            Console.WriteLine("Enter the number to select a movie.");
            Console.WriteLine("[N] Next Page  [P] Previous Page  [B] Back to Search  [Q] Quit to Main Menu");
        }

        private string? GetNavigationInput()
        {
            Console.Write("\nYour choice: ");
            return Console.ReadLine()?.Trim().ToLower();
        }

        private bool HandleNavigationChoice(string navChoice)
        {
            if (int.TryParse(navChoice, out int selection))
            {
                if (selection > 0 && selection <= _names.Count)
                {
                    HandlePersonSelection(selection);
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
                    if ((_currentPage + 1) * _pageSize < _names.Count) _currentPage++;
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

        private void HandlePersonSelection(int selection)
        {
            Console.WriteLine($"\nYou selected: {_names[selection - 1]}");
            Console.Write("\nPress any key to return to the search menu...");
            Console.ReadKey();
        }
    }
}
