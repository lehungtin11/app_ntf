using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace app_ntf
{
    public class config
    {
        public RabbitMQ RabbitMQ { get; set; }
        public Dataconnection Dataconnection { get; set; }
    }
    public class Dataconnection
    {
        public string mysql { get; set; }
    }
    public class RabbitMQ
    {
        public List<string> Hostname { get; set; }
        public int Port { get; set; }
        public int MessageCount { get; set; }
        public string user { get; set; }
        public string pass { get; set; }
    }

    public class Message
    {
        public string id { get; set; }
        public string fkKH { get; set; }
        public string agent { get; set; }
        public string content { get; set; }
        public string thoiGian { get; set; }
        public string type { get; set; }
    }

}
