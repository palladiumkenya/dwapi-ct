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
  public  class ReadPatientExtractDbCommand: IReadPatientExtractCommand
  {
      private readonly IDbConnection _connection;
      private readonly string _commandText;

      public ReadPatientExtractDbCommand(IDbConnection connection,string commandText)
      {
          _connection = connection;
          _commandText = commandText;
      }

      public IEnumerable<IPatientExtractRow> Execute()
      {
          var list = new List<IPatientExtractRow>();

          using (_connection)
          {
              if (_connection.State != ConnectionState.Open)
              {
                  _connection.Open();
              }

              using (var command=_connection.CreateCommand())
              {
                  command.CommandText = _commandText;
                  var reader = command.ExecuteReader();

                  while (reader.Read())
                  {
                      var data = new PatientExtractRow();
                      data.Load(reader);
                      list.Add(data);
                  }
              }
          }
          return list;
      }
  }
}
