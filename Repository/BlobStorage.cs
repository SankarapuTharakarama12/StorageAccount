
using Azure.Storage;
using Azure.Storage.Blobs;
using Azure.Storage.Blobs.Models;

namespace StorageAccount.Repository
{
    public class BlobStorage
    {
        static string connectionString="DefaultEndpointsProtocol=https;AccountName=tharakaz12;AccountKey=7g3J3B68pv9o+IUL4KHZNqeDOf9HM1XHVd+Y1qxLwpKMbuv420VHihU1Y9C4CQO8WxZsiy1wpxTj+AStqAvB1A==;EndpointSuffix=core.windows.net";
        public static async Task CreateBlob(string blobName)
        {
            if(string.IsNullOrEmpty(blobName))
            {
                throw new ArgumentNullException("enter blob name");
            }
            try
            {
                BlobContainerClient container=new BlobContainerClient(connectionString,blobName);
                await container.CreateAsync();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public static async Task DeleteBlob(string blobName)
        {
            if(string.IsNullOrEmpty(blobName))
            {
                throw new ArgumentNullException("enter blob name");
            }
            try
            {
                BlobContainerClient container=new BlobContainerClient(connectionString,blobName);
                await container.DeleteAsync();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public static async Task DeleteBlobContent(string blobName,string file)
        {
             if(string.IsNullOrEmpty(blobName))
            {
                throw new ArgumentNullException("enter blob name");
            }
            try
            {
                BlobContainerClient container=new BlobContainerClient(connectionString,blobName);
                await container.DeleteBlobAsync(file);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public static async Task<BlobProperties> UpdateBlobContent(string blobName,string file)
        {
             if(string.IsNullOrEmpty(blobName))
            {
                throw new ArgumentNullException("enter blob name");
            }
            try
            {
                string fileName=Path.GetTempFileName();
                BlobContainerClient container=new BlobContainerClient(connectionString,blobName);
                BlobClient blob=container.GetBlobClient(file);
                await blob.UploadAsync(fileName);
                BlobProperties prop=await blob.GetPropertiesAsync();
                return prop;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public static async Task<BlobProperties> getBlobContent(string blobName,string file)
        {
            if(string.IsNullOrEmpty(blobName))
            {
                throw new ArgumentNullException("enter blob name");
            }
            try
            {
                BlobContainerClient container=new BlobContainerClient(connectionString,blobName);
                BlobClient blob=container.GetBlobClient(file);
                BlobProperties prop=await blob.GetPropertiesAsync();
                return prop;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
        public static async Task<List<string>> GetBlob(string blobName,string file)
        {
           if(string.IsNullOrEmpty(blobName))
            {
                throw new ArgumentNullException("enter blob name");
            }
            try
            {
                BlobContainerClient container=new BlobContainerClient(connectionString,blobName);
                List<string> names=new List<string>();
                await foreach(BlobItem a in container.GetBlobsAsync())
                {
                    names.Add(a.Name);
                }
                return names;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static async Task<BlobProperties> DownloadBlob(string blobName,string file)
        {
            try
            {
                string path=@"C:\Users\vmadmin\Desktop\DemoStorageAccount\StorageAccount\Downloads\"+blobName;
                BlobContainerClient container=new BlobContainerClient(connectionString,blobName);
                BlobClient client=container.GetBlobClient(file);
                await client.DownloadToAsync(path);
                BlobProperties prop=await client.GetPropertiesAsync();
                return prop;
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}