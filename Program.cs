using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;

public class Program
{
  private static string EndpointUri => config["EndpointUri"];
  private static string PrimaryKey => config["PrimaryKey"];
  private static IConfigurationRoot config;
  private CosmosClient cosmosClient;
  private Database database;
  private Container container;
  private string databaseId = "az204Database";
  private string containerId = "az204Container";

  public static async Task Main(string[] args)
  {
    try
    {
      Console.WriteLine("Beginning operations...\n");

      config = new ConfigurationBuilder()
      .SetBasePath(Directory.GetCurrentDirectory())
      .AddJsonFile("appsettings.json")
      .AddUserSecrets<Program>()
      .Build();

      Program p = new Program();
      await p.CosmosAsync();
    }
    catch (CosmosException ex)
    {
      Console.WriteLine($"{ex.StatusCode} error occurred: {ex}");
    }
    catch (Exception ex)
    {
      Console.WriteLine($"Error: {ex}");
    }
    finally
    {
      Console.WriteLine("End of demo, press any key to exit.");
      Console.ReadKey();
    }
  }

  public async Task CosmosAsync()
  {
    this.cosmosClient = new CosmosClient(EndpointUri, PrimaryKey);
    await this.CreateDatabaseAsync();
    await this.CreateContainerAsync();
  }

  private async Task CreateDatabaseAsync()
  {
    this.database = await this.cosmosClient.CreateDatabaseIfNotExistsAsync(databaseId);
    Console.WriteLine("Created Database: {0}\n", this.database.Id);
  }

  private async Task CreateContainerAsync()
  {
    this.container = await this.database.CreateContainerIfNotExistsAsync(containerId, "/LastName");
    Console.WriteLine("Created Container: {0}\n", this.container.Id);
  }

}