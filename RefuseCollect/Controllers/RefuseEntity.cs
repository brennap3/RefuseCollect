using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using System.Text;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure.Storage.Auth;
using System.Configuration;

namespace RefuseCollect.Controllers
{
    public class RefuseEntity : TableEntity
    {
        public RefuseEntity()
        {
        }
        public RefuseEntity(string areaname, string areaid, string platitude, string plongitude, string timereported, string hasbeencollected, string timecollected)
        {
            this.PartitionKey = areaname;
            this.RowKey = areaid;
            this.latitude = platitude;
            this.longitude = plongitude;
            this.Timereported = timereported;
            this.HasBeenCollected = hasbeencollected;
            this.Timecollected = timecollected;
        }

        public string latitude { get; set; }
        public string longitude { get; set; }
        public string Timereported { get; set; }
        public string Timecollected { get; set; }
        public string HasBeenCollected { get; set; }
    }
}