namespace Reportes.Pages
{
    internal class SqlConnection
    {
        private string connectionString;

        public SqlConnection(SqlConnection connection)
        {
        }

        public SqlConnection(string connectionString)
        {
            this.connectionString = connectionString;
        }
    }
}