using Data;
using Data.Models;

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
                Console.Clear();
                DisplayMenuHeader();

                string wildcard = GetSearchInput();
                if (string.IsNullOrEmpty(wildcard))
                {
                    DisplayInvalidInputMessage();
                    continue;
                }

                _titles = _repository.GetTitles(wildcard);
                if (_titles.Count == 0)
                {
                    if (!HandleNoTitlesFound()) return;
                    continue;
                }

                _currentPage = 0;
                while (true)
                {
                    DisplayResultsPage(_titles, wildcard);
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
            Console.WriteLine("                  Search Title By Name                   ");
            Console.WriteLine("=========================================================");
            Console.WriteLine();
        }

        private string GetSearchInput()
        {
            Console.Write("Enter search parameter (e.g. \"The Godfather\"): ");
            return Console.ReadLine()?.Trim().ToLower() ?? "";
        }

        private void DisplayInvalidInputMessage()
        {
            Console.WriteLine("\nInvalid input. Returning to menu...");
            Console.ReadKey();
        }

        private bool HandleNoTitlesFound()
        {
            Console.WriteLine("\nNo titles found with the given name.");
            Console.WriteLine();
            Console.WriteLine("[R] Retry Search   [Q] Quit to Main Menu");
            string? retryChoice = Console.ReadLine()?.Trim().ToLower();

            return retryChoice != "q";
        }

        private void DisplayResultsPage(List<Title> titles, string wildcard)
        {
            Console.Clear();
            int startIndex = _currentPage * _pageSize;
            int endIndex = Math.Min(startIndex + _pageSize, titles.Count);

            Console.WriteLine();
            Console.WriteLine("=========================================================");
            Console.WriteLine($"                   Results for: \"{wildcard}\"                   ");
            Console.WriteLine($"                       Page {_currentPage + 1} of {Math.Ceiling((double)titles.Count / _pageSize)}");
            Console.WriteLine("=========================================================");
            Console.WriteLine();

            for (int i = startIndex; i < endIndex; i++)
            {
                string displayTitle = titles[i].PrimaryTitle;
                int maxWidth = 50; // Adjusted width to accommodate the index and brackets
                if (displayTitle.Length > maxWidth)
                {
                    displayTitle = displayTitle.Substring(0, maxWidth - 3) + "...";
                }

                Console.WriteLine($"[{i + 1}] {displayTitle}");
            }

            Console.WriteLine("\nPage Navigation:");
            Console.WriteLine("Enter the number to select a title.");
            Console.WriteLine();
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
                    if ((_currentPage + 1) * _pageSize < _titles.Count) _currentPage++;
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
            Console.Clear();

            PrintCenteredHeader(selectedTitle.PrimaryTitle);

            PrintWrappedLine("Title Type      : " + selectedTitle.Type.Name);
            PrintWrappedLine("Primary Title   : " + selectedTitle.PrimaryTitle);
            PrintWrappedLine("Original Title  : " + selectedTitle.OriginalTitle);
            PrintWrappedLine("Adult Content   : " + (selectedTitle.IsAdult ? "Yes" : "No"));
            PrintWrappedLine("Release Year    : " + (selectedTitle.StartYear.HasValue ? selectedTitle.StartYear.ToString() : "Unknown"));
            PrintWrappedLine("End Year        : " + (selectedTitle.EndYear.HasValue ? selectedTitle.EndYear.ToString() : "N/A"));
            PrintWrappedLine("Runtime         : " + (selectedTitle.RuntimeMinutes.HasValue ? $"{selectedTitle.RuntimeMinutes} minutes" : "N/A"));
            PrintWrappedLine("Genres          : " + (selectedTitle.Genres.Count > 0 ? string.Join(", ", selectedTitle.Genres.Select(g => g.Name)) : "None"));

            Console.WriteLine();
            Console.WriteLine("=========================================================");
            Console.Write("\nPress any key to return to the search menu...");
            Console.ReadKey();
        }


        private void PrintCenteredHeader(string title)
        {
            int totalWidth = 57;
            int maxTitleWidth = totalWidth - 2; // To account for padding

            // Break long titles into multiple lines
            var lines = new List<string>();
            while (title.Length > maxTitleWidth)
            {
                int breakIndex = title.LastIndexOf(' ', maxTitleWidth);
                if (breakIndex == -1) breakIndex = maxTitleWidth;
                lines.Add(title.Substring(0, breakIndex));
                title = title.Substring(breakIndex).TrimStart();
            }
            lines.Add(title); // Add the remaining part

            Console.WriteLine();
            Console.WriteLine("=========================================================");

            foreach (string line in lines)
            {
                int padding = (totalWidth - line.Length) / 2;
                Console.WriteLine(line.PadLeft(padding + line.Length).PadRight(totalWidth));
            }

            Console.WriteLine("=========================================================");
            Console.WriteLine();
        }

        private void PrintWrappedLine(string text, int maxWidth = 55)
        {
            int labelWidth = text.IndexOf(':') + 2; // Find the position after the colon and space
            int availableWidth = maxWidth - labelWidth;

            // Print the first line directly
            if (text.Length <= maxWidth)
            {
                Console.WriteLine(text);
                return;
            }

            int breakIndex = text.LastIndexOf(' ', maxWidth);
            if (breakIndex == -1) breakIndex = maxWidth;
            Console.WriteLine(text.Substring(0, breakIndex));
            text = text.Substring(breakIndex).TrimStart();

            // Print remaining lines with alignment
            while (text.Length > 0)
            {
                breakIndex = text.Length > availableWidth ? text.LastIndexOf(' ', availableWidth) : text.Length;
                if (breakIndex == -1) breakIndex = availableWidth;
                Console.WriteLine(new string(' ', labelWidth) + text.Substring(0, breakIndex).Trim());
                text = text.Substring(breakIndex).TrimStart();
            }
        }
    }
}
