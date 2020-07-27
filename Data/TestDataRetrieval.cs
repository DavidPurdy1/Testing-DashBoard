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
        public int SearchId = 36;   
        public Task<TestData[]> GetPreviousRunsDataAsync(){
            return Task.FromResult(Enumerable.Range(1, 15).Select( index => GetRecentRunsByTestRunId(index)).ToArray());
        }
        public Task<TestData[]> GetLastRunDataAsync(){
            return Task.FromResult(Enumerable.Range(1, GetTestCaseCount(GetMaxTestRunId())).Select( index => GetRecentCasesByTestRunId(index)).ToArray());
        }
        public Task<TestData[]> GetSearchDataAsync(){
            return Task.FromResult(Enumerable.Range(1,GetTestCaseCount(SearchId)).Select( index => SearchDB(SearchId, index)).ToArray());
        }
        public TestData GetRecentCasesByTestRunId(int index){
            TestData data = new TestData();
            SqlConnection connection = new SqlConnection(connectionString); 
            connection.Open();

            SqlCommand command = new SqlCommand();
            command.CommandTimeout = 60;
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "spGetAllTestCasesByTestRunId";

            //max test run
            int recent = GetMaxTestRunId();
            int increment = GetTestCaseId(recent) - index; 
            command.Parameters.Add("testRunId", SqlDbType.BigInt).Value = recent;
                using (SqlDataReader reader = command.ExecuteReader()){
                    while (reader.Read()){
                        data.TestRunId = recent;
                        data.CreatedBy = reader["createdBy"].ToString(); 
                        data.ImagePath = reader["imagePath"].ToString();
                        data.CreatedDate = DateTime.Parse(reader["createdDate"].ToString());
                        data.TestCaseId = increment;
                        data.TestName = reader["testName"].ToString();
                        data.TestStatus = int.Parse(reader["testStatusId"].ToString());
                    }   
                    reader.Close(); 
                    connection.Close();             
                }
            return data;
        }
        public TestData GetRecentRunsByTestRunId(int index){
            TestData data = new TestData();
            SqlConnection connection = new SqlConnection(connectionString); 
            connection.Open();

            SqlCommand command = new SqlCommand();
            command.CommandTimeout = 60;
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "spGetAllTestRunsByTestRunId";

            //increments down by 2 each time from the most recent test run 
            int recent = GetMaxTestRunId() - index * 2;

            command.Parameters.Add("testRunId", SqlDbType.BigInt).Value = recent;
                using (SqlDataReader reader = command.ExecuteReader()){
                    while (reader.Read()){
                        data.TestRunId = recent;
                        data.CreatedBy = reader["createdBy"].ToString(); 
                        data.ApplicationName = reader["applicationName"].ToString();
                        data.ApplicationVersion = reader["applicationVersion"].ToString(); 
                        data.TestsFailed = int.Parse(reader["testsFailed"].ToString()); 
                        data.TestsPassed = int.Parse(reader["testPassed"].ToString()); 
                        data.CreatedDate = DateTime.Parse(reader["createdDate"].ToString());
                    }   
                    reader.Close();
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
            int value = Convert.ToInt32(command.ExecuteScalar()); 
            connection.Close();
            return value; 
        }
        public int GetTestCaseCount(int runId){
            SqlConnection connection = new SqlConnection(connectionString); 
            connection.Open();

            SqlCommand command = new SqlCommand();
            command.CommandTimeout = 60;
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "spGetTestCaseCountByTestRun";
            command.Parameters.Add("testRunId", SqlDbType.BigInt).Value = runId;
            int value = Convert.ToInt32(command.ExecuteScalar());
            connection.Close(); 
            return value; 
        }
        public int GetTestCaseId(int runId){
            SqlConnection connection = new SqlConnection(connectionString); 
            connection.Open();

            SqlCommand command = new SqlCommand();
            command.CommandTimeout = 60;
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "spGetTestCaseId";
            command.Parameters.Add("testRunId", SqlDbType.BigInt).Value = runId;
            int value = Convert.ToInt32(command.ExecuteScalar());
            connection.Close();
            return value;  
        }
        public TestData SearchDB(int id,int index){
            TestData data = new TestData();
            SqlConnection connection = new SqlConnection(connectionString); 
            connection.Open();

            SqlCommand command = new SqlCommand();
            command.CommandTimeout = 60;
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "spGetAllTestCasesByTestRunId";

            int increment = GetTestCaseId(id) - index; 
            command.Parameters.Add("testRunId", SqlDbType.BigInt).Value = id;
                using (SqlDataReader reader = command.ExecuteReader()){
                    while (reader.Read()){
                        data.TestRunId = id;
                        data.CreatedBy = reader["createdBy"].ToString(); 
                        data.ImagePath = reader["imagePath"].ToString();
                        data.CreatedDate = DateTime.Parse(reader["createdDate"].ToString());
                        data.TestCaseId = increment; 
                        data.TestName = reader["testName"].ToString();
                        data.TestStatus = int.Parse(reader["testStatusId"].ToString());
                    }   
                    reader.Close();
                    connection.Close();             
                }
            return data;
        } 
    }
}