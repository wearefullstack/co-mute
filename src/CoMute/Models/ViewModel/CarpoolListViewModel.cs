using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CoMute.Web.Models.ViewModel
{
    public class CarpoolListViewModel
    {
        public IEnumerable<Carpool> Carpools { get; set; }
        public PagingInfo PagingInfo { get; set; }
        public string myId { get; set; }
    }
}