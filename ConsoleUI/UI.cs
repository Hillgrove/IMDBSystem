using Data;

namespace ConsoleUI
{
    internal class UI
    {
        private readonly IMovieRepository _movieRepository;
        private readonly INameRepository _nameRepository;

        private readonly TitleSearchMenu _movieSearchMenu;
        private readonly PersonSearchMenu _personSearchMenu;
        private readonly AddTitleMenu _addMovieMenu;

        public UI(IMovieRepository movieRepository, INameRepository nameRepository)
        {
            _movieRepository = movieRepository;
            _nameRepository = nameRepository;

            _movieSearchMenu = new TitleSearchMenu(_movieRepository);
            _personSearchMenu = new PersonSearchMenu(_nameRepository);
            _addMovieMenu = new AddTitleMenu(_movieRepository);
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
                        _movieSearchMenu.Execute();
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
                        _addMovieMenu.Execute();
                        break;

                    case "6":
                        //AddPersonToDB();
                        break;

                    case "7":
                        //UpdateOrDeleteMovieInformation();
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

            Console.WriteLine($"  [1] Search movie title by wildcard");
            Console.WriteLine($"  [2] Search person by wildcard");
            Console.WriteLine($"  [3] (Ekstra) See all information about specific movie");
            Console.WriteLine($"  [4] (Ekstra) See all information about a specific person");
            Console.WriteLine($"  [5] Add movie to database");
            Console.WriteLine($"  [6] Add person to database");
            Console.WriteLine($"  [7] Update and/or delete movie information");

            Console.WriteLine($"  [Q] Quit");

            Console.WriteLine("\n=========================================================");
            Console.WriteLine();
            Console.Write("Please enter action: ");
        }
    }
}
