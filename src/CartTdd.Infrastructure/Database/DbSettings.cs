namespace CartTdd.Infrastructure.Database
{
    public class DbSettings
    {
        public string ConnectionString { get; private set; }
        public string DatabaseName { get; private set; }

        public DbSettings(string connectionString, string databaseName)
        {
            ConnectionString = connectionString;
            DatabaseName = databaseName;
        }
    }
}
