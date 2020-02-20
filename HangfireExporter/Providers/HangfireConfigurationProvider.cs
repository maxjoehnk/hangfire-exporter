﻿using Hangfire;
using Hangfire.Azure.ServiceBusQueue;
using Hangfire.LiteDB;
using Hangfire.MemoryStorage;
using Hangfire.Mongo;
using Hangfire.MySql;
using Hangfire.PostgreSql;
using Hangfire.SqlServer;
using Hangfire.States;
using Microsoft.ServiceBus.Messaging;
using Microsoft.WindowsAzure;
using StackExchange.Redis;
using System;
using System.Transactions;

namespace HangfireExporter.Providers
{
    public static class HangfireConfigurationProvider
    {
        public static void GetMongoConfiguration(string connectionString, string mongoDatabaseName)
        {
            GlobalConfiguration.Configuration.UseMongoStorage(connectionString, mongoDatabaseName, new MongoStorageOptions());
        }

        public static void GetSqlServerConfiguration(string connectionString)
        {
            GlobalConfiguration.Configuration.UseSqlServerStorage(connectionString);
        }

        public static void GetRedisConfiguration(string redisIp)
        {
            ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(redisIp);
            GlobalConfiguration.Configuration.UseRedisStorage(redis);
        }

        public static void GetAzureServiceBusQueueConfiguration(string connectionString)
        {
            SqlServerStorage sqlStorage = new SqlServerStorage(connectionString);
            string azureConnectionString = CloudConfigurationManager.GetSetting("Microsoft.ServiceBus.ConnectionString");

            Action<QueueDescription> configureAction = qd =>
            {
                qd.MaxSizeInMegabytes = 5120;
                qd.DefaultMessageTimeToLive = new TimeSpan(0, 1, 0);
            };

            sqlStorage.UseServiceBusQueues(new ServiceBusQueueOptions
            {
                ConnectionString = azureConnectionString,
                Configure = configureAction,
                Queues = new[] { EnqueuedState.DefaultQueue },
                CheckAndCreateQueues = false,
                LoopReceiveTimeout = TimeSpan.FromMilliseconds(500)
            });

            GlobalConfiguration.Configuration.UseStorage(sqlStorage);
        }

        public static void GetLiteDbConfiguration(string liteDbPath)
        {
            GlobalConfiguration.Configuration.UseLiteDbStorage(liteDbPath);
        }

        public static void GetMemoryStorageConfiguration()
        {
            GlobalConfiguration.Configuration.UseMemoryStorage();
        }

        public static void GetMySqlConfiguration(string connectionString)
        {
            GlobalConfiguration.Configuration.UseStorage(new MySqlStorage(connectionString, new MySqlStorageOptions
            {
                TransactionIsolationLevel = IsolationLevel.ReadCommitted,
                QueuePollInterval = TimeSpan.FromSeconds(15),
                JobExpirationCheckInterval = TimeSpan.FromHours(1),
                CountersAggregateInterval = TimeSpan.FromMinutes(5),
                PrepareSchemaIfNecessary = true,
                DashboardJobListLimit = 50000,
                TransactionTimeout = TimeSpan.FromMinutes(1)
            }));
        }

        public static void GetPostgresConfiguration(string connectionString)
        {            
            GlobalConfiguration.Configuration.UsePostgreSqlStorage(connectionString);
        }
    }
}
