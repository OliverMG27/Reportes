using System.Data;

namespace Reportes.Pages
{
    internal class SqlConnection
    {
        private string connectionString;

#pragma warning disable CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.
        public SqlConnection(SqlConnection connection)
#pragma warning restore CS8618 // Un campo que no acepta valores NULL debe contener un valor distinto de NULL al salir del constructor. Considere la posibilidad de declararlo como que admite un valor NULL.
        {
        }

        public SqlConnection(string connectionString)
        {
            this.connectionString = connectionString;
        }

        public ConnectionState State { get; internal set; }

        internal void Open()
        {
            throw new NotImplementedException();
        }
    }
}