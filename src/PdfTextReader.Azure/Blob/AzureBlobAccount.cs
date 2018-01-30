﻿using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.Text;

namespace PdfTextReader.Azure.Blob
{
    public class AzureBlobAccount : AzureBlobFolder
    {
        CloudBlobClient _client;

        public AzureBlobAccount(string connectionString, string accountName) : base(accountName)
        {
            _client = GetClient(connectionString);
        }

        CloudBlobClient GetClient(string connectionString)
        {
            var storageAccount = CloudStorageAccount.Parse(connectionString);
            var client = storageAccount.CreateCloudBlobClient();

            return client;
        }

        CloudBlobContainer GetContainer(string containerName)
        {
            return _client.GetContainerReference(containerName); 
        }

        protected override AzureBlobFolder GetChildFolder(string name)
        {
            var container = GetContainer(name);

            var folder = new AzureBlobContainer(this, name, container);

            return folder;
        }

        protected override AzureBlobFileBlock GetChildFile(string name)
        {
            throw new System.IO.FileNotFoundException($"'{this.Uri}' is a storage account, not a file");
        }

        public override IEnumerable<AzureBlobRef> EnumItems()
        {
            BlobContinuationToken token = null;

            do
            {
                var segment = _client.ListContainersSegmentedAsync(token).Result;

                foreach (var container in segment.Results)
                {
                    string name = container.Name;

                    yield return new AzureBlobContainer(this, name, container);
                }

                token = segment.ContinuationToken;

            } while (token != null);
        }

        public override bool Exists()
        {
            return true;
        }
    }
}
