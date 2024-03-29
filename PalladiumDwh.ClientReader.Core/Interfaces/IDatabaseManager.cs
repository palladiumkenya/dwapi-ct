﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using PalladiumDwh.ClientReader.Core.Model;
using PalladiumDwh.Shared.Model;

namespace PalladiumDwh.ClientReader.Core.Interfaces
{
    public interface IDatabaseManager
    {
        string DatabaseName { get; }
        bool CheckDatabaseExist(string provider, string connectionString);
        Task PreserveLiveApp();
        Task RestoreLiveApp();
        Task RunUpdateAsync(IProgress<DProgress> progress=null,bool seed=true);
        IDbConnection GetConnection(string provider, string connectionString);
        IDbConnection GetConnection(DatabaseConfig databaseConfig);
        DatabaseConfig GetDatabaseConfig(string provider, string connectionString);
        Task<bool> CheckServerConnection(DatabaseConfig databaseConfig);
        Task<bool> CheckConnection(DatabaseConfig databaseConfig);
        Task<bool> CheckAppConnection(DatabaseConfig databaseConfig, IProgress<DProgress> progress = null);       
        Task<List<string>> GetServersList(DatabaseType databaseType,IProgress<DProgress> progress = null);
        Task<List<string>> GetDatabaseList(DatabaseConfig databaseConfig, IProgress<DProgress> progress = null);
    }
}