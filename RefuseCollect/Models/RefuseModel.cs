using System;
using System.Collections.Generic;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Table;
using System.Configuration;

namespace RefuseCollect.Models
{
    public class RefuseModel
    {

        public CloudTable Table(String tableName)
        {

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(ConfigurationManager.ConnectionStrings["ConnString"].ConnectionString);

            CloudTableClient tableClient = storageAccount.CreateCloudTableClient();

            CloudTable table = tableClient.GetTableReference(tableName);

            return table;
        }

        public IEnumerable<Controllers.RefuseEntity> Selectbyid(String id, CloudTable table)
        {
            TableQuery<Controllers.RefuseEntity> query = new TableQuery<Controllers.RefuseEntity>().Where(TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, id));
            
            var results = table.ExecuteQuery(query);

            return results;
        }

        public IEnumerable<Controllers.RefuseEntity> SelectbyidCollect(String id, CloudTable table)
        {
            TableQuery<Controllers.RefuseEntity> query = new TableQuery<Controllers.RefuseEntity>().Where(
            TableQuery.CombineFilters(
            TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, id),
            TableOperators.And,
            TableQuery.GenerateFilterCondition("HasBeenCollected", QueryComparisons.Equal, "Refuse has been collected")));

            var results = table.ExecuteQuery(query);

            return results;
        }
        //Has Not been Collected Yet
        public IEnumerable<Controllers.RefuseEntity> SelectbyidNotCollect(String id, CloudTable table)
        {
            TableQuery<Controllers.RefuseEntity> query = new TableQuery<Controllers.RefuseEntity>().Where(
            TableQuery.CombineFilters(
            TableQuery.GenerateFilterCondition("PartitionKey", QueryComparisons.Equal, id),
            TableOperators.And,
            TableQuery.GenerateFilterCondition("HasBeenCollected", QueryComparisons.Equal, "Has Not been Collected Yet")));

            var results = table.ExecuteQuery(query);

            return results;
        }

        public String InsertbyPostrefuse(CloudTable table, Controllers.PostRefuse postrefuse)
        {

            DateTime nowDate = DateTime.Now;
            //ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss \"GMT\"zzz")
            string time = "Time:" + nowDate.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss \"GMT\"zzz");

            DateTime collectionDate = nowDate.AddYears(1000);

            string time2 = "Time:" + collectionDate.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss \"GMT\"zzz");

            Controllers.RefuseEntity refuse = new Controllers.RefuseEntity(postrefuse.id, postrefuse.pareaid, postrefuse.platitude, postrefuse.plongitude, time, "Has Not been Collected Yet", time2 );

            TableOperation insertOperation = TableOperation.Insert(refuse);
            try
            {
                table.Execute(insertOperation);
                return "insert executed sucessfully";
            }
            catch (Exception e)
            {
                return "insert not executed sucessfully";
                //do something
            }
        }

        public String UpdatebyPutrefuse(CloudTable table, Controllers.PutRefuse putrefuse)
        {
            Controllers.RefuseEntity updateEntity = RetrieveRefuse(table, putrefuse);

            if (updateEntity != null)
            {
                DateTime nowDate2 = DateTime.Now;

                string time3 = "Time:" + nowDate2.ToLocalTime().ToString("yyyy-MM-dd HH:mm:ss \"GMT\"zzz");
                
                // Change the collection time
                updateEntity.Timecollected = time3;
                updateEntity.HasBeenCollected = "Refuse has been collected";

                // Create the Replace TableOperation.
                TableOperation updateOperation = TableOperation.Replace(updateEntity);

                // Execute the operation.
                try
                {
                    table.Execute(updateOperation);
                    return "update executed sucessfully";

                }
                catch (Exception e)
                {
                    return "update not executed sucessfully";
                    //do something
                }
            }
            else { return "update not executed sucessfully"; }

        }

        public Controllers.RefuseEntity RetrieveRefuse(CloudTable table, Controllers.PutRefuse putrefuse)
        {
            // Create a retrieve operation that takes a customer entity.
            TableOperation retrieveOperation = TableOperation.Retrieve<Controllers.RefuseEntity>(putrefuse.id, putrefuse.pareaid);

            // Execute the operation.
            TableResult retrievedResult = table.Execute(retrieveOperation);

            // Assign the result to a CustomerEntity object.
            Controllers.RefuseEntity refuseEntity = (Controllers.RefuseEntity)retrievedResult.Result;
            return refuseEntity;
        }

        public String Deleteentry(CloudTable table, String id)
        {
            TableQuery<Controllers.RefuseEntity> query = new TableQuery<Controllers.RefuseEntity>().Where(TableQuery.GenerateFilterCondition("RowKey", QueryComparisons.Equal, id));
            var results = table.ExecuteQuery(query);
            String outcome = "";
            // Create the Delete TableOperation.
            foreach (Controllers.RefuseEntity deleteEntity in results)
                if (deleteEntity != null)
                {
                    TableOperation deleteOperation = TableOperation.Delete(deleteEntity);

                    // Execute the operation.
                    try
                    {
                        table.Execute(deleteOperation);
                        outcome = "success row deleted";
                        return outcome;

                    }
                    catch (Exception e)
                    {
                        //do soomething
                        outcome = "error ocuured while trying to delete record" + e;
                    }
                }

                else { outcome = "row not deleted could not find entity"; }

            return outcome;

        }
    }
}