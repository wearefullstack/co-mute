using Co_Mute.Api.Models;
using Co_Mute.Api.Models.Dto;
using Microsoft.AspNetCore.Mvc;

namespace Co_Mute.Api.Repository
{
    public interface ICarPoolTicketRepository
    {
        public Task<IEnumerable<FindCarPoolTicket>> FindCarPoolTickets(SearchCarPoolTicketsDto searchCarPoolTicketsDto, int id);
        public Task<IEnumerable<CarPoolTicket>> GetCreatedCarPoolTicketsByUserId(int id);
        public Task<IEnumerable<CarPoolTicket>> GetRegisteredCarPoolTicketsByUserId(int id);
        public Task<int> CreateCarPoolTicket(CreateCarPoolTicket oCreateCarPoolTicket, int id);
        public Task<int> CancelCreatedCarPoolTicketID(FunctionCommandUser functionCommandUser, int id);
        public Task<int> UpdateCarPoolTicketDetails(UpdateCarPoolTicketDetailsDto updateCarPoolTicketDetailsDto, int id);
        public Task<int> JoinCarPoolTicket(FunctionCommandCarPoolTicketDto oFunctionCommandCarPoolTicketDto, int id);
        public Task<int> CancelJoinCarPoolTicket(FunctionCommandCarPoolTicketDto oFunctionCommandCarPoolTicketDto, int id);
        public Task<IEnumerable<CarPoolTicketDetails>> GetCarPoolTicketDetailsById(int iId);
       
    }
}
