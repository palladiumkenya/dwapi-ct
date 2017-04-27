
using System;
using System.Data;
using PalladiumDwh.ClientReader.Core.Model;

namespace PalladiumDwh.ClientReader.Core.Interfaces.Repository
{
    public interface IEMRRepository: IClientRepository<EMR>
    {
        EMR GetDefault();
        void SetEmrAsDefault(Guid id);
        IDbConnection GetConnection();
        IDbConnection GetEmrConnection();
    }
}