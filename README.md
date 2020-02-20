# Hangfire Prometheus Exporter

This exporter that exposes information Hangfire.

[Simple Grafana Dashboard](https://grafana.com/grafana/dashboards/10928)


## Usage

```docker
docker pull maxjoehnk/hangfire-exporter:latest
```
### Parameters                                                                             

|  Parameter Name  | Description                    | Values                                      |
| ---------------- |--------------------------------|---------------------------------------------|
| dataProvider     | Hangfire datastorage.          | mongo, mysql, postgres, memorystorage, redis, sqlserver, azureservicebusqueue, litedb       |
| connectionString | Datastorage connection string. |                                             |
| dbName           | Datastorage database name.     |                                             |


----
#### DataStorage MongoDB


Parameter (dataProvider,connectionString,dbName)

Sample

```docker
docker run -d -p 5001:80 -e "dataProvider=mongo" -e "connectionString=mongodb://192.168.1.1:27017" -e "dbName=hangfire" --name myapp maxjoehnk/hangfire-exporter:latest
```

---
#### DataStorage SqlServer(SqlExpress and localDB include)


Parameter (dataProvider,connectionString)

Sample

```docker
docker run -d -p 5001:80 -e "dataProvider=sqlserver" -e "connectionString=Server=(localdb)\MSSQLLocalDB; database=hangfire; integrated security=True;" --name myapp maxjoehnk/hangfire-exporter:latest
```
---
#### DataStorage Redis(StackExchange)


Parameter (dataProvider,connectionString)

Sample

```docker
docker run -d -p 5001:80 -e "dataProvider=redis" -e "connectionString=192.168.1.1:6379" --name myapp maxjoehnk/hangfire-exporter:latest
```

---
#### DataStorage Azure Service Bus Queue


Parameter (dataProvider,connectionString)

Sample

```docker
docker run -d -p 5001:80 -e "dataProvider=azureservicebusqueue" -e "connectionString=..." --name myapp maxjoehnk/hangfire-exporter:latest
```
---
#### DataStorage LiteDB


Parameter (dataProvider,connectionString)

Sample

```docker
docker run -d -p 5001:80 -e "dataProvider=litedb" -e "connectionString=filePath" --name myapp maxjoehnk/hangfire-exporter:latest
```
---
#### DataStorage Memory Storage


Parameter (dataProvider)

Sample

```docker
docker run -d -p 5001:80 -e "dataProvider=memorystorage" --name myapp maxjoehnk/hangfire-exporter:latest
```

---
#### DataStorage Mysql


Parameter (dataProvider,connectionString)

Sample

```docker
docker run -d -p 5001:80 -e "dataProvider=mysql" -e "connectionString=server=192.168.1.1;uid=root;pwd=admin;database=hangfire;Allow User Variables=True" --name myapp maxjoehnk/hangfire-exporter:latest
```

---
#### DataStorage Postgres


Parameter (dataProvider,connectionString)

Sample

```docker
docker run -d -p 5001:80 -e "dataProvider=postgres" -e "connectionString=User ID = postgres; Password = password; Host = 192.168.1.1; Port = 5432; Database = hangfire;" --name myapp maxjoehnk/hangfire-exporter:latest
```

## Api Response
```text
# Help Servers Count 
hangfire_servers_count 0
# Help Queues Count
hangfire_queues_count 0
# Help Deleted Jobs Count
hangfire_deleted_jobs_total_count 0
# Help Enqueued Jobs Count
hangfire_enqueued_jobs_total_count 0
# Help Failed Jobs Count
hangfire_failed_jobs_total_count 47
# Help Processing Jobs Count
hangfire_processing_jobs_total_count 0
# Help Recurring Jobs Count
hangfire_recurring_jobs_count 2
# Help Scheduled Jobs Count
hangfire_scheduled_jobs_total_count 0
# Help Succeeded Jobs List Count
hangfire_succeeded_jobs_total_count 5353
# Help Failed Jobs By Dates Count
hangfire_failed_jobs_by_dates_count{key="02/20/2020"} 0
hangfire_failed_jobs_by_dates_count{key="02/19/2020"} 0
hangfire_failed_jobs_by_dates_count{key="02/18/2020"} 0
hangfire_failed_jobs_by_dates_count{key="02/17/2020"} 0
hangfire_failed_jobs_by_dates_count{key="02/16/2020"} 0
hangfire_failed_jobs_by_dates_count{key="02/15/2020"} 0
hangfire_failed_jobs_by_dates_count{key="02/14/2020"} 0
# Help Succeeded Jobs By Dates Count
hangfire_succeeded_jobs_by_dates_count{key="02/20/2020"} 0
hangfire_succeeded_jobs_by_dates_count{key="02/19/2020"} 69
hangfire_succeeded_jobs_by_dates_count{key="02/18/2020"} 0
hangfire_succeeded_jobs_by_dates_count{key="02/17/2020"} 201
hangfire_succeeded_jobs_by_dates_count{key="02/16/2020"} 0
hangfire_succeeded_jobs_by_dates_count{key="02/15/2020"} 0
hangfire_succeeded_jobs_by_dates_count{key="02/14/2020"} 69
# Help Hourly Failed Jobs Count
hangfire_hourly_failed_jobs_count{key="02/20/2020 15:30:43"} 0
hangfire_hourly_failed_jobs_count{key="02/20/2020 14:30:43"} 0
hangfire_hourly_failed_jobs_count{key="02/20/2020 13:30:43"} 0
hangfire_hourly_failed_jobs_count{key="02/20/2020 12:30:43"} 0
hangfire_hourly_failed_jobs_count{key="02/20/2020 11:30:43"} 0
hangfire_hourly_failed_jobs_count{key="02/20/2020 10:30:43"} 0
hangfire_hourly_failed_jobs_count{key="02/20/2020 09:30:43"} 0
hangfire_hourly_failed_jobs_count{key="02/20/2020 08:30:43"} 0
hangfire_hourly_failed_jobs_count{key="02/20/2020 07:30:43"} 0
hangfire_hourly_failed_jobs_count{key="02/20/2020 06:30:43"} 0
hangfire_hourly_failed_jobs_count{key="02/20/2020 05:30:43"} 0
hangfire_hourly_failed_jobs_count{key="02/20/2020 04:30:43"} 0
hangfire_hourly_failed_jobs_count{key="02/20/2020 03:30:43"} 0
hangfire_hourly_failed_jobs_count{key="02/20/2020 02:30:43"} 0
hangfire_hourly_failed_jobs_count{key="02/20/2020 01:30:43"} 0
hangfire_hourly_failed_jobs_count{key="02/20/2020 00:30:43"} 0
hangfire_hourly_failed_jobs_count{key="02/19/2020 23:30:43"} 0
hangfire_hourly_failed_jobs_count{key="02/19/2020 22:30:43"} 0
hangfire_hourly_failed_jobs_count{key="02/19/2020 21:30:43"} 0
hangfire_hourly_failed_jobs_count{key="02/19/2020 20:30:43"} 0
hangfire_hourly_failed_jobs_count{key="02/19/2020 19:30:43"} 0
hangfire_hourly_failed_jobs_count{key="02/19/2020 18:30:43"} 0
hangfire_hourly_failed_jobs_count{key="02/19/2020 17:30:43"} 0
hangfire_hourly_failed_jobs_count{key="02/19/2020 16:30:43"} 0
# Help Hourly Succeeded Jobs Count
hangfire_hourly_succeeded_jobs_count{key="02/20/2020 15:30:43"} 0
hangfire_hourly_succeeded_jobs_count{key="02/20/2020 14:30:43"} 0
hangfire_hourly_succeeded_jobs_count{key="02/20/2020 13:30:43"} 0
hangfire_hourly_succeeded_jobs_count{key="02/20/2020 12:30:43"} 0
hangfire_hourly_succeeded_jobs_count{key="02/20/2020 11:30:43"} 0
hangfire_hourly_succeeded_jobs_count{key="02/20/2020 10:30:43"} 0
hangfire_hourly_succeeded_jobs_count{key="02/20/2020 09:30:43"} 0
hangfire_hourly_succeeded_jobs_count{key="02/20/2020 08:30:43"} 0
hangfire_hourly_succeeded_jobs_count{key="02/20/2020 07:30:43"} 0
hangfire_hourly_succeeded_jobs_count{key="02/20/2020 06:30:43"} 0
hangfire_hourly_succeeded_jobs_count{key="02/20/2020 05:30:43"} 0
hangfire_hourly_succeeded_jobs_count{key="02/20/2020 04:30:43"} 0
hangfire_hourly_succeeded_jobs_count{key="02/20/2020 03:30:43"} 0
hangfire_hourly_succeeded_jobs_count{key="02/20/2020 02:30:43"} 0
hangfire_hourly_succeeded_jobs_count{key="02/20/2020 01:30:43"} 0
hangfire_hourly_succeeded_jobs_count{key="02/20/2020 00:30:43"} 0
hangfire_hourly_succeeded_jobs_count{key="02/19/2020 23:30:43"} 0
hangfire_hourly_succeeded_jobs_count{key="02/19/2020 22:30:43"} 0
hangfire_hourly_succeeded_jobs_count{key="02/19/2020 21:30:43"} 0
hangfire_hourly_succeeded_jobs_count{key="02/19/2020 20:30:43"} 0
hangfire_hourly_succeeded_jobs_count{key="02/19/2020 19:30:43"} 0
hangfire_hourly_succeeded_jobs_count{key="02/19/2020 18:30:43"} 0
hangfire_hourly_succeeded_jobs_count{key="02/19/2020 17:30:43"} 0
hangfire_hourly_succeeded_jobs_count{key="02/19/2020 16:30:43"} 0
# Help Average Total Duration
hangfire_average_total_duration 895.521739130435
```
