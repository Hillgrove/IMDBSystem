using ConsoleUI.Helpers;
using Data;

namespace ConsoleUI
{
    internal class UI
    {
        private readonly ITitleRepository _titleRepository;
        private readonly INameRepository _nameRepository;

        private readonly TitleSearchMenu _titleSearchMenu;
        private readonly NameSearchMenu _nameSearchMenu;
        private readonly AddTitleMenu _addTitleMenu;
        private readonly AddNameMenu _addNameMenu;

        public UI(ITitleRepository titleRepository, INameRepository nameRepository)
        {
            _titleRepository = titleRepository;
            _nameRepository = nameRepository;

            _titleSearchMenu = new TitleSearchMenu(_titleRepository);
            _nameSearchMenu = new NameSearchMenu(_nameRepository);
            _addTitleMenu = new AddTitleMenu(_titleRepository);
            _addNameMenu = new AddNameMenu(_nameRepository);
        }

        public void Start()
        {
            while (true)
            {
                Console.Clear();
                PrintMainMenu();

                string? choice = Console.ReadLine()?.Trim().ToLower();
                Console.WriteLine();

                switch (choice)
                {
                    case "1":
                        _titleSearchMenu.Execute();
                        break;

                    case "2":
                        _nameSearchMenu.Execute();
                        break;

                    case "3":
                        _addTitleMenu.Execute();
                        break;

                    case "4":
                        _addNameMenu.Execute();
                        break;

                    case "q":
                        Console.WriteLine("\nThanks for using the Magical IMDB System... Stay safe");
                        return;
                }
            }
        }
 

        private void PrintMainMenu()
        {
            ConsoleFormatter.DisplayMenuHeader("Welcome to the Magical IMDB System");

            Console.WriteLine($" [1] Search title by wildcard and update/delete found title");
            Console.WriteLine($" [2] Search person by wildcard");
            Console.WriteLine($" [3] Add title to database");
            Console.WriteLine($" [4] Add person to database");

            Console.WriteLine($" [Q] Quit");

            Console.WriteLine("\n======================================================================");
            Console.WriteLine();
            Console.Write("Please enter action: ");
        }
    }
}
