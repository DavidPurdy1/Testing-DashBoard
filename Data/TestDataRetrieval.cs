using System.Threading.Tasks;
using System.Linq;
using System;
using System.Data.SqlClient;
using System.Configuration; 

namespace BlazorApp.Data
{
    public class TestDataRetrieval // make sure this can also just find the max results for the most recent test.
    {
        private int max;
        private string connectionString;

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
            return Task.FromResult(Enumerable.Range(1, 1).Select( index => new TestData{
                AssemblyId = index,
                //use AssemblyId to obtain other values
                TestId = 199, 
                TestTime = DateTime.Now.Date, 
                TestStatus = true,
                Tester = "david",
                ImageLocation = "location",
            }).ToArray());
        }

        //For this i think I want to find all the information about the tests by assembly numbers and return an array of the data for 1 test. This will run 8 times
        public TestData GetTestDataFromSql(int index){
            TestData data = new TestData();
            connectionString = ConfigurationManager.ConnectionStrings["i dont know what the connection is yet ? "].ToString();
            using (SqlConnection myConnection = new SqlConnection(connectionString)){
                //example query to type in
                int id = max - index;
                string queryString = "Select * from Employees where AssemblyId=@id";

                SqlCommand command = new SqlCommand(queryString, myConnection);

                //not exact for sure how commands work but this is an example 
                //command.Parameters.AddWithValue("@Fname", fName);         

                myConnection.Open(); //ensures that it will close on code exit in the using 

                using (SqlDataReader reader = command.ExecuteReader()){
                    while (reader.Read()){   
                        data.AssemblyId = id; 
                        data.Tester = reader["Tester"].ToString();
                        data.ImageLocation = reader["ImageLocation"].ToString();
                        data.TestTime = DateTime.Parse(reader["TestTime"].ToString());
                        data.TestId = int.Parse(reader["TestId"].ToString());
                    }
                    myConnection.Close();
                }               
            }
            return data;
        }
        public void GetMax(){
            connectionString = ConfigurationManager.ConnectionStrings["i dont know what the connection is yet ? "].ToString();
            using (SqlConnection myConnection = new SqlConnection(connectionString)){
                //To get the max assembly number first, 
                string queryString = "Select MAX(AssemblyId) as maximum from TABLE";

                SqlCommand getMax = new SqlCommand(queryString, myConnection); 
                myConnection.Open();

                using (SqlDataReader reader = getMax.ExecuteReader()){
                    while (reader.Read()){    
                      // max = reader["AssemblyId"].ToString(); convert to int
                    }
                    myConnection.Close();
                }
            }
        }
    }
        
}