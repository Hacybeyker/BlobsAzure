using System;
using Microsoft.Azure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System.IO;

/* https://azure.microsoft.com/en-us/documentation/articles/storage-dotnet-how-to-use-blobs */
/* http://www.genbetadev.com/programacion-en-la-nube/operaciones-de-fichero-contra-un-blob-en-windows-azure */
/* Install-Package WindowsAzure.Storage -Version 7.1.2 */
/* Install-Package Microsoft.WindowsAzure.ConfigurationManager */

namespace BlobsAzure
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                CloudStorageAccount storageAccount = CloudStorageAccount.Parse(CloudConfigurationManager.GetSetting("StorageConnectionString"));
                CloudBlobClient blobClient = storageAccount.CreateCloudBlobClient();
                CloudBlobContainer container = blobClient.GetContainerReference("facturacionelectronica");
                CloudBlockBlob blockBlob;
                #region Upload
                blockBlob = container.GetBlockBlobReference("20600995805-01-F001-00000001");
                using (var fileStream = File.OpenRead(@"C:\FacturacionElectronicaSNET\Factura\enviado\20600995805-01-F001-00000001.xml"))
                {
                    blockBlob.UploadFromStream(fileStream);
                }
                Console.WriteLine("Subida Exitosa");
                #endregion
                #region Download
                blockBlob = container.GetBlockBlobReference("20600995805-01-F001-00000001");
                using (var fileStream = File.OpenWrite(@"C:\Users\Carlos\Downloads\20600995805-01-F001-00000001"))
                {
                    blockBlob.DownloadToStream(fileStream);
                }
                Console.WriteLine("Descarga Exitosa");
                //Console.ReadLine();
                #endregion
                #region Delete
                blockBlob = container.GetBlockBlobReference("20600995805-01-F001-00000001");
                //blockBlob.Delete();
                Console.WriteLine("Eliminacion Exitosa");
                #endregion
                Console.ReadLine();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                throw e;
            }
        }
    }
}
