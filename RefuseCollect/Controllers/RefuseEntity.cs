
using Microsoft.WindowsAzure.Storage.Table;


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