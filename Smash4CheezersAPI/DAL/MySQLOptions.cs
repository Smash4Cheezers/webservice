namespace DAL;

public class MySqlOptions
{
    private static MySqlOptions _options;

    private MySqlOptions()
    {
    }

    public static MySqlOptions Options
    {
        get
        {
            if (_options == null) _options = new MySqlOptions();
            return _options;
        }
    }

    public string Host { get; set; } = "localhost";
    public string Password { get; set; } = "";
    public uint Port { get; set; } = 3306;
    public string Username { get; set; } = "root";
    public string Schema { get; set; } = "smashforcheezers";
}