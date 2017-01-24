using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using PalladiumDwh.ClientReader.Core.Interfaces;
using PalladiumDwh.Shared.Model;
using PalladiumDwh.Shared.Model.DTO;

namespace PalladiumDwh.ClientReader.Infrastructure.Data
{
    public class PatientReader:IPatientReader
    {
        private readonly IDbConnection _connection;

        public PatientReader(IDbConnection connection)
        {
            _connection = connection;
        }

        public IEnumerable<PatientExtractDTO> Read(string sqlQuery)
        {
            var list=new List<PatientExtractDTO>();
            string sql = sqlQuery;

            IDbCommand command = _connection.CreateCommand();
            command.CommandType = CommandType.Text;
            command.CommandText = sqlQuery;

            using (IDataReader reader = command.ExecuteReader())
            {
                
                while (reader.Read())
                {   
                 
                    // get data from the reader
                }
            }
            throw new System.NotImplementedException();
        }
    }
}