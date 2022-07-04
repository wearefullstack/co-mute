using CoMute.Web.Models.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoMute.Web.Interface
{
    public interface ICarPoolRepository : IDisposable
    {
        IEnumerable<CarPool> GetCarPools();
        CarPool GetOwnerByID(int OwnerID);
        void InsertCarPool(CarPool carPool);
        void DeleteCarPool(int Id);
        void UpdateCarPool(CarPool carPool);
        void Save();
        new void Dispose();
    }
}
