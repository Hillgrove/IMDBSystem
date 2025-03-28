using ConsoleUI;
using Data;

IMovieRepository movieRepository = MovieRepositoryList.Instance;
// IMovieRepository repository = new MovieRepositorySql("YourConnectionString");

INameRepository nameRepository = NameRepositoryList.Instance;
// INameRepository nameRepository = new NameRepositoryListSql("YourConnectionString");


UI SupercalifragilisticexpialidociousUserInterfaceDeluxe = new UI(movieRepository, nameRepository);
SupercalifragilisticexpialidociousUserInterfaceDeluxe.Start();
