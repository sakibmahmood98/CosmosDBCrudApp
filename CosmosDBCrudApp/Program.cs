using DbHelper;
using Microsoft.Azure.Cosmos;
using Models;
using System.Configuration;

namespace CosmosDbCrudApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            // The Azure Cosmos DB endpoint for running this sample.
            string EndpointUri = ConfigurationManager.AppSettings["EndPointUri"];

             // The primary key for the Azure Cosmos account.
            string PrimaryKey = ConfigurationManager.AppSettings["PrimaryKey"];

            // The name of the database and container we will create
            string databaseId = "AllPublicationsBook";
            string containerId = "Books";

            // The Cosmos client instance
            CosmosClient cosmosClient = new CosmosClient(EndpointUri, PrimaryKey);

            // The database we will create
            Database database = Task.Run(async () => await cosmosClient.CreateDatabaseIfNotExistsAsync(databaseId)).Result;

            // The container we will create.
            Container container = Task.Run(async () => await database.CreateContainerIfNotExistsAsync(containerId, "/id")).Result;

            Console.WriteLine($"Added container: {containerId}");

            DbClient<Book> mClient = new DbClient<Book>(cosmosClient, databaseId, containerId);

            Book book = new Book
            {
                Id = "1",
                Title = "Foo",
                Publish_Year = "2021`",
                Base_Price = "15.00 USD",
                Book_Availability = "Unavailable"
            };

            Book updatedBook = new Book
            {
                Id = "2",
                Title = "boo",
                Publish_Year = "2022",
                Base_Price = "20.00 USD",
                Book_Availability = "Available"
            };

/*          await mClient.AddItemAsync(book);
            Console.WriteLine($"Added product: {book}");
            await mClient.UpdateItemAsync(book.Id, updatedBook);
            Console.WriteLine($"Updated: {updatedBook}");*/
            await mClient.DeleteItemAsync(book.Id);
            Console.WriteLine($"Deleted: {book}");
        }
    }
}