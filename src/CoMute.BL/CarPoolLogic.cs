using CoMute.BE;
using CoMute.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoMute.BL
{
    public class CarPoolLogic : ICarPoolLogic
    {
        CarPoolRepository obj=new CarPoolRepository();

        //TODO Mapper and IUnitOfWork
        //private readonly IUnitOfWork unitOfWork;
        //private GenericRepository<CarPool> carPoolRepository;

        //public CarPoolLogic(IUnitOfWork unitOfWork)
        //{
        //    this.unitOfWork = unitOfWork;
        //    this.carPoolRepository = unitOfWork.GetRepository<CarPool>();
        //}

        #region Read
        //This is the get method using CarPoolRepository obj to call getEntity on the data layer, the get method uses an id to get the specific CarPool 
        public CarPool getCarPool(int CarPoolId)
        {

            var carPool = obj.GetEntity(CarPoolId);
            return carPool;

        }

        //This is the Index method using CarPoolRepository obj to call GetAllCarPools method to display all CarPools
        public List<CarPool> GetAllCarPools()
        {

            var list = obj.SelectAll();
            return list.ToList();

        }

        //This is the search method for CarPool
        //Search Travel Opportunities
        public List<CarPool> CarPoolSearch(string SearchText = null)
        {
            var CarPoolQry = obj.SelectAll().ToList();

            if (!string.IsNullOrEmpty(SearchText))
            {
                SearchText = SearchText.Trim().ToLower();
                //CarPool List will search by the following fields
                var SearchList = CarPoolQry.AsEnumerable().Where(CarPoolItems =>
                                          (CarPoolItems.OwnerLeader != null && CarPoolItems.OwnerLeader.ToLower().Contains(SearchText)) ||
                                          (CarPoolItems.Origin != null && CarPoolItems.Origin.ToLower().Contains(SearchText)) ||
                                          (CarPoolItems.Notes != null && CarPoolItems.Notes.ToLower().Contains(SearchText)) ||
                                          (CarPoolItems.Destination != null && CarPoolItems.Destination.ToLower().Contains(SearchText)) ||
                                          (CarPoolItems.AvailableSeats.ToString() != null && CarPoolItems.AvailableSeats.ToString().ToLower().Contains(SearchText)) ||
                                          (CarPoolItems.DaysAvailable.ToString().ToLower().Contains(SearchText))

                                          ).ToList();

                return SearchList;
            }

            var CarPoolLst = CarPoolQry.ToList();

            return CarPoolLst;
        }

        //GetCarPoolByUserId method will get all CarPools for that specific user
        //A listing of all car-pool opportunities created by a user
        public List<CarPool> GetCarPoolByUserId(int UserId)
        {


            var GetCarPool = obj.SelectAll().Where(x => x.UserId == UserId).ToList();


            return GetCarPool;
        }


        public bool CheckCarPoolExit(DateTime DepartureTime, DateTime ExpectedArrivalTime, string Destination)
        {
            var CarPoolExit = false;

            var list = obj.SelectAll().Where(x => x.DepartureTime == DepartureTime && x.ExpectedArrivalTime == ExpectedArrivalTime && x.Destination == Destination).ToList();
            if (list.Count > 0)
            {
                CarPoolExit = true;
            }


            return CarPoolExit;
        }

        //Check for overlapping 
        public bool Overlaps(DateTime DepartureTime, DateTime ExpectedArrivalTim, string Destination, DateTime DateCreated, int UserId)
        {
            bool OverLaps = false;
            var list = obj.SelectAll().ToList();
            var CheckExist = CheckCarPoolExit(DepartureTime, ExpectedArrivalTim, Destination);
            if (CheckExist == true)
            {
                OverLaps = true;
            }
            else
            {
                list = obj.SelectAll().Where(x => x.UserId == UserId && x.DepartureTime != DepartureTime && x.ExpectedArrivalTime != ExpectedArrivalTim && x.DateCreated == DateCreated).ToList(); 
            }
            foreach (var item in list)
            {
                if (list.Count>0)
                {
                    if (DepartureTime < item.ExpectedArrivalTime && item.DepartureTime < ExpectedArrivalTim)
                    {
                        OverLaps = true;

                    }
                }
            }
           
           
            return OverLaps;



        }

        public TimeSpan GetDuration(DateTime DepartureTime, DateTime ExpectedArrivalTime)
        {
            return ExpectedArrivalTime - DepartureTime;
        }
        #endregion Read


        #region Modify
        //This is the Add method using CarPoolRepository obj to call Insert method on the data layer,add new CarPool to the database
        public void AddCarPool(CarPool carPool)
        {
            obj.Insert(carPool);

        }

        //This is the Update method using CarPoolRepository obj to call update method on the data layer,modifies CarPool info from the database
        public void UpdateCarPool(CarPool carPool)
        {
            obj.Update(carPool);
        }

        //This is the Delete method using CarPoolRepository obj to call delete method on the data layer. It deletes the CarPool info from the database
        public void DeleteCarPool(CarPool carPool)
        {
            obj.Delete(carPool);
        }
        #endregion Modify

    }
}

