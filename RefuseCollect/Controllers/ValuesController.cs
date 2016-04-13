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

            //string pcode = "Dublin 7";
            Models.RefuseModel refusemodel = new Models.RefuseModel();

            CloudTable table = refusemodel.Table("RefuseCollect");

            var results = refusemodel.Selectbyid(id, table);

            return results;
        }


        public RefuseEntity Get(String id, String pareaid)
        {
            Models.RefuseModel refusemodel = new Models.RefuseModel();

            CloudTable table = refusemodel.Table("RefuseCollect");

            var selectrefuse = new PutRefuse() { id = id, pareaid = pareaid };

            var results = refusemodel.RetrieveRefuse(table, selectrefuse);

            return results;

        }


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
