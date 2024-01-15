namespace WebApplication1.Helpers.Utils
{
    public static class GlobalAttributes
    {
        public static MySqlConfiguration mySqlConfiguration = new MySqlConfiguration();
    }

    public class MySqlConfiguration
    {
        public string connectionString { get; set; }
    }
}
