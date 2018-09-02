using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Newtonsoft.Json;
using rgomezj.Freelance.Me.Data.Implementation.TableStorage.Config;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace rgomezj.Freelance.Me.Data.Implementation.TableStorage
{
    public abstract class TableStorageContext
    {
        const string DATACONTAINER = "freelancepagedata";
        public string TableName { get; private set; }
        private CloudTableClient tableClient = null;

        public TableStorageContext(TableStorageConfig config, string tableName)
        {
            this.TableName = tableName;
            var storageAccount = CloudStorageAccount.Parse(config.ConnectionString);
            tableClient = storageAccount.CreateCloudTableClient();
        }

        public async Task<T> GetEntity<T>(CancellationToken ct = default(CancellationToken))
        {
            T result = default(T);
            CloudTable table = tableClient.GetTableReference(DATACONTAINER);
            await table.CreateIfNotExistsAsync();
            TableQuery query = new TableQuery().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, this.TableName));
            TableContinuationToken token = null;
            query.TakeCount = 1;
            do
            {
                TableQuerySegment segment = await table.ExecuteQuerySegmentedAsync(query, token);
                token = segment.ContinuationToken;
                var singleResult = segment.Results[0];
                var resultString = singleResult.Properties["data"].StringValue;
                result = JsonConvert.DeserializeObject<T>(resultString);
            }
            while (token != null);
            return result;
        }
    }
}