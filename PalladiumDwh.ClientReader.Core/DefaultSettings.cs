using System.Collections.Generic;

namespace PalladiumDwh.ClientReader.Core
{
    public class DefaultSettings
    {

        public static List<DwapiSetting> ProviderSettings = new List<DwapiSetting>
        {
            new DwapiSetting("System.Data.SqlClient", "Persist Security Info=True;MultipleActiveResultSets=True;Pooling=True;","Provider"),
            new DwapiSetting("MySql.Data.MySqlClient","convert zero datetime=True;", "Provider"),
            new DwapiSetting("Npgsql",string.Empty,"Provider")
        };

        
    }
}
