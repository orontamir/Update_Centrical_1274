using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UpdateCentrical_1274.DataModel
{
    public class Seler
    {
        public int IdNum { get; set; }

        public int MPhone { get; set; }

        public string SelerSsn  { get; set; }

        public int Status { get; set; }

        public int Station { get; set; }

        public DateTime StampTar { get; set; }

        public DateTime SmsTar { get; set; }

        public string ErrorMessage { get; set; }

        public override string ToString()
        {
            JObject json = new JObject(
                       new JProperty("user", SelerSsn),
                       new JProperty("event_time", DateTime.Now.ToString("yyyy-MM-dd-HH-mm-ss")),
                       new JProperty("event_description", $"Update seler ssn {SelerSsn}"),
                       new JProperty("event_value", 1),
                       new JProperty("phone", MPhone)
                       );
            return json.ToString();
        }


    }
}
