using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;

using Newtonsoft.Json;
using rgomezj.Freelance.Me.Data.Implementation.JSON.Config;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace rgomezj.Freelance.Me.Data.Implementation.JSON
{
    public abstract class JSONConfigContext
    {
        const string DATACONTAINER = "freelancepagedata";
        public string FileName { get; private set; }
        private CloudBlobContainer container;

        public JSONConfigContext(JSONDatabaseConfig config, string fileName)
        {
            this.FileName = fileName;
            var storageAccount = CloudStorageAccount.Parse(config.ConnectionString);
            var myClient = storageAccount.CreateCloudBlobClient();
            container = myClient.GetContainerReference(DATACONTAINER);
        }

        public async Task<T> GetEntity<T>()
        {
            T result = default(T);
            CloudBlockBlob blockBlob = container.GetBlockBlobReference(this.FileName);

            if (blockBlob != null)
            {
                var resultString = await  blockBlob.DownloadTextAsync();
                result = JsonConvert.DeserializeObject<T>(resultString);
            }
            return result;
        }
    }
}