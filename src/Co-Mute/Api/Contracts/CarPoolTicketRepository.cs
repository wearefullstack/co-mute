using Co_Mute.Api.Contexts;
using Co_Mute.Api.Models;
using Co_Mute.Api.Models.Dto;
using Co_Mute.Api.Repository;
using System.Data.SqlClient;
using System.Data;
using Dapper;
using Microsoft.AspNetCore.Mvc;
using System.Data.Common;

namespace Co_Mute.Api.Contracts
{
    public class CarPoolTicketRepository : ICarPoolTicketRepository
    {
        private readonly SqlConnectionContext _oSqlConnectionContext;

        public CarPoolTicketRepository(SqlConnectionContext oSqlConnectionContext) => _oSqlConnectionContext = oSqlConnectionContext;

        //Get all Car Pool Tickets
        async Task<IEnumerable<FindCarPoolTicket>> ICarPoolTicketRepository.FindCarPoolTickets(SearchCarPoolTicketsDto searchCarPoolTicketsDto, int id)
        {
            string sProcedureName = "FindCarPoolTickets";
            var parameters = new DynamicParameters();

            parameters.Add("@searchText", searchCarPoolTicketsDto.SearchText, DbType.String);
            parameters.Add("@OwnerId", id, DbType.Int32);

            using (var sConnection = _oSqlConnectionContext.CreateConnection())
            {
                var CarPoolTickets = await sConnection.QueryAsync<FindCarPoolTicket>(sProcedureName, parameters, commandType: CommandType.StoredProcedure);

                return CarPoolTickets.ToList();
            }
        }

        async Task<IEnumerable<CarPoolTicket>> ICarPoolTicketRepository.GetCreatedCarPoolTicketsByUserId(int id)
        {
            string sProcedureName = "GetCreatedCarPoolTicketsByUserId";

            using (var sConnection = _oSqlConnectionContext.CreateConnection())
            {
                DynamicParameters oParameters = new DynamicParameters();
                oParameters.Add("OwnerId", id, DbType.Int32);

                var CarPoolTickets = await sConnection.QueryAsync<CarPoolTicket>(sProcedureName, oParameters, commandType: CommandType.StoredProcedure);
                
                return CarPoolTickets.ToList();
            }
        }
        

        //Create a Car Pool Ticket
        async Task<int> ICarPoolTicketRepository.CreateCarPoolTicket(CreateCarPoolTicket oCreateCarPoolTicket, int id)
        {
            string sProcedureName = "CreateCarPoolTicket";
            var oParameters = new DynamicParameters();

            oParameters.Add("OwnerId", id, DbType.Int32);
            oParameters.Add("Origin", oCreateCarPoolTicket.Origin, DbType.String);
            oParameters.Add("Destination", oCreateCarPoolTicket.Destination, DbType.String);
            oParameters.Add("DepartureTime", oCreateCarPoolTicket.DepartureTime, DbType.DateTime);
            oParameters.Add("ExpectedArrivalTime", oCreateCarPoolTicket.ExpectedArrivalTime, DbType.DateTime);
            oParameters.Add("AvailableSeats", oCreateCarPoolTicket.AvailableSeats, DbType.Int32);
            oParameters.Add("Notes", oCreateCarPoolTicket.Notes, DbType.String);

            using (var sConnection = _oSqlConnectionContext.CreateConnection())
            {
                var joinedCarPoolTicketId = await sConnection.ExecuteAsync(sProcedureName, oParameters, commandType: CommandType.StoredProcedure);
                return joinedCarPoolTicketId;
            }
        }

        //Join on a Car Pool Ticket
        async Task<int> ICarPoolTicketRepository.JoinCarPoolTicket(FunctionCommandCarPoolTicketDto oFunctionCommandCarPoolTicketDto, int id)
        {
            string sProcedureName = "JoinCarPoolTicket";
            var oParameters = new DynamicParameters();

            oParameters.Add("CarPoolTicketId",id, DbType.Int32);
            oParameters.Add("OwnerId", oFunctionCommandCarPoolTicketDto.OwnerId, DbType.Int32);
            oParameters.Add("PassengerNote", oFunctionCommandCarPoolTicketDto.PassengerNote, DbType.String);

            using (var sConnection = _oSqlConnectionContext.CreateConnection())
            {
                int joinedCarPoolTicketId = await sConnection.ExecuteScalarAsync<int>(sProcedureName, oParameters, commandType: CommandType.StoredProcedure);
                return joinedCarPoolTicketId;
            }
        }

