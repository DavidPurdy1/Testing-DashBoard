using System.Threading.Tasks;
using System.Linq;
using System;
using System.Data.SqlClient;
using System.Data; 
using System.Configuration; 

namespace BlazorApp.Data
{
    public class TestDataRetrieval // make sure this can also just find the max results for the most recent test.
    {
        private int max;
        private string connectionString = ConfigurationManager.AppSettings.Get("DBConnection");

        //Only getting one test from each assembly
        public Task<TestData[]> GetPreviousDataAsync(){
            //max = GetMax();
            return Task.FromResult(Enumerable.Range(1, 8).Select( index => new TestData{
                AssemblyId = index,
                //use AssemblyId to obtain other values
                TestId = 199, 
                TestTime = DateTime.Now.Date, 
                TestStatus = true,
                Tester = "david",
                ImageLocation = "location",
            }).ToArray());
        }
        public Task<TestData[]> GetLastDataAsync(){
            //max = GetMax();
            return Task.FromResult(Enumerable.Range(1, 1).Select( index => GetTestDataFromSql(index) ).ToArray());
        }

        public TestData GetFromSql(int index){
            TestData data = new TestData();
            SqlConnection connection = new SqlConnection(connectionString); 
            connection.Open();

            SqlCommand command = new SqlCommand();
            command.CommandTimeout = 60;
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "";

                using (SqlDataReader reader = command.ExecuteReader()){
                    while (reader.Read()){   
                        data.CreatedBy = reader["createdBy"].ToString(); 
                        data.Tester = reader["Tester"].ToString();
                        data.ImageLocation = reader["ImageLocation"].ToString();
                        data.TestTime = DateTime.Parse(reader["TestTime"].ToString());
                        data.TestId = int.Parse(reader["TestId"].ToString());
                    }
                    connection.Close();             
                }
            return data;
        }

        // public void GetMax(){
        //     connectionString = ConfigurationManager.ConnectionStrings["i dont know what the connection is yet ? "].ToString();
        //     using (SqlConnection connection = new SqlConnection(connectionString)){
        //         //To get the max assembly number first, 
        //         string queryString = "Select MAX(AssemblyId) as maximum from TABLE";

        //         SqlCommand getMax = new SqlCommand(queryString, connection); 
        //         connection.Open();

        //         using (SqlDataReader reader = getMax.ExecuteReader()){
        //             while (reader.Read()){    
        //               // max = reader["AssemblyId"].ToString(); convert to int
        //             }
        //             connection.Close();
        //         }
        //     }
        // }
    }
}