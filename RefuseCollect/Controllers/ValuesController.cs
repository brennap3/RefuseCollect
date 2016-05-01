using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web;
using Newtonsoft.Json;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using Microsoft.WindowsAzure.Storage.Auth;
using System.Configuration;

namespace RefuseCollect.Controllers
{
    public class ValuesController : ApiController
    {
        // GET api/values 
        public IEnumerable<RefuseEntity> Get(String id)
        {
            Models.RefuseModel refusemodel = new Models.RefuseModel();

            CloudTable table = refusemodel.Table("RefuseCollect");

            var results = refusemodel.Selectbyid(id, table);

            return results;
        }

        public int Get(String anid, String Aggtype, String Aggquery)
        {
            //see https://social.msdn.microsoft.com/Forums/azure/en-US/33553664-9715-475c-807e-f0686304cd08/multiple-get-will-cause-add-azure-api-app-client-to-fail?forum=AzureAPIApps
            //need a unique string for each route parameter, in SWAGGER 2.0 very poor design no use of hierarchical structure
            if (Aggtype == "Agg" && Aggquery == "CountById")
            {
                Models.RefuseModel refusemodel = new Models.RefuseModel();

                CloudTable table = refusemodel.Table("RefuseCollect");

                var results = refusemodel.Selectbyid(anid, table);

                var countrefusebyparreaid = results.Count();

                return countrefusebyparreaid;
            }
            else return 0;
        }


        public RefuseEntity Get(String abid, String pareaid)
        {
            Models.RefuseModel refusemodel = new Models.RefuseModel();

            CloudTable table = refusemodel.Table("RefuseCollect");

            var selectrefuse = new PutRefuse() { id = abid, pareaid = pareaid };

            var results = refusemodel.RetrieveRefuse(table, selectrefuse);

            return results;

        }

        //"api/{controller}/{postid}/{postpareaid}/{postlatitude}/{postlongitude}"
        public String Post([FromBody] PostRefuse postrefuse)
        {
            Models.RefuseModel refusemodel = new Models.RefuseModel();

            CloudTable table = refusemodel.Table("RefuseCollect");

            String insertbypostrefuse = refusemodel.InsertbyPostrefuse(table, postrefuse);

            return insertbypostrefuse;
        }


        public String Put([FromBody] PutRefuse putrefuse)
        {
            Models.RefuseModel refusemodel = new Models.RefuseModel();

            CloudTable table = refusemodel.Table("RefuseCollect");

            String updatebyputrefuse = refusemodel.UpdatebyPutrefuse(table, putrefuse);

            return updatebyputrefuse;
        }

        // DELETE api/values/5 
        public String Delete(String id)
        {
            Models.RefuseModel refusemodel = new Models.RefuseModel();
            CloudTable table = refusemodel.Table("RefuseCollect");
            String Deleteoutcome = refusemodel.Deleteentry(table, id);
            return Deleteoutcome;
        }
    }
}
