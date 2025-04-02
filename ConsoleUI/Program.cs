using ConsoleUI;
using Data;
using Microsoft.Extensions.Configuration;

var config = new ConfigurationBuilder()
    .AddUserSecrets(System.Reflection.Assembly.GetExecutingAssembly())
    .Build();

string connectionString = config.GetConnectionString("Default")
    ?? throw new InvalidOperationException("Connection string 'Default' not found in user secrets.");


ITitleRepository titleRepository = TitleRepositoryList.Instance;
//ITitleRepository repository = new TitleRepositorySql("YourConnectionString");

//INameRepository nameRepository = NameRepositoryList.Instance;
INameRepository nameRepository = new NameRepositorySql(connectionString);


UI SupercalifragilisticexpialidociousUserInterfaceDeluxe = new UI(titleRepository, nameRepository);
SupercalifragilisticexpialidociousUserInterfaceDeluxe.Start();
