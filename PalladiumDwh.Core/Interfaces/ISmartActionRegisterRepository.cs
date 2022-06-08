using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using PalladiumDwh.Core.Model;
using PalladiumDwh.Shared.Interfaces;

namespace PalladiumDwh.Core.Interfaces
{
    public interface ISmartActionRegisterRepository : IRepository<SmartActionRegister>
    {
        Task<bool> Clear(int siteCode);
        void CreateAction(List<SmartActionRegister> actionRegisters);
    }
}