using ConsoleUI.Helpers;
using Data;
using Data.Models;
using System.Diagnostics;

namespace ConsoleUI
{
    public class TitleSearchMenu
    {
        private int _pageSize = 10;
        private int _currentPage = 0;
        private List<Title> _titles = new List<Title>();

        private readonly ITitleRepository _repository;

        public TitleSearchMenu(ITitleRepository repository)
        {
            _repository = repository;
        }

        public void Execute()
        {
            while (true)
            {
                IMDBType? selectedType = GetOptionalTitleType();
                string wildcard = GetSearchString();

                if (string.IsNullOrEmpty(wildcard))
                {
                    DisplayInvalidInputMessage();
                    continue;
                }

                _currentPage = 0;
                
                while (true)
                {
                    int offset = _currentPage * _pageSize;
                    
                    Stopwatch stopwatch = Stopwatch.StartNew();                 
                    RunWithSpinner(() =>
                    {
                        _titles = _repository.GetTitles(wildcard, _currentPage * _pageSize, _pageSize, selectedType);
                    });
                    stopwatch.Stop();

                    long elapsedMs = stopwatch.ElapsedMilliseconds;

                    if (_titles.Count == 0)
                    {
                        if (_currentPage == 0)
                        {
                            if (!HandleNoTitlesFound()) return;
                            break;
                        }
                        else
                        {
                            Console.WriteLine("\nNo more results.");
                            Console.ReadKey();
                            _currentPage--;
                            continue;
                        }
                    }

                    DisplayResultsPage(_titles, wildcard, elapsedMs, selectedType);
                    
                    string? navChoice = GetNavigationInput();
                    bool shouldQuit = HandleNavigationChoice(navChoice);
                    
                    if (shouldQuit)
                        return;

                    if (navChoice == "b")
                        break;
                }
            }
        }

        private IMDBType? GetOptionalTitleType()
        {
            var types = _repository.GetTypes();

            while (true)
            {
                Console.Clear();
                ConsoleFormatter.DisplayMenuHeader("Titles Search");
                Console.WriteLine("Optional: Select Title Type (or press Enter to search all types):");
                Console.WriteLine();

                for (int i = 0; i < types.Count; i++)
                    Console.WriteLine($" [{i + 1}] {types[i].Name}");

                Console.Write("\nYour choice: ");
                string? input = Console.ReadLine()?.Trim();

                if (string.IsNullOrEmpty(input))
                    return null;

                if (int.TryParse(input, out int index) && index >= 1 && index <= types.Count)
                    return types[index - 1];

                Console.WriteLine("Invalid input. Press any key to retry...");
                Console.ReadKey();
            }
        }

        private string GetSearchString()
        {
            Console.Clear();
            ConsoleFormatter.DisplayMenuHeader("Titles Search");

            Console.Write("Enter search parameter (e.g. \"The Godfather\"): ");
            return Console.ReadLine()?.Trim().ToLower() ?? "";
        }

        private void DisplayResultsPage(List<Title> titles, string wildcard, long elapsedMs, IMDBType? selectedType)
        {
            string typeLabel = selectedType != null ? $" (type: {selectedType.Name})" : "";
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine("======================================================================");           
            Console.WriteLine($"     Results for: \"{wildcard}\"{typeLabel}");
            Console.WriteLine($"     Page {_currentPage + 1}   ({elapsedMs} ms)");
            Console.WriteLine("======================================================================");
            Console.WriteLine();

            Console.WriteLine(" #   Title                                Type       Year    Minutes");
            Console.WriteLine("----------------------------------------------------------------------");

            for (int i = 0; i < titles.Count; i++)
            {
                var t = titles[i];
                string title = t.PrimaryTitle.Length > 36 ? t.PrimaryTitle[..33] + "..." : t.PrimaryTitle;
                string type = t.Type?.Name ?? "unknown";
                string year = t.StartYear?.ToString() ?? "????";
                string runtime = t.RuntimeMinutes.HasValue ? $"{t.RuntimeMinutes}" : "N/A";

                Console.WriteLine($"[{(i + 1),-2}] {title,-36} {type,-10} {year,-5} {runtime,7}");
            }

            Console.WriteLine("\nEnter the number to select a title.");
            Console.WriteLine();
            Console.WriteLine("[N] Next Page  [P] Previous Page  [B] Back to Search  [Q] Quit to Main Menu");
        }
        
