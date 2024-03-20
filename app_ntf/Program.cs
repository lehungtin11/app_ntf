// See https://aka.ms/new-console-template for more information
using app_ntf;
using MySql.Data.MySqlClient;
using Newtonsoft.Json;
using Serilog;
using Serilog.Events;
using System.Data;
using System.Text;

Console.WriteLine("Hello, World!");

Log.Logger = new LoggerConfiguration()
          .MinimumLevel.Information()
          .MinimumLevel.Override("Microsoft", LogEventLevel.Error)
          .Enrich.FromLogContext()
          .WriteTo.File(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "logs", "log.txt"), rollingInterval: RollingInterval.Day, rollOnFileSizeLimit: true, fileSizeLimitBytes: 5000000, flushToDiskInterval: TimeSpan.FromSeconds(3))
          .CreateLogger();
string txt = System.IO.File.ReadAllText("config.json");

var appSettings = Newtonsoft.Json.JsonConvert.DeserializeObject<config>(txt);

Log.Information("Start!");
Console.WriteLine("Start!");
Configure_RabbitMQ rab = new Configure_RabbitMQ(appSettings);
MySqlConnection conn = new MySqlConnection();
conn.ConnectionString = appSettings.Dataconnection.mysql;
MySqlCommand cmd = new MySqlCommand();
try
{
    conn.Open();
    cmd.Connection = conn;

    cmd.CommandText = "sys_get_ntf";
    cmd.CommandType = CommandType.StoredProcedure;

    string rs = (string)cmd.ExecuteScalar();

    if (!string.IsNullOrEmpty(rs))
    {
        List<Message> ls = JsonConvert.DeserializeObject<List<Message>>(rs);
        foreach (var item in ls)
        {
            // agent-
            var msg = Newtonsoft.Json.JsonConvert.SerializeObject(item);
            Log.Information(msg);
            rab.sendExchange($"agent-{item.agent}", msg);
        }
    }
}
catch (Exception e)
{
    Log.Error("Loi DBA: " + e.ToString());
}
finally
{
    conn.Close();
}
Console.WriteLine("end!");