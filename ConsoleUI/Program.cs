using ConsoleUI;
using Data;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

var vaultUri = new Uri("https://hillvault.vault.azure.net/");
var client = new SecretClient(vaultUri, new DefaultAzureCredential());
KeyVaultSecret secret = client.GetSecret("Obl-SQL-ConnectionString");
string connectionString = secret.Value;

//ITitleRepository titleRepository = TitleRepositoryList.Instance;
ITitleRepository titleRepository = new TitleRepositorySql(connectionString);

//INameRepository nameRepository = NameRepositoryList.Instance;
INameRepository nameRepository = new NameRepositorySql(connectionString);


UI SupercalifragilisticexpialidociousUserInterfaceDeluxe = new UI(titleRepository, nameRepository);
SupercalifragilisticexpialidociousUserInterfaceDeluxe.Start();
