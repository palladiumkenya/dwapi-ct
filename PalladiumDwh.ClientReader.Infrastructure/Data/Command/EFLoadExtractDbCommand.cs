using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.SqlClient;
using System.Reflection;
using System.Transactions;
using log4net;
using PalladiumDwh.ClientReader.Core.Interfaces.Commands;
using PalladiumDwh.ClientReader.Core.Model.Source;
using Dapper;
using PalladiumDwh.Shared.Model;

namespace PalladiumDwh.ClientReader.Infrastructure.Data.Command
{
    public abstract class EFLoadExtractDbCommand<T> : ILoadExtractCommand<T> where T : TempExtract
    {
        internal static readonly ILog Log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        internal readonly IDbConnection SourceConnection;
        internal DbContext DbContext;
        internal readonly string CommandText;
        internal readonly int BatchSize;

        protected EFLoadExtractDbCommand(IDbConnection sourceConnection, DbContext context, string commandText,
            int batchSize = 100)
        {
            SourceConnection = sourceConnection;
            DbContext = context;
            DbContext.Configuration.AutoDetectChangesEnabled = false;
            CommandText = commandText;
            BatchSize = batchSize;
        }

        public virtual void Execute()
        {
            using (SourceConnection)
            {
                if (SourceConnection.State != ConnectionState.Open)
                {
                    SourceConnection.Open();
                }
                using (var command = SourceConnection.CreateCommand())
                {
                    command.CommandText = CommandText;
                    using (var reader = command.ExecuteReader())
                    {
                        if (null != reader)
                        {
                            using (var tx = new TransactionScope())
                            {
                                DbContext = null;
                                try
                                {
                                    DbContext = new DwapiRemoteContext();
                                    DbContext.Configuration.AutoDetectChangesEnabled = false;

                                    int count = 0;
                                    while (reader.Read())
                                    {
                                        var extract = (T) Activator.CreateInstance(typeof(T));
                                        extract.Load(reader);
                                        if (!extract.HasError)
                                        {
                                            ++count;
                                            DbContext = AddToContext(extract, count, BatchSize, true);
                                        }
                                    }
                                    DbContext.SaveChanges();
                                }
                                finally
                                {

                                    if (null != DbContext)
                                        DbContext.Dispose();
                                }

                                tx.Complete();
                            }

                        }
                    }
                }
            }
        }

        private DbContext AddToContext(T entity, int count, int commitCount, bool recreateContext)
        {
            DbContext.Set<T>().Add(entity);

            if (count%commitCount == 0)
            {
                DbContext.SaveChanges();
                if (recreateContext)
                {
                    DbContext.Dispose();
                    DbContext = new DwapiRemoteContext();
                    DbContext.Configuration.AutoDetectChangesEnabled = false;
                }
            }

            return DbContext;
        }
    }
}
