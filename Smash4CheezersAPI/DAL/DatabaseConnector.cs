using MySqlConnector;

namespace DAL;

public class DatabaseConnector
{
    private string? _connectionString;

    public DatabaseConnector(MySQLOptions options)
    {
        var initializer = new MySqlConnectionStringBuilder
        {
            Server = options.Host,
            Port = options.Port,
            Database = options.Schema,
            UserID = options.Username,
            Password = options.Password,
            Pooling = true
        };
        _connectionString = initializer.ConnectionString;
    }
}