        private bool HandleNoTitlesFound()
        {
            Console.WriteLine("\nNo titles found with the given name.");
            Console.WriteLine();
            Console.WriteLine("[R] Retry Search   [Q] Quit to Main Menu");
            string? retryChoice = Console.ReadLine()?.Trim().ToLower();

            return retryChoice != "q";
        }

        private void DisplayInvalidInputMessage()
        {
            Console.WriteLine("\nInvalid input. Try again...");
            Console.ReadKey();
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
                if (selection > 0 && selection <= _titles.Count)
                {
                    HandleTitleSelection(selection);
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
                    _currentPage++;
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

        private void HandleTitleSelection(int selection)
        {
            Title selectedTitle = _titles[selection - 1];
            
            while (true)
            {
                Console.Clear();

                ConsoleFormatter.DisplayMenuHeader(selectedTitle.PrimaryTitle);

                ConsoleFormatter.PrintWrappedLine("Title Type      : " + selectedTitle.Type.Name);
                ConsoleFormatter.PrintWrappedLine("Primary Title   : " + selectedTitle.PrimaryTitle);
                ConsoleFormatter.PrintWrappedLine("Original Title  : " + selectedTitle.OriginalTitle);
                ConsoleFormatter.PrintWrappedLine("Adult Content   : " + (selectedTitle.IsAdult ? "Yes" : "No"));
                ConsoleFormatter.PrintWrappedLine("Release Year    : " + (selectedTitle.StartYear.HasValue ? selectedTitle.StartYear.ToString() : "Unknown"));
                ConsoleFormatter.PrintWrappedLine("End Year        : " + (selectedTitle.EndYear.HasValue ? selectedTitle.EndYear.ToString() : "N/A"));
                ConsoleFormatter.PrintWrappedLine("Runtime         : " + (selectedTitle.RuntimeMinutes.HasValue ? $"{selectedTitle.RuntimeMinutes} minutes" : "N/A"));
                ConsoleFormatter.PrintWrappedLine("Genres          : " + (selectedTitle.Genres.Count > 0 ? string.Join(", ", selectedTitle.Genres.Select(g => g.Name)) : "None"));

                Console.WriteLine();
                Console.WriteLine("[U] Update title   [D] Delete title   [B] Back");
                Console.Write("\nYour choice: ");
                string? choice = Console.ReadLine()?.Trim().ToLower();

                switch (choice)
                {
                    case "u":
                        var updated = PromptForTitleUpdate(selectedTitle);
                        _repository.UpdateTitle(selectedTitle, updated);

                        int index = _titles.IndexOf(selectedTitle);
                        if (index >= 0)
                            _titles[index] = updated;

                        selectedTitle = updated;
                        break;

                    case "d":
                        Console.Write("Are you sure you want to delete this title? [Y/N]: ");
                        var confirm = Console.ReadLine()?.Trim().ToLower();
                        if (confirm == "y")
                        {
                            _repository.DeleteTitle(selectedTitle);
                            _titles.Remove(selectedTitle);
                            Console.Write("Title deleted. Press any key to continue...");
                            Console.ReadKey();
                            return;
                        }
                        break;

                    case "b":
                        return;

                    default:
                        Console.Write("Invalid choice. Press any key to try again...");
                        Console.ReadKey();
                        break;
                }
            }
            
        }

        private Title PromptForTitleUpdate(Title old)
        {
            var types = _repository.GetTypes();
            var genres = _repository.GetGenres();

            // Kopiér eksisterende værdier
            var updated = new Title
            {
                Type = old.Type,
                PrimaryTitle = old.PrimaryTitle,
                OriginalTitle = old.OriginalTitle,
                IsAdult = old.IsAdult,
                StartYear = old.StartYear,
                EndYear = old.EndYear,
                RuntimeMinutes = old.RuntimeMinutes,
                Genres = old.Genres.ToList()
            };

            updated.Type = PromptSelect("Select new type", types, updated.Type, old.PrimaryTitle);
            updated.PrimaryTitle = PromptString("Primary Title", updated.PrimaryTitle, old.PrimaryTitle);
            updated.OriginalTitle = PromptString("Original Title", updated.OriginalTitle, old.PrimaryTitle);
            updated.IsAdult = PromptBool("Is Adult", updated.IsAdult, old.PrimaryTitle);
            updated.StartYear = PromptInt("Start Year", updated.StartYear, old.PrimaryTitle);
            updated.EndYear = PromptInt("End Year", updated.EndYear, old.PrimaryTitle);
            updated.RuntimeMinutes = PromptInt("Runtime (minutes)", updated.RuntimeMinutes, old.PrimaryTitle);
            updated.Genres = PromptMultiSelect("Select genres (max 3)", genres, updated.Genres, 3, old.PrimaryTitle);

            return updated;
        }

        private T PromptSelect<T>(string label, List<T> options, T current, string titleName) where T : class
        {
            while (true)
            {
                ConsoleFormatter.DisplayMenuHeader($"Updating {titleName}");
                Console.WriteLine("Press Enter without input to keep current value.");
                Console.WriteLine();
                Console.WriteLine($"{label} (current: {current})");

                for (int i = 0; i < options.Count; i++)
                    Console.WriteLine($"[{i + 1}] {options[i]}");

                Console.WriteLine();
                Console.Write("Choice: ");
                string? input = Console.ReadLine()?.Trim();

                if (string.IsNullOrEmpty(input))
                    return current;

                if (int.TryParse(input, out int index) && index >= 1 && index <= options.Count)
                    return options[index - 1];

                Console.WriteLine("Invalid input. Press any key to try again...");
                Console.ReadKey();
            }
        }

        private string PromptString(string label, string current, string titleName)
        {
            ConsoleFormatter.DisplayMenuHeader($"Updating {titleName}");
            Console.WriteLine("Press Enter without input to keep current value.");
            Console.WriteLine();
            Console.Write($"{label} (current: \"{current}\"): ");
            string? input = Console.ReadLine()?.Trim();
            return string.IsNullOrEmpty(input) ? current : input;
        }

        private bool PromptBool(string label, bool current, string titleName)
        {
            while (true)
            {
                ConsoleFormatter.DisplayMenuHeader($"Updating {titleName}");
                Console.WriteLine("Press Enter without input to keep current value.");
                Console.WriteLine();
                Console.Write($"{label} (current: {(current ? "Y" : "N")}) [Y/N]: ");

                string? input = Console.ReadLine()?.Trim().ToLower();

                if (string.IsNullOrEmpty(input))
                    return current;

                if (input == "y") return true;
                if (input == "n") return false;
            }
        }

        private int? PromptInt(string label, int? current, string titleName)
        {
            ConsoleFormatter.DisplayMenuHeader($"Updating {titleName}");
            Console.WriteLine("Press Enter without input to keep current value.");
            Console.WriteLine();
            Console.WriteLine($"Updating: {(current.HasValue ? current.ToString() : "N/A")}");
            Console.WriteLine();
            Console.Write($"{label} (current: {(current.HasValue ? current.ToString() : "N/A")}): ");
            string? input = Console.ReadLine()?.Trim();
            if (string.IsNullOrEmpty(input)) return current;
            if (int.TryParse(input, out int result)) return result;

            Console.WriteLine("Invalid input. Press any key to keep current.");
            Console.ReadKey();
            return current;
        }

        private List<Genre> PromptMultiSelect(string label, List<Genre> allGenres, List<Genre> current, int max, string titleName)
        {
            var selected = new List<Genre>();
            while (selected.Count < max)
            {
                ConsoleFormatter.DisplayMenuHeader($"Updating {titleName}");
                Console.WriteLine($"{label} (current: {string.Join(", ", current.Select(g => g.Name))})");
                Console.WriteLine();

                for (int i = 0; i < allGenres.Count; i++)
                    Console.WriteLine($"[{i + 1}] {allGenres[i].Name}");

                Console.Write($"Select genre {selected.Count + 1} (or Enter to finish): ");
                string? input = Console.ReadLine()?.Trim();
                if (string.IsNullOrEmpty(input)) break;

                if (int.TryParse(input, out int choice) && choice >= 1 && choice <= allGenres.Count)
                {
                    Genre g = allGenres[choice - 1];
                    if (!selected.Contains(g))
                        selected.Add(g);
                }
            }

            return selected.Count > 0 ? selected : current;
        }

        private void RunWithSpinner(Action action)
        {
            using var spinnerCts = new CancellationTokenSource();

            var spinnerThread = new Thread(() =>
            {
                string[] sequence = { "|", "/", "-", "\\" };
                int i = 0;
                while (!spinnerCts.Token.IsCancellationRequested)
                {
                    Console.Write($"\rSearching... {sequence[i++ % sequence.Length]}");
                    Thread.Sleep(100);
                }
                Console.Write("\rSearching... done.       \n");
            });

            spinnerThread.Start();
            action();
            spinnerCts.Cancel();
            spinnerThread.Join();
        }


    }
}
