﻿using Microsoft.Azure.Storage.Blob;
using Rebus.AzureBlobs.DataBus;
using Rebus.DataBus;
using Rebus.Logging;
using Rebus.Tests.Contracts.DataBus;
using System;

namespace Rebus.AzureBlobs.Tests.DataBus
{
    public class AzureBlobsDataBusStorageFactory : IDataBusStorageFactory
    {
        readonly string _containerName = $"container-{Guid.NewGuid().ToString().Substring(0, 3)}".ToLowerInvariant();

        public IDataBusStorage Create()
        {
            Console.WriteLine($"Creating blobs data bus storage for container {_containerName}");

            return new AzureBlobsDataBusStorage(AzureConfig.StorageAccount, _containerName, new ConsoleLoggerFactory(false));
        }

        public void CleanUp()
        {
            Console.WriteLine($"Deleting container {_containerName} (if it exists)");

            AsyncHelpers.RunSync(() => AzureConfig.StorageAccount.CreateCloudBlobClient()
                .GetContainerReference(_containerName)
                .DeleteIfExistsAsync());
        }
    }
}