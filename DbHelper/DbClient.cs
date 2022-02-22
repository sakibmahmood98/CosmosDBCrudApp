using Microsoft.Azure.Cosmos;
using System.Reflection;

namespace DbHelper
{
    public class DbClient<T>
    {
        private Container _container;

        public DbClient(CosmosClient dbClient, string databaseName, string containerName)
        {
            this._container = dbClient.GetContainer(databaseName, containerName);
        }

        public async Task AddItemAsync(T item)
        {
            try
            {
                Type myType = item.GetType();
                PropertyInfo propertyInfo = myType.GetProperty("Id");
                await this._container.CreateItemAsync<T>(item, new PartitionKey(propertyInfo.GetValue(item).ToString()));

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task DeleteItemAsync(string id)
        {
            await this._container.DeleteItemAsync<T>(id, new PartitionKey(id));
        }

        public async Task<T> GetItemAsync(string id)
        {
            try
            {
                ItemResponse<T> response = await this._container.ReadItemAsync<T>(id, new PartitionKey(id));
                return response.Resource;
            }
            catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return default(T);
            }

        }

        public async Task UpdateItemAsync(string id, T item)
        {
            await this._container.UpsertItemAsync<T>(item, new PartitionKey(id));
        }
    }
}