        //Cancel Join on a Car Pool Ticket
        async Task<int> ICarPoolTicketRepository.CancelJoinCarPoolTicket(FunctionCommandCarPoolTicketDto oFunctionCommandCarPoolTicketDto, int id)
        {
            string sProcedureName = "CancelJoinCarPoolTicket";
            var oParameters = new DynamicParameters();

            oParameters.Add("CarPoolTicketId", id, DbType.Int32);
            oParameters.Add("OwnerId", oFunctionCommandCarPoolTicketDto.OwnerId, DbType.Int32);

            using (var sConnection = _oSqlConnectionContext.CreateConnection())
            {
               var cancelledCarppolTicketId = await sConnection.ExecuteScalarAsync<int>(sProcedureName, oParameters, commandType: CommandType.StoredProcedure);
                return cancelledCarppolTicketId;
            }
        }

        //Get All Details Per Car Pool Ticket
        async Task<IEnumerable<CarPoolTicketDetails>> ICarPoolTicketRepository.GetCarPoolTicketDetailsById(int vid)
        {
            string sProcedureName = "GetCarPoolTicketDetailsById";
            var parameters = new DynamicParameters();
            parameters.Add("@OwnerId", vid, DbType.Int32);

            using (var sConnection = _oSqlConnectionContext.CreateConnection())
            {
                var lookup = new Dictionary<int, UserDetail>();

                var CarPoolTicket = await sConnection.QueryAsync<CarPoolTicketDetails, UserDetail, CarPoolTicketDetails>(sProcedureName, (cpt, u) =>
                    {
                        UserDetail udetail;
                        if (!lookup.TryGetValue(cpt.Id, out udetail))
                        {
                            lookup.Add(cpt.Id, udetail = u);
                        }
                        if (cpt.Passengers == null)
                        {
                            cpt.Passengers = new List<UserDetail>();
                        }
                        if (u.Name != null)
                        {
                            cpt.Passengers.Add(u);
                        }
                        return cpt;
                    }, parameters, commandType: CommandType.StoredProcedure
                );
                
                
                return CarPoolTicket.ToList();
            }
        }

        async Task<int> ICarPoolTicketRepository.CancelCreatedCarPoolTicketID(FunctionCommandUser functionCommandUser, int id)
        {
            string procedureName = "CancelCreatedCarPoolTicket";
            var parameters = new DynamicParameters();

            parameters.Add("OwnerId", functionCommandUser.UserId, DbType.Int32);
            parameters.Add("CarPoolTicketId", id, DbType.Int32);

            using (var connection = _oSqlConnectionContext.CreateConnection())
            {
                var carPoolTicketStatus = await connection.ExecuteScalarAsync<int>(procedureName, parameters, commandType: CommandType.StoredProcedure);
                return carPoolTicketStatus;
            }
        }

        async Task<int> ICarPoolTicketRepository.UpdateCarPoolTicketDetails(UpdateCarPoolTicketDetailsDto updateCarPoolTicketDetailsDto, int id)
        {
            string procedureName = "UpdateCarPoolTicketDetails";
            var parameters = new DynamicParameters();

            parameters.Add("CarPoolTicketId", id, DbType.Int32);
            parameters.Add("Origin", updateCarPoolTicketDetailsDto.Origin, DbType.String);
            parameters.Add("Destination", updateCarPoolTicketDetailsDto.Destination, DbType.String);
            parameters.Add("DepartureTime", updateCarPoolTicketDetailsDto.DepartureTime, DbType.DateTime);
            parameters.Add("ExpectedArrivalTime", updateCarPoolTicketDetailsDto.ExpectedArrivalTime, DbType.DateTime);
            parameters.Add("AvailableSeats", updateCarPoolTicketDetailsDto.AvailableSeats, DbType.Int32);
            parameters.Add("Notes", updateCarPoolTicketDetailsDto.Notes, DbType.String);

            using (var connection = _oSqlConnectionContext.CreateConnection())
            {
                var carPoolTicket = await connection.ExecuteScalarAsync<int>(procedureName, parameters, commandType: CommandType.StoredProcedure);
                return carPoolTicket;
            }
        }

        async Task<IEnumerable<CarPoolTicket>> ICarPoolTicketRepository.GetRegisteredCarPoolTicketsByUserId(int id)
         {
            string procedureName = "GetRegisteredCarPoolTicketsbyUserId";

            using (var sConnection = _oSqlConnectionContext.CreateConnection())
            {
                DynamicParameters oParameters = new DynamicParameters();
                oParameters.Add("CarPoolTicketPassengerID", id, DbType.Int32);

                var CarPoolTickets = await sConnection.QueryAsync<CarPoolTicket>(procedureName, oParameters, commandType: CommandType.StoredProcedure);

                return CarPoolTickets.ToList();
            }
        }
    }
}
