using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PalladiumDwh.ClientReader.Core.Interfaces;
using PalladiumDwh.ClientReader.Core.Model;
using PalladiumDwh.Shared.Interfaces;
using PalladiumDwh.Shared.Model.DTO;

namespace PalladiumDwh.ClientReader.Infrastructure.Data
{
    public abstract class ExtractDbCommand<T> : IReadCommand<T> where T:ExtractRow
    {
        internal readonly IDbConnection Connection;
        internal readonly string CommandText;

        protected ExtractDbCommand(IDbConnection connection, string commandText)
        {
            Connection = connection;
            CommandText = commandText;
        }

        public virtual IEnumerable<T> Execute()
        {
            var extractList = new List<T>();
            using (Connection)
            {
                if (Connection.State != ConnectionState.Open)
                {
                    Connection.Open();
                }
                using (var command = Connection.CreateCommand())
                {
                    command.CommandText = CommandText;
                    var reader = command.ExecuteReader();
                    while (reader.Read())
                    {
                        var extract =(T) Activator.CreateInstance(typeof(T));
                        extract.Load(reader);
                        extractList.Add(extract);
                    }
                }
            }
            return extractList;
        }
    }
}
