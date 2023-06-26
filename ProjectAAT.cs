// Database connection 
using System;
using System.Data.SqlClient;

public class DatabaseManager
{
    private string connectionString;
    private SqlConnection connection;

    public DatabaseManager(string connectionString)
    {
        this.connectionString = connectionString;
        connection = new SqlConnection(connectionString);
    }

    public void OpenConnection()
    {
        connection.Open();
        Console.WriteLine("Connection opened successfully.");
    }

    public void CloseConnection()
    {
        connection.Close();
        Console.WriteLine("Connection closed successfully.");
    }
}

// Table creation
public class TableCreator
{
    private DatabaseManager databaseManager; // Dependency Injection:

    public TableCreator(DatabaseManager databaseManager)
    {
        this.databaseManager = databaseManager;
    }

    public void CreateTable()
    {
        string createTableQuery = "CREATE TABLE Resources (" +
            "Id INT IDENTITY(1,1) PRIMARY KEY," +
            "Name NVARCHAR(100) NOT NULL," +
            "Type NVARCHAR(50) NOT NULL," +
            "CloudProvider NVARCHAR(50) NOT NULL)";

        using (SqlCommand command = new SqlCommand(createTableQuery, databaseManager.Connection))
        {
            command.ExecuteNonQuery();
            Console.WriteLine("Table created successfully.");
        }
    }
}

// Data inserter
public class DataInserter
{
    private DatabaseManager databaseManager;

    public DataInserter(DatabaseManager databaseManager)
    {
        this.databaseManager = databaseManager;
    }

    public void InsertData(string name, string type, string cloudProvider)
    {
        string insertDataQuery = "INSERT INTO Resources (Name, Type, CloudProvider) VALUES (@Name, @Type, @CloudProvider)";

        using (SqlCommand command = new SqlCommand(insertDataQuery, databaseManager.Connection))
        {
            command.Parameters.AddWithValue("@Name", name);
            command.Parameters.AddWithValue("@Type", type);
            command.Parameters.AddWithValue("@CloudProvider", cloudProvider);

            command.ExecuteNonQuery();
            Console.WriteLine("Data inserted successfully.");
        }
    }
}
// Data retrieval
public class DataRetriever
{
    private DatabaseManager databaseManager;

    public DataRetriever(DatabaseManager databaseManager)
    {
        this.databaseManager = databaseManager;
    }

    public void RetrieveData()
    {
        string retrieveDataQuery = "SELECT * FROM Resources";

        using (SqlCommand command = new SqlCommand(retrieveDataQuery, databaseManager.Connection))
        {
            using (SqlDataReader reader = command.ExecuteReader())
            {
                while (reader.Read())
                {
                    int id = (int)reader["Id"];
                    string name = (string)reader["Name"];
                    string type = (string)reader["Type"];
                    string cloudProvider = (string)reader["CloudProvider"];

                    Console.WriteLine($"Id: {id}, Name: {name}, Type: {type}, CloudProvider: {cloudProvider}");
                }
            }
        }
    }
}
Remember to modify the table structure and queries based on your specific requirements. Additionally, make sure to handle exceptions, implement error checking, and dispose of resources properly for production-ready code.






