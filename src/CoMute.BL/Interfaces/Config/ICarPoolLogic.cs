using CoMute.BE;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoMute.BL
{
    public interface ICarPoolLogic
    {

        #region Ready
        CarPool getCarPool(int CarPoolId);
        List<CarPool> GetCarPoolByUserId(int UserId);
        List<CarPool> GetAllCarPools();
        List<CarPool> CarPoolSearch(string SearchText = null);
        TimeSpan GetDuration(DateTime DepartureTime, DateTime ExpectedArrivalTime);
        bool Overlaps(DateTime DepartureTime, DateTime ExpectedArrivalTim, string Destination, DateTime DateCreated, int UserId);
        #endregion

        #region Modify
        void AddCarPool(CarPool carPool);
        void UpdateCarPool(CarPool carPool);
        void DeleteCarPool(CarPool carPool);
        #endregion
    }
}
