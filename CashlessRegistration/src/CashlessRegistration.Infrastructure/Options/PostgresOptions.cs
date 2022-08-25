namespace CashlessRegistration.Infrastructure.Options
{
    public class PostgresOptions
    {
        public string Host { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string Database { get; set; }
        public string Port { get; set; }

        public string ConnectionString
        {
            get => $"Server={Host};Port={Port};Database={Database};User Id={UserName};Password={Password};Include Error Detail=true;";
        }
    }
}
