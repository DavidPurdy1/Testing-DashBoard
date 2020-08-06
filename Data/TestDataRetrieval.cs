using System.Threading.Tasks;
using System.Linq;
using System;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace BlazorApp.Data
{
    public class TestDataRetrieval
    {
        #region 
        private string connectionString = ConfigurationManager.AppSettings.Get("DBConnection");
        public int SearchId = 15; //search default case for the search page
        public bool allSelected = false;
        public int HomePageSearchId = 0; //value entered into the search bar on home page
        public string HomePageSearchString = ""; //The value if it is a date or testname
        public int searchMethod = 1;   //button selected on home page: 1 = test run id, 2 = test case id, 3 = date, 4 = test name
        public string RetrievalErrorMsg = "";
        #endregion
        public Task<TestData[]> GetPreviousRunsDataAsync()
        { //previous page
            if (SearchId < 0 || SearchId > GetTestRunCount() || allSelected)
            {
                SearchId = GetTestRunCount();
            }
            allSelected = false;
            return Task.FromResult(Enumerable.Range(1, SearchId).Select(index => GetRecentRunsByTestRunId(index)).ToArray());
        }
        public Task<TestData[]> GetHomeRunDataAsync()
        { //home page
            if (HomePageSearchId == 0 && HomePageSearchString == "")
            { //when nothing is entered in the blank, most recent test
                Console.WriteLine("nothing entered, most recent result returned");
                return Task.FromResult(Enumerable.Range(1, GetTestCaseCountByTestRunId(GetMaxTestRunId())).Select(index => GetMostRecentCasesByTestRunId(index)).ToArray());
            }
            else if (searchMethod == 1)
            { // if search method is 1 use entered test run id 
                Console.WriteLine("search by test run id");
                return Task.FromResult(Enumerable.Range(1, GetTestCaseCountByTestRunId(HomePageSearchId)).Select(index => GetCasesByTestRunId(index, HomePageSearchId)).ToArray());
            }
            else if (searchMethod == 2)
            { // if search method is 2 use entered test case id 
                Console.WriteLine("search by test case id");
                return Task.FromResult(Enumerable.Range(1, GetTestCaseCountByTestCaseId(HomePageSearchId)).Select(index => GetCaseByTestCaseId(index, HomePageSearchId)).ToArray());
            }
            else if (searchMethod == 3)
            { // if search method is 3 use entered date 
                Console.WriteLine("search by date");
                return Task.FromResult(Enumerable.Range(1, GetTestCaseCountByDate(HomePageSearchString)).Select(index => GetCasesByDate(index - 1, HomePageSearchString)).ToArray());
            }
            else if (searchMethod == 4)
            { // if search method is 4 use entered test name 
                Console.WriteLine("search by test name");
                return Task.FromResult(Enumerable.Range(1, GetTestCaseCountByTestName(HomePageSearchString)).Select(index => GetCasesByTestName(index - 1, HomePageSearchString)).ToArray());
            }
            else
            { //else return to the latest test
                Console.WriteLine("Unexpected");
                return Task.FromResult(Enumerable.Range(1, GetTestCaseCountByTestRunId(GetMaxTestRunId())).Select(index => GetMostRecentCasesByTestRunId(index)).ToArray());
            }
        }
        public TestData GetMostRecentCasesByTestRunId(int index)
        { //on the home page most recent test cases from test ran and default
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
            using (SqlDataReader reader = command.ExecuteReader())
            {
                DataTable table = new DataTable();
                table.Load(reader);
                data.TestCaseId = int.Parse(table.Rows[index - 1]["testCaseId"].ToString());
                data.CreatedBy = table.Rows[index - 1]["createdBy"].ToString();
                data.ImagePath = table.Rows[index - 1]["imagePath"].ToString();
                data.TestRunId = int.Parse(table.Rows[index - 1]["testRunId"].ToString());
                data.TestName = table.Rows[index - 1]["testName"].ToString();
                data.TestStatus = int.Parse(table.Rows[index - 1]["testStatusId"].ToString());
                data.CreatedDate = DateTime.Parse(table.Rows[index - 1]["createdDate"].ToString());


                // data.TestRunId = recent;
                // data.CreatedBy = reader["createdBy"].ToString();
                // data.ImagePath = reader["imagePath"].ToString();
                // data.CreatedDate = DateTime.Parse(reader["createdDate"].ToString());
                // data.TestCaseId = increment;
                // data.TestName = reader["testName"].ToString();
                // data.TestStatus = int.Parse(reader["testStatusId"].ToString());

                reader.Close();
                connection.Close();
            }
            return data;
        }
        public TestData GetCasesByTestRunId(int index, int HomePageSearchId)
        { //entry to search for test cases by run id 
            TestData data = new TestData();
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            SqlCommand command = new SqlCommand();
            command.CommandTimeout = 60;
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "spGetAllTestCasesByTestRunId";

            //inputted Run Id value to search for
            int recent = HomePageSearchId;
            int increment = GetTestCaseId(recent) - index;
            command.Parameters.Add("testRunId", SqlDbType.BigInt).Value = recent;
            using (SqlDataReader reader = command.ExecuteReader())
            {
                DataTable table = new DataTable();
                table.Load(reader);
                data.TestCaseId = int.Parse(table.Rows[index - 1]["testCaseId"].ToString());
                data.CreatedBy = table.Rows[index - 1]["createdBy"].ToString();
                data.ImagePath = table.Rows[index - 1]["imagePath"].ToString();
                data.TestRunId = int.Parse(table.Rows[index - 1]["testRunId"].ToString());
                data.TestName = table.Rows[index - 1]["testName"].ToString();
                data.TestStatus = int.Parse(table.Rows[index - 1]["testStatusId"].ToString());
                data.CreatedDate = DateTime.Parse(table.Rows[index - 1]["createdDate"].ToString());
                reader.Close();
                connection.Close();
            }
            return data;
        }
        public TestData GetRecentRunsByTestRunId(int index)
        { //previous page most recent tests ran, no case data 
            TestData data = new TestData();
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            SqlCommand command = new SqlCommand();
            command.CommandTimeout = 60;
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "spGetAllTestRunsByTestRunId";

            //increments down by 2 each time from the most recent test run 
            int recent;
            if (index > 1)
            {
                recent = GetMaxTestRunId() - index * 2;
            }
            else
            {
                recent = GetMaxTestRunId();
            }
            command.Parameters.Add("testRunId", SqlDbType.BigInt).Value = recent;
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
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
        public TestData GetCaseByTestCaseId(int index, int HomePageSearchId)
        { //test case id is unique, only will return one
            TestData data = new TestData();
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            SqlCommand command = new SqlCommand();
            command.CommandTimeout = 60;
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "spGetTestCaseByTestCaseId";
            //inputted value for test case Id 
            int recent = HomePageSearchId;

            command.Parameters.Add("testCaseId", SqlDbType.BigInt).Value = recent;

            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
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
        public TestData GetCasesByDate(int index, string HomePageSearchString)
        {
            TestData data = new TestData();
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();
            SqlCommand command = new SqlCommand();
            command.CommandTimeout = 60;
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "spGetAllCasesByTestDate";

            //gets rid of the am or pm and then converts the string to datetime
            DateTime recent;
            if (!DateTime.TryParse(HomePageSearchString, out recent))
            {
                RetrievalErrorMsg = "Improper Date Entry";
            }
            command.Parameters.Add("createdDate", SqlDbType.Date).Value = recent.Date;
            using (SqlDataReader reader = command.ExecuteReader())
            {
                DataTable table = new DataTable();
                table.Load(reader);
                data.TestCaseId = int.Parse(table.Rows[index]["testCaseId"].ToString());
                data.CreatedBy = table.Rows[index]["createdBy"].ToString();
                data.ImagePath = table.Rows[index]["imagePath"].ToString();
                data.TestRunId = int.Parse(table.Rows[index]["testRunId"].ToString());
                data.TestName = table.Rows[index]["testName"].ToString();
                data.TestStatus = int.Parse(table.Rows[index]["testStatusId"].ToString());
                data.CreatedDate = DateTime.Parse(table.Rows[index]["createdDate"].ToString());
                reader.Close();
                connection.Close();
            }
            return data;
        }
        public TestData GetCasesByTestName(int index, string HomePageSearchString)
        {
            TestData data = new TestData();
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            SqlCommand command = new SqlCommand();
            command.CommandTimeout = 60;
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "spGetAllCasesByTestName";

            //inputted test name 
            string recent = HomePageSearchString;

            command.Parameters.Add("testName", SqlDbType.NVarChar).Value = recent;
            using (SqlDataReader reader = command.ExecuteReader())
            {
                DataTable table = new DataTable();
                table.Load(reader);
                data.TestCaseId = int.Parse(table.Rows[index]["testCaseId"].ToString());
                data.CreatedBy = table.Rows[index]["createdBy"].ToString();
                data.ImagePath = table.Rows[index]["imagePath"].ToString();
                data.TestRunId = int.Parse(table.Rows[index]["testRunId"].ToString());
                data.TestName = recent;
                data.CreatedDate = DateTime.Parse(table.Rows[index]["createdDate"].ToString());
                data.TestStatus = int.Parse(table.Rows[index]["testStatusId"].ToString());
                reader.Close();
                connection.Close();
            }
            return data;
        }

        #region helper methods
        public int GetMaxTestRunId()
        {
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
        public int GetTestCaseCountByTestRunId(int runId)
        {
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
        public int GetTestCaseCountByTestCaseId(int testCaseId)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            SqlCommand command = new SqlCommand();
            command.CommandTimeout = 60;
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "spGetTestCaseCountByTestCaseId";
            command.Parameters.Add("testCaseId", SqlDbType.BigInt).Value = testCaseId;
            int value = Convert.ToInt32(command.ExecuteScalar());
            connection.Close();
            return value;
        }
        public int GetTestCaseCountByDate(string HomePageSearchString)
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            SqlCommand command = new SqlCommand();
            command.CommandTimeout = 60;
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "spGetTestCaseCountByDate";
            DateTime recent;
            if (!DateTime.TryParse(HomePageSearchString, out recent))
            {
                RetrievalErrorMsg = "Improper date format";
            }
            command.Parameters.Add("createdDate", SqlDbType.Date).Value = recent.Date;
            int value = Convert.ToInt32(command.ExecuteScalar());
            connection.Close();
            return value;
        }
        public int GetTestCaseCountByTestName(string HomePageSearchString)
        {
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
        public int GetTestCaseId(int runId)
        {
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
        public int GetTestRunCount()
        {
            SqlConnection connection = new SqlConnection(connectionString);
            connection.Open();

            SqlCommand command = new SqlCommand();
            command.CommandTimeout = 60;
            command.Connection = connection;
            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = "getTestRunsCount";
            int value = Convert.ToInt32(command.ExecuteScalar());
            connection.Close();
            return value;
        }
        #endregion
    }
}