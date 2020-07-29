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
        public int SearchId = 36; //search default case for the search page
        public int HomePageSearchId = 0; //value entered into the search bar on home page
        public string HomePageSearchString = ""; //The value if it is a date or testname
        public int searchMethod = 1;   //button selected on home page: 1 = test run id, 2 = test case id, 3 = date, 4 = test name

        public Task<TestData[]> GetPreviousRunsDataAsync(){ //previous page
            return Task.FromResult(Enumerable.Range(1, 15).Select( index => GetRecentRunsByTestRunId(index)).ToArray());
        }
        public Task<TestData[]> GetHomeRunDataAsync(){ //home page
            if(HomePageSearchId==0 && HomePageSearchString == ""){ //when nothing is entered in the blank, most recent test
                Console.WriteLine("nothing entered, max"); 
                return Task.FromResult(Enumerable.Range(1, GetTestCaseCount(GetMaxTestRunId())).Select( index => GetMostRecentCasesByTestRunId(index)).ToArray()); 
            }else if(searchMethod == 1){ // if search method is 1 use entered test run id 
                Console.WriteLine("search by test run id"); 
                return Task.FromResult(Enumerable.Range(1, GetTestCaseCount(HomePageSearchId)).Select( index => GetCasesByTestRunId(index, HomePageSearchId)).ToArray()); 
            }else if(searchMethod == 2){ // if search method is 2 use entered test case id 
                Console.WriteLine("search by test case id");
                return Task.FromResult(Enumerable.Range(1, 1).Select( index => GetCaseByTestCaseId(index, HomePageSearchId)).ToArray());
            }else if(searchMethod == 3){ // if search method is 3 use entered date 
                Console.WriteLine("search by date");
                return Task.FromResult(Enumerable.Range(1, GetTestCaseCountByDate(HomePageSearchString)).Select( index => GetCasesByDate(index-1, HomePageSearchString)).ToArray());
            }else if(searchMethod == 4){ // if search method is 4 use entered test name 
                Console.WriteLine("search by test name");
                return Task.FromResult(Enumerable.Range(1, GetTestCaseCountByTestName(HomePageSearchString)).Select( index => GetCasesByTestName(index-1, HomePageSearchString)).ToArray());
            }else{ //else return to the latest test
                Console.WriteLine("Unexpected");
                return Task.FromResult(Enumerable.Range(1, GetTestCaseCount(GetMaxTestRunId())).Select( index => GetMostRecentCasesByTestRunId(index)).ToArray());  
            }
        }
        public Task<TestData[]> GetSearchDataAsync(){ //search page
            return Task.FromResult(Enumerable.Range(1, GetTestCaseCount(SearchId)).Select( index => SearchDB(SearchId, index)).ToArray());
        }
        public TestData GetMostRecentCasesByTestRunId(int index){ //on the home page most recent test cases from test ran and default
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
        public TestData GetCasesByTestRunId(int index, int HomePageSearchId){ //entry to search for test cases by run id 
            TestData data = new TestData();
            SqlConnection connection = new SqlConnection(connectionString); 
            connection.Open();

            SqlCommand command = new SqlCommand();
            command.CommandTimeout = 60;
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "spGetAllTestCasesByTestRunId";
            //max test run
            int recent = HomePageSearchId;
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
        public TestData GetRecentRunsByTestRunId(int index){ //previous page most recent tests ran, no case data 
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
        public TestData GetCaseByTestCaseId(int index, int HomePageSearchId){ //test case id is unique, only will return one
            TestData data = new TestData();
            SqlConnection connection = new SqlConnection(connectionString); 
            connection.Open();

            SqlCommand command = new SqlCommand();
            command.CommandTimeout = 60;
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "spGetTestCaseByTestCaseId";

            int recent = HomePageSearchId;

            command.Parameters.Add("testCaseId", SqlDbType.BigInt).Value = recent;

                using (SqlDataReader reader = command.ExecuteReader()){
                    while (reader.Read()){
                        data.TestCaseId = recent;
                        data.TestRunId = int.Parse(reader["testRunId"].ToString()); 
                        data.CreatedBy = reader["createdBy"].ToString(); 
                        data.ImagePath = reader["imagePath"].ToString();
                        data.CreatedDate = DateTime.Parse(reader["createdDate"].ToString());
                        data.TestName = reader["testName"].ToString();
                        data.TestStatus = int.Parse(reader["testStatusId"].ToString());
                    }   
                    reader.Close();
                    connection.Close();             
                }
            return data;
        }
        public TestData GetCasesByDate(int index, string HomePageSearchString ){ 
            TestData data = new TestData();
            SqlConnection connection = new SqlConnection(connectionString); 
            connection.Open();

            SqlCommand command = new SqlCommand();
            command.CommandTimeout = 60;
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "spGetAllTestCasesByTestDate";

            //increments down by 2 each time from the most recent test run 
            DateTime recent = DateTime.Parse(HomePageSearchString);

            command.Parameters.Add("createdDate", SqlDbType.DateTime).Value = recent;
                using (SqlDataReader reader = command.ExecuteReader()){
                    DataTable table = new DataTable(); 
                    table.Load(reader); 
                        data.TestCaseId =int.Parse(table.Rows[index]["testCaseId"].ToString());
                        data.CreatedBy = table.Rows[index]["createdBy"].ToString(); 
                        data.ImagePath = table.Rows[index]["imagePath"].ToString(); 
                        data.TestRunId = int.Parse(table.Rows[index]["testRunId"].ToString()); 
                        data.TestName = table.Rows[index]["testName"].ToString(); 
                        data.TestStatus = int.Parse(table.Rows[index]["testStatusId"].ToString()); 
                        // data.TestCaseId = int.Parse(reader["testCaseId"].ToString()); 
                        // data.CreatedBy = reader["createdBy"].ToString(); 
                        // data.ImagePath = reader["imagePath"].ToString();
                        // data.TestRunId = int.Parse(reader["testRunId"].ToString()); 
                        // data.TestName = reader["testName"].ToString();
                        // data.TestStatus = int.Parse(reader["testStatusId"].ToString());
                        data.CreatedDate = recent;   
                    reader.Close();
                    connection.Close();             
                }
            return data;
        }
        public TestData GetCasesByTestName(int index, string HomePageSearchString){
            TestData data = new TestData();
            SqlConnection connection = new SqlConnection(connectionString); 
            connection.Open();

            SqlCommand command = new SqlCommand();
            command.CommandTimeout = 60;
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "spGetAllCasesByTestName";

            //increments down by 2 each time from the most recent test run 
            string recent = HomePageSearchString; 

            command.Parameters.Add("testName", SqlDbType.NVarChar).Value = recent;
                using (SqlDataReader reader = command.ExecuteReader()){
                    DataTable table = new DataTable(); 
                    table.Load(reader);
                        data.TestCaseId = int.Parse(table.Rows[index]["testCaseId"].ToString());
                        data.CreatedBy = table.Rows[index]["createdBy"].ToString(); 
                        data.ImagePath = table.Rows[index]["imagePath"].ToString(); 
                        data.TestRunId = int.Parse(table.Rows[index]["testRunId"].ToString()); 
                        data.TestName = recent; 
                        data.CreatedDate = DateTime.Parse(table.Rows[index]["createdDate"].ToString());
                        data.TestStatus = int.Parse(table.Rows[index]["testStatusId"].ToString()); 
                    // while (reader.Read()){
                    //     data.TestRunId = int.Parse(reader["testRunId"].ToString());
                    //     data.CreatedBy = reader["createdBy"].ToString(); 
                    //     data.ImagePath = reader["imagePath"].ToString();
                    //     data.CreatedDate = DateTime.Parse(reader["createdDate"].ToString());
                    //     data.TestCaseId = int.Parse(reader["testCaseId"].ToString());
                    //     data.TestName = recent; 
                    //     data.TestStatus = int.Parse(reader["testStatusId"].ToString());
                    // }   
                    reader.Close();
                    connection.Close();             
                }
            return data;
        }

        #region helper
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
        public int GetTestCaseCountByDate(string HomePageSearchString){
            SqlConnection connection = new SqlConnection(connectionString); 
            connection.Open();

            SqlCommand command = new SqlCommand();
            command.CommandTimeout = 60;
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "spGetTestCaseCountByDate";
            command.Parameters.Add("createdDate", SqlDbType.DateTime).Value = DateTime.Parse(HomePageSearchString);
            int value = Convert.ToInt32(command.ExecuteScalar());
            connection.Close(); 
            return value; 
        }
        public int GetTestCaseCountByTestName(string HomePageSearchString){
            SqlConnection connection = new SqlConnection(connectionString); 
            connection.Open();

            SqlCommand command = new SqlCommand();
            command.CommandTimeout = 60;
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "spGetTestCaseCountByTestName";
            command.Parameters.Add("testName", SqlDbType.NVarChar).Value = HomePageSearchString;
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
        #endregion
        public TestData SearchDB(int id, int index){
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