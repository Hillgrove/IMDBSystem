using Data;

namespace ConsoleUI
{
    internal class UI
    {
        private readonly ITitleRepository _titleRepository;
        private readonly INameRepository _nameRepository;

        private readonly TitleSearchMenu _titleSearchMenu;
        private readonly PersonSearchMenu _personSearchMenu;
        private readonly AddTitleMenu _addTitleMenu;
        private readonly AddNameMenu _addNameMenu;

        public UI(ITitleRepository titleRepository, INameRepository nameRepository)
        {
            _titleRepository = titleRepository;
            _nameRepository = nameRepository;

            _titleSearchMenu = new TitleSearchMenu(_titleRepository);
            _personSearchMenu = new PersonSearchMenu(_nameRepository);
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
                        _personSearchMenu.Execute();
                        break;

                    case "3":
                        Console.WriteLine("Not Implemented yet");
                        Console.Write("Press any key to continue...");
                        Console.ReadKey();
                        break;

                    case "4":
                        Console.WriteLine("Not Implemented yet");
                        Console.Write("Press any key to continue...");
                        Console.ReadKey();
                        break;

                    case "5":
                        _addTitleMenu.Execute();
                        break;

                    case "6":
                        _addNameMenu.Execute();
                        break;

                    case "7":
                        //UpdateOrDeleteTitleInformation();
                        break;


                    case "q":
                        Console.WriteLine("\nThanks for using the Magical IMDB System... Stay safe");
                        return;
                }
            }
        }
 

        private void PrintMainMenu()
        {
            Console.WriteLine();
            Console.WriteLine("=========================================================");
            Console.WriteLine("            Welcome to the Magical IMDB System           ");
            Console.WriteLine("=========================================================");
            Console.WriteLine();

            Console.WriteLine($"  [1] Search title by wildcard");
            Console.WriteLine($"  [2] Search person by wildcard");
            Console.WriteLine($"  [3] (Ekstra) See all information about specific title");
            Console.WriteLine($"  [4] (Ekstra) See all information about a specific person");
            Console.WriteLine($"  [5] Add title to database");
            Console.WriteLine($"  [6] Add person to database");
            Console.WriteLine($"  [7] Update and/or delete title information");

            Console.WriteLine($"  [Q] Quit");

            Console.WriteLine("\n=========================================================");
            Console.WriteLine();
            Console.Write("Please enter action: ");
        }
    }
}
