namespace DAL;

public class MySQLOptions
{
    private static MySQLOptions _options;

    public static MySQLOptions Options
    {
        get
        {
            if (_options == null)
            {
                _options = new MySQLOptions();
            }
            return _options;
        }
    }

    private MySQLOptions()
    {
        
    }
    
    public string Host { get; set; } = "localhost";
    public string Password { get; set; } = "";
    public uint Port { get; set; } = 3306;
    public string Username { get; set; } = "root";
    public string Schema { get; set; } = "smashforcheezers";
}