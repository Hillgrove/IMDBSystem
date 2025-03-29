using Data;
using Data.Models;

namespace ConsoleUI
{
    internal class AddNameMenu
    {
        private readonly INameRepository _repository;

        public AddNameMenu(INameRepository repository)
        {
            _repository = repository;
        }

        public void Execute()
        {
            while (true)
            {
                Console.Clear();
                DisplayMenuHeader();

                // Primary Name
                var primaryName = GetNameInput("Primary Name");
                if (primaryName == null)
                    continue;

                // Birth Year
                int birthYear = GetYearInput("Year of birth")!.Value;

                // Death Year
                int? deathYear = GetYearInput("Year of death", birthYear);

                var newName = new Name
                {
                    PrimaryName = primaryName,
                    BirthYear = birthYear,
                    DeathYear = deathYear
                };

                while (true)
                {
                    Console.Clear();
                    DisplayMenuHeader();
                    Console.WriteLine("Name to be added:");
                    Console.WriteLine(newName);

                    if (GetYesNoInput("Accept? [Y] Yes / [N] No", newName))
                    {
                        _repository.AddName(newName);
                        Console.Clear();
                        DisplayMenuHeader();
                        Console.WriteLine("Name added successfully!");
                        break;
                    }

                    else
                    {
                        Console.Clear();
                        DisplayMenuHeader();
                        Console.WriteLine("Operation cancelled.");
                        break;
                    }
                }

                if (!GetYesNoInput("Add another name? [Y] Yes / [N] No"))
                    return;
            }
        }

        private string? GetNameInput(string prompt)
        {
            Console.Write($"{prompt}: ");
            string input = Console.ReadLine()?.Trim() ?? "";
            if (string.IsNullOrEmpty(input))
            {
                Console.WriteLine("Input cannot be empty. Press any key to retry...");
                Console.ReadKey();
                return null;
            }
            Console.Clear();
            DisplayMenuHeader();
            return input;
        }

        private int? GetYearInput(string prompt, int? minYear = null)
        {
            while (true)
            {
                Console.Write($"{prompt}{(minYear.HasValue ? " (optional)" : "")}: ");
                string? input = Console.ReadLine()?.Trim();

                if (string.IsNullOrEmpty(input))
                {
                    if (!minYear.HasValue)
                    {
                        Console.Clear();
                        DisplayMenuHeader();
                        Console.WriteLine("Year cannot be empty.");
                        continue;
                    }

                    Console.Clear();
                    DisplayMenuHeader();
                    return null;
                }

                if (int.TryParse(input, out int year) &&
                    year >= (minYear ?? 1800) &&
                    year <= DateTime.Now.Year)
                {
                    Console.Clear();
                    DisplayMenuHeader();
                    return year;
                }

                Console.Clear();
                DisplayMenuHeader();
                Console.WriteLine($"Invalid input. Year must be between {(minYear ?? 1800)} and {DateTime.Now.Year}.");
            }
        }

        private bool GetYesNoInput(string prompt, Name? newName = null)
        {
            while (true)
            {
                Console.WriteLine();
                Console.Write($"{prompt}: ");
                string? input = Console.ReadLine()?.Trim().ToLower();

                if (input == "y")
                    return true;
                if (input == "n")
                    return false;

                // Clear the entire screen and reprint the menu and the current item
                Console.Clear();
                DisplayMenuHeader();
                if (newName != null)
                {
                    Console.WriteLine("Name to be added:");
                    Console.WriteLine(newName);  // Print the name object if available
                    Console.WriteLine();
                }
                Console.WriteLine("Invalid input. Please type 'Y' or 'N' and press Enter.");
            }
        }

        private void DisplayMenuHeader()
        {
            Console.WriteLine();
            Console.WriteLine("=========================================================");
            Console.WriteLine("                 Add Name to Database                    ");
            Console.WriteLine("=========================================================");
            Console.WriteLine();
        }
    }
}