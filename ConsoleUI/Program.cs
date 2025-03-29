using ConsoleUI;
using Data;

ITitleRepository titleRepository = TitleRepositoryList.Instance;
// ITitleRepository repository = new TitleRepositorySql("YourConnectionString");

INameRepository nameRepository = NameRepositoryList.Instance;
// INameRepository nameRepository = new NameRepositoryListSql("YourConnectionString");


UI SupercalifragilisticexpialidociousUserInterfaceDeluxe = new UI(titleRepository, nameRepository);
SupercalifragilisticexpialidociousUserInterfaceDeluxe.Start();
