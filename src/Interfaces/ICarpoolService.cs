using FSWebApi.Dto.Carpool;
using FSWebApi.Models;

namespace FSWebApi.Interfaces
{
    public interface ICarpoolService
    {
        public IEnumerable<CarpoolDTO> GetAllCarpools();
        public IEnumerable<CarpoolDTO> GetAvailableCarpools(Guid userId);
        public IEnumerable<CarpoolDTO> GetJoinedCarpools(Guid userId);
        public IEnumerable<CarpoolDTO> GetCreatedCarpools(Guid userId);
        public CarpoolDTO RegisterCarpool(CreateCarpoolDTO carpool);
        public CarpoolDTO JoinCarpool(JoinCarpoolDTO joinCarpoolDTO);
        public bool ExitCarpool(Guid carpoolId, Guid userId);
  
    }
}
