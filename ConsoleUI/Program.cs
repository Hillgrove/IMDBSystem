using ConsoleUI;
using Data;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;

string serverName = "";
string database = "hillgrove_dk_db_datamatiker";
string userId = "";     
string password = "";

string connectionString;

if (!string.IsNullOrWhiteSpace(serverName) &&
    !string.IsNullOrWhiteSpace(userId) &&
    !string.IsNullOrWhiteSpace(password))
{
    // Use manually filled values
    connectionString =
        $"Server={serverName};Database={database};User ID={userId};Password={password};Encrypt=True;TrustServerCertificate=True;";
}
else
{
    // Use Azure Key Vault
    var vaultUri = new Uri("https://hillvault.vault.azure.net/");
    var client = new SecretClient(vaultUri, new DefaultAzureCredential());
    KeyVaultSecret secret = client.GetSecret("Obl-SQL-ConnectionString");
    connectionString = secret.Value;
}

//ITitleRepository titleRepository = TitleRepositoryList.Instance;
ITitleRepository titleRepository = new TitleRepositorySql(connectionString);

//INameRepository nameRepository = NameRepositoryList.Instance;
INameRepository nameRepository = new NameRepositorySql(connectionString);


UI SupercalifragilisticexpialidociousUserInterfaceDeluxe = new UI(titleRepository, nameRepository);
SupercalifragilisticexpialidociousUserInterfaceDeluxe.Start();