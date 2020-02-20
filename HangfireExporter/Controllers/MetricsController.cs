using Hangfire;
using Hangfire.Storage;
using HangfireExporter.Providers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Text;

namespace HangfireExporter.Controllers
{
    [Route("/metrics")]
    [ApiController]
    public class MetricsController : ControllerBase
    {
        private readonly IMonitoringApi api;

        public MetricsController()
        {
            ConnectDataStorage();
            
            api = JobStorage.Current.GetMonitoringApi();
        }

        [HttpGet]
        public string GetMetrics()
        {
            StringBuilder data = new StringBuilder();
            AppendServerStats(data);
            AppendTotalJobCounts(data);
            AppendFailedJobsByDatesCount(data);
            AppendSucceededJobsByDatesCount(data);
            AppendHourlyFailedJobsCount(data);
            AppendHourlySucceededJobsCount(data);
            AppendAverageJobDuration(data);

            return data.ToString();
        }

        private static void ConnectDataStorage()
        {
            string dataProviderArg = Environment.GetEnvironmentVariable("dataProvider");
            string connStringArg = Environment.GetEnvironmentVariable("connectionString");
            string dbNameArg = Environment.GetEnvironmentVariable("dbName");

            switch (dataProviderArg)
            {
                case "mongo":
                    HangfireConfigurationProvider.GetMongoConfiguration(connStringArg, dbNameArg);
                    break;
                case "sqlserver":
                    HangfireConfigurationProvider.GetSqlServerConfiguration(connStringArg);
                    break;
                case "redis":
                    HangfireConfigurationProvider.GetRedisConfiguration(connStringArg);
                    break;
                case "azureservicebusqueue":
                    HangfireConfigurationProvider.GetAzureServiceBusQueueConfiguration(connStringArg);
                    break;
                case "litedb":
                    HangfireConfigurationProvider.GetLiteDbConfiguration(connStringArg);
                    break;
                case "memorystorage":
                    HangfireConfigurationProvider.GetMemoryStorageConfiguration();
                    break;
                case "mysql":
                    HangfireConfigurationProvider.GetMySqlConfiguration(connStringArg);
                    break;
                case "postgres":
                    HangfireConfigurationProvider.GetPostgresConfiguration(connStringArg);
                    break;
                default:
                    Console.WriteLine("Data Connection error. Please check connection string.");
                    break;
            }
        }

        private void AppendTotalJobCounts(StringBuilder data)
        {
            data.AppendLine("# Help Deleted Jobs Count");
            data.AppendLine($"hangfire_deleted_jobs_total_count {api.GetStatistics().Deleted}");
            data.AppendLine("# Help Enqueued Jobs Count");
            data.AppendLine($"hangfire_enqueued_jobs_total_count {api.GetStatistics().Enqueued}");
            data.AppendLine("# Help Failed Jobs Count");
            data.AppendLine($"hangfire_failed_jobs_total_count {api.GetStatistics().Failed}");
            data.AppendLine("# Help Processing Jobs Count");
            data.AppendLine($"hangfire_processing_jobs_total_count {api.GetStatistics().Processing}");
            data.AppendLine("# Help Recurring Jobs Count");
            data.AppendLine($"hangfire_recurring_jobs_count {api.GetStatistics().Recurring}");
            data.AppendLine("# Help Scheduled Jobs Count");
            data.AppendLine($"hangfire_scheduled_jobs_total_count {api.GetStatistics().Scheduled}");
            data.AppendLine("# Help Succeeded Jobs List Count");
            data.AppendLine($"hangfire_succeeded_jobs_total_count {api.GetStatistics().Succeeded}");
        }

        private void AppendServerStats(StringBuilder data)
        {
            data.AppendLine("# Help Servers Count ");
            data.AppendLine($"hangfire_servers_count {api.GetStatistics().Servers}");
            data.AppendLine("# Help Queues Count");
            data.AppendLine($"hangfire_queues_count {api.GetStatistics().Queues}");
        }

        private void AppendHourlySucceededJobsCount(StringBuilder data)
        {
            data.AppendLine("# Help Hourly Succeeded Jobs Count");
            foreach ((DateTime key, long value) in api.HourlySucceededJobs())
            {
                data.AppendLine($"hangfire_hourly_succeeded_jobs_count{{key=\"{key}\"}} {value}");
            }
        }

        private void AppendHourlyFailedJobsCount(StringBuilder data)
        {
            data.AppendLine("# Help Hourly Failed Jobs Count");
            foreach ((DateTime key, long value) in api.HourlyFailedJobs())
            {
                data.AppendLine($"hangfire_hourly_failed_jobs_count{{key=\"{key}\"}} {value}");
            }
        }

        private void AppendSucceededJobsByDatesCount(StringBuilder data)
        {
            data.AppendLine("# Help Succeeded Jobs By Dates Count");
            foreach ((DateTime key, long value) in api.SucceededByDatesCount())
            {
                data.AppendLine(
                    $"hangfire_succeeded_jobs_by_dates_count{{key=\"{key.ToShortDateString()}\"}} {value}");
            }
        }

        private void AppendFailedJobsByDatesCount(StringBuilder data)
        {
            data.AppendLine("# Help Failed Jobs By Dates Count");
            foreach ((DateTime key, long value) in api.FailedByDatesCount())
            {
                data.AppendLine(
                    $"hangfire_failed_jobs_by_dates_count{{key=\"{key.ToShortDateString()}\"}} {value}");
            }
        }

        private void AppendAverageJobDuration(StringBuilder data)
        {
            data.AppendLine("# Help Average Total Duration");
            double average = api.SucceededJobs(0, int.MaxValue)
                .Select(job => job.Value.TotalDuration)
                .Average() ?? 0;
            data.AppendLine($"hangfire_average_total_duration {average}");
        }
    }
}