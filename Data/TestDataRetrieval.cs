using System.Threading.Tasks;
using System.Linq;
using System;
using System.Data.SqlClient;
using System.Data; 
using System.Configuration; 

namespace BlazorApp.Data
{
    public class TestDataRetrieval{  
        private string connectionString = ConfigurationManager.AppSettings.Get("DBConnection");
        public Task<TestData[]> GetPreviousDataAsync(){
            return Task.FromResult(Enumerable.Range(1, 8).Select( index => GetRecentByTestRunId(index)).ToArray());
        }
        public Task<TestData[]> GetLastDataAsync(){
            return Task.FromResult(Enumerable.Range(1, 1).Select( index => GetRecentByTestRunId(index)).ToArray());
        }

        public TestData GetRecentByTestRunId(int index){
            Console.WriteLine(index);
            TestData data = new TestData();
            SqlConnection connection = new SqlConnection(connectionString); 
            connection.Open();

            SqlCommand command = new SqlCommand();
            command.CommandTimeout = 60;
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "spGetAllTestCasesByTestRunId";

            //increments down by 2 each time from the most recent test run 
            int recent = GetMaxTestRunId() - index * 2;

            command.Parameters.Add("testRunId", SqlDbType.BigInt).Value = recent;
                using (SqlDataReader reader = command.ExecuteReader()){
                    while (reader.Read()){
                        data.TestRunId = recent;
                        data.CreatedBy = reader["createdBy"].ToString(); 
                        data.ImagePath = reader["imagePath"].ToString();
                        data.CreatedDate = DateTime.Parse(reader["createdDate"].ToString());
                        data.TestCaseId = int.Parse(reader["testCaseId"].ToString());;
                        data.TestName = reader["testName"].ToString();
                        data.TestStatus = int.Parse(reader["testStatusId"].ToString());
                    }   
                    connection.Close();             
                }
            return data;
        }

        public int GetMaxTestRunId(){
            SqlConnection connection = new SqlConnection(connectionString); 
            connection.Open();

            SqlCommand command = new SqlCommand();
            command.CommandTimeout = 60;
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "spGetMaxTestRunId";
            return Convert.ToInt32(command.ExecuteScalar()); 
        }
    }
}