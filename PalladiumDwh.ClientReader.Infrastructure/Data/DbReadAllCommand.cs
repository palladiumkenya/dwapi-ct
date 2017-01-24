using System.Collections.Generic;
using System.Data;
using PalladiumDwh.ClientReader.Core.Interfaces;

namespace PalladiumDwh.ClientReader.Infrastructure.Data
{
    public class DbReadAllCommand<T>:IReadAllCommand<T> where T:class
    {
        private readonly IDbConnection _connection;

        public DbReadAllCommand(IDbConnection connection)
        {
            _connection = connection;
        }

        public IEnumerable<T> Execute()
        {
            using (_connection)
            {
                if (_connection.State != ConnectionState.Open)
                {
                    _connection.Open();
                }
                using (IDbCommand command = _connection.CreateCommand())
                {
                    command.CommandText = "";
                    IDataReader reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                     
                    }

                }
            }
            throw new System.NotImplementedException();
        }
    }
}