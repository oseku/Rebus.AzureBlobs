﻿using Microsoft.Azure.Storage;
using Rebus.Exceptions;
using System;
using System.IO;

namespace Rebus.AzureBlobs.Tests
{
    public static class AzureConfig
    {
        public static CloudStorageAccount StorageAccount => CloudStorageAccount.Parse(ConnectionString);

        public static string ConnectionString => ConnectionStringFromFileOrNull(Path.Combine(GetBaseDirectory(), "azure_storage_connection_string.txt"))
                                         ?? ConnectionStringFromEnvironmentVariable("rebus2_storage_connection_string")
                                         ?? Throw("Could not find Azure Storage connection string!");

        static string GetBaseDirectory() => AppContext.BaseDirectory;

        static string ConnectionStringFromFileOrNull(string filePath)
        {
            if (!File.Exists(filePath))
            {
                Console.WriteLine("Could not find file {0}", filePath);
                return null;
            }

            Console.WriteLine("Using Azure Storage connection string from file {0}", filePath);
            return File.ReadAllText(filePath);
        }

        static string ConnectionStringFromEnvironmentVariable(string environmentVariableName)
        {
            var value = Environment.GetEnvironmentVariable(environmentVariableName);

            if (value == null)
            {
                Console.WriteLine("Could not find env variable {0}", environmentVariableName);
                return null;
            }

            Console.WriteLine("Using Azure Storage connection string from env variable {0}", environmentVariableName);

            return value;
        }

        static string Throw(string message)
        {
            throw new RebusConfigurationException(message);
        }
    }
}