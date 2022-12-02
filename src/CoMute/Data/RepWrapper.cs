using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;

namespace CoMute.Web.Data
{
    public class RepWrapper : IRepWrapper
    {
        private AppDbContext _appDbContext;
        private ICarpoolRep _carpoolRep;

        public RepWrapper(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public ICarpoolRep Carpools
        {
            get
            {
                if (_carpoolRep == null)
                {
                    _carpoolRep = new CarpoolRep(_appDbContext);
                }

                return _carpoolRep;
            }
        }

        public void Save()
        {
            _appDbContext.SaveChanges();
        }
    }

    public class RepWrapper2 : IRepWrapper2
    {
        private AppDbContext _appDbContext;
        private IUserRep _userRep;

        public RepWrapper2(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }
        public IUserRep AppUsers
        {
            get
            {
                if (_userRep == null)
                {
                    _userRep = new UserRep(_appDbContext);
                }

                return _userRep;
            }
        }

        public void Save()
        {
            try
            {
                // Your code...
                // Could also be before try if you know the exception occurs in SaveChanges
                _appDbContext.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
            
        }
    }
}