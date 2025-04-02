using ConsoleUI;
using Data;
using Microsoft.Extensions.Configuration;

var config = new ConfigurationBuilder()
    .AddUserSecrets(System.Reflection.Assembly.GetExecutingAssembly())
    .Build();

string connectionString = config.GetConnectionString("Default")
    ?? throw new InvalidOperationException("Connection string 'Default' not found in user secrets.");

//string connectionString = "Data Source=mssql11.unoeuro.com;Initial Catalog=hillgrove_dk_db_datamatiker;User ID=hillgrove_dk;Password=9coz8Awpel0Jo5dEvA9jXQiNRQseko2quzus;Encrypt=True;TrustServerCertificate=True;";



ITitleRepository titleRepository = TitleRepositoryList.Instance;
//ITitleRepository repository = new TitleRepositorySql("YourConnectionString");

//INameRepository nameRepository = NameRepositoryList.Instance;
INameRepository nameRepository = new NameRepositorySql(connectionString);


UI SupercalifragilisticexpialidociousUserInterfaceDeluxe = new UI(titleRepository, nameRepository);
SupercalifragilisticexpialidociousUserInterfaceDeluxe.Start();
