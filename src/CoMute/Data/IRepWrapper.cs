using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CoMute.Web.Data
{
    public interface IRepWrapper
    {
        ICarpoolRep Carpools { get; }

        void Save();
    }

    public interface IRepWrapper2
    {
        IUserRep AppUsers { get; }

        void Save();
    }
}
