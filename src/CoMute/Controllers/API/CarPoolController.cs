using CoMute.Web.Data;
using CoMute.Web.Models;
using CoMute.Web.Models.Dto;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;

namespace CoMute.Web.Controllers.API
{
    public class CarPoolController : ApiController
    {
        private readonly ComuteContext _comuteContext;

        public CarPoolController()
        {
            _comuteContext = new ComuteContext();
        }

        [Route("api/carpools")]
        [HttpGet]
        public HttpResponseMessage Get()
        {
            List<CarPool> carpools = _comuteContext.CarPools.ToList();
            List<CarPoolDto> carPoolDtos = new List<CarPoolDto>();
            if(carpools != null)
            {
                foreach(CarPool carpool in carpools)
                {
                    _comuteContext.Entry(carpool).Collection(s => s.AvailableDays).Load();
                    CarPoolDto carPoolDto = new CarPoolDto
                    {
                        Id = carpool.Id,
                        UserId = carpool.UserId,
                        AvailableDays = Converters.Converters.ConvertAvailableDaysToDto(carpool.AvailableDays),
                        AvailableSeats = carpool.AvailableSeats,
                        DepartureTime = carpool.DepartureTime,
                        ExpectedArrivalTime = carpool.ExpectedArrivalTime,
                        Origin = carpool.Origin,
                        Destination = carpool.Destination,
                        Notes = carpool.Notes,
                        DateCreated = carpool.CreatedDate
                    };
                    carPoolDtos.Add(carPoolDto);
                }
                return Request.CreateResponse(HttpStatusCode.OK, carPoolDtos);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
        }

        [Route("api/carpool/{id}")]
        [HttpGet]
        public async Task<HttpResponseMessage> Get(int id)
        {
            CarPool carpool = await _comuteContext.CarPools.FindAsync(id);
            if(carpool != null)
            {
                _comuteContext.Entry(carpool).Collection(s => s.AvailableDays).Load();
                CarPoolDto carPoolDto = new CarPoolDto
                {
                    Id = carpool.Id,
                    UserId = carpool.UserId,
                    AvailableDays = Converters.Converters.ConvertAvailableDaysToDto(carpool.AvailableDays),
                    AvailableSeats = carpool.AvailableSeats,
                    DepartureTime = carpool.DepartureTime,
                    ExpectedArrivalTime = carpool.ExpectedArrivalTime,
                    Origin = carpool.Origin,
                    Destination = carpool.Destination,
                    Notes = carpool.Notes,
                    DateCreated = carpool.CreatedDate
                };
                return Request.CreateResponse(HttpStatusCode.OK, carPoolDto);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
        }

        [Route("api/user/{id}/carpools")]
        [HttpGet]
        public HttpResponseMessage GetUserCarPools(int id)
        {
            ICollection<CarPool> carpools = _comuteContext.CarPools.Where(s => s.UserId == id).ToList();
            List<CarPoolDto> carPoolDtos = new List<CarPoolDto>();
            if (carpools != null)
            {
                foreach (CarPool carpool in carpools)
                {
                    _comuteContext.Entry(carpool).Collection(s => s.AvailableDays).Load();
                    CarPoolDto carPoolDto = new CarPoolDto
                    {
                        Id = carpool.Id,
                        UserId = carpool.UserId,
                        AvailableDays = Converters.Converters.ConvertAvailableDaysToDto(carpool.AvailableDays),
                        AvailableSeats = carpool.AvailableSeats,
                        DepartureTime = carpool.DepartureTime,
                        ExpectedArrivalTime = carpool.ExpectedArrivalTime,
                        Origin = carpool.Origin,
                        Destination = carpool.Destination,
                        Notes = carpool.Notes,
                        DateCreated = carpool.CreatedDate
                    };
                    carPoolDtos.Add(carPoolDto);
                }

                return Request.CreateResponse(HttpStatusCode.OK, carPoolDtos);
            }
            return Request.CreateResponse(HttpStatusCode.NotFound);
        }

        [Route("api/user/{id}/carpool/memberships")]
        [HttpGet]
        public async Task<HttpResponseMessage> GetCarPoolMemberships(int id)
        {
            User user = await _comuteContext.Users.FindAsync(id);
            if (user != null)
            {
                _comuteContext.Entry(user).Collection(s => s.CarPoolMemberships).Load();
                if (user.CarPoolMemberships.Count == 0)
                {
                    return Request.CreateResponse(HttpStatusCode.NoContent);
                }
                else
                {
                    List<CarPoolDto> carPoolDtos = new List<CarPoolDto>();
                    foreach (CarPoolMembership membership in user.CarPoolMemberships)
                    {
                        CarPool carpool = _comuteContext.CarPools.Find(membership.CarPoolId);
                        if(carpool != null)
                        {
                            CarPoolDto carPoolDto = new CarPoolDto
                            {
                                Id = carpool.Id,
                                UserId = carpool.UserId,
                                AvailableDays = Converters.Converters.ConvertAvailableDaysToDto(carpool.AvailableDays),
                                AvailableSeats = carpool.AvailableSeats,
                                DepartureTime = carpool.DepartureTime,
                                ExpectedArrivalTime = carpool.ExpectedArrivalTime,
                                Origin = carpool.Origin,
                                Destination = carpool.Destination,
                                Notes = carpool.Notes,
                                DateCreated = carpool.CreatedDate,
                                DateJoined = membership.DateJoined,
                                CarPoolMembershipId = membership.Id
                        };
                            carPoolDtos.Add(carPoolDto);
                        }
                    }

                    return Request.CreateResponse(HttpStatusCode.OK, carPoolDtos);
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
        }

        [Route("api/user/{userId}/carpools/available")]
        [HttpGet]
        public HttpResponseMessage GetAvailableCarPools(int userId)
        {
            User user =_comuteContext.Users.Find(userId);
            if(user == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Could not find user");
            }

            List<CarPool> carpools = _comuteContext.CarPools.Where(x => x.UserId != userId).ToList();
            List<CarPoolDto> carPoolDtos = new List<CarPoolDto>();
            if (carpools != null)
            {
                foreach (CarPool carpool in carpools)
                {
                    _comuteContext.Entry(carpool).Collection(s => s.AvailableDays).Load();
                    CarPoolDto carPoolDto = new CarPoolDto
                    {
                        Id = carpool.Id,
                        UserId = carpool.UserId,
                        AvailableDays = Converters.Converters.ConvertAvailableDaysToDto(carpool.AvailableDays),
                        AvailableSeats = carpool.AvailableSeats,
                        DepartureTime = carpool.DepartureTime,
                        ExpectedArrivalTime = carpool.ExpectedArrivalTime,
                        Origin = carpool.Origin,
                        Destination = carpool.Destination,
                        Notes = carpool.Notes,
                        DateCreated = carpool.CreatedDate
                    };
                    carPoolDtos.Add(carPoolDto);
                }

                _comuteContext.Entry(user).Collection(s => s.CarPoolMemberships).Load();
                if (user.CarPoolMemberships.Count > 0)
                {
                    List<CarPoolDto> result = carPoolDtos.
                        Where(x => !user.CarPoolMemberships.Any(p => p.CarPoolId == x.Id && p.UserId == x.UserId)).ToList();
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }

                return Request.CreateResponse(HttpStatusCode.OK, carPoolDtos);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
        }

        [Route("api/user/{userId}/carpools/filter")]
        [HttpGet]
        public HttpResponseMessage FilterAvailableCarPools(int userId, string origin, string destination)
        {
            User user = _comuteContext.Users.Find(userId);
            if (user == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "Could not find user");
            }

            if (string.IsNullOrEmpty(origin))
            {
                origin = string.Empty;
            }

            if (string.IsNullOrEmpty(destination))
            {
                destination = string.Empty;
            }

            List<CarPool> carpools = _comuteContext.CarPools.
                Where(x => x.UserId != userId).ToList();

            List<CarPoolDto> carPoolDtos = new List<CarPoolDto>();
            if (carpools != null)
            {
                carpools = carpools.Where(x => x.Origin.ToLower().Contains(origin.ToLower()) && x.Destination.ToLower().Contains(destination.ToLower())).ToList();

                foreach (CarPool carpool in carpools)
                {
                    if(carpool.AvailableSeats > 0)
                    {
                        _comuteContext.Entry(carpool).Collection(s => s.AvailableDays).Load();
                        CarPoolDto carPoolDto = new CarPoolDto
                        {
                            Id = carpool.Id,
                            UserId = carpool.UserId,
                            AvailableDays = Converters.Converters.ConvertAvailableDaysToDto(carpool.AvailableDays),
                            AvailableSeats = carpool.AvailableSeats,
                            DepartureTime = carpool.DepartureTime,
                            ExpectedArrivalTime = carpool.ExpectedArrivalTime,
                            Origin = carpool.Origin,
                            Destination = carpool.Destination,
                            Notes = carpool.Notes,
                            DateCreated = carpool.CreatedDate
                        };
                        carPoolDtos.Add(carPoolDto);
                    }
                }

                _comuteContext.Entry(user).Collection(s => s.CarPoolMemberships).Load();
                if (user.CarPoolMemberships.Count > 0)
                {
                    List<CarPoolDto> result = carPoolDtos.
                        Where(x => !user.CarPoolMemberships.Any(p => p.CarPoolId == x.Id && p.UserId == x.UserId)).ToList();
                    return Request.CreateResponse(HttpStatusCode.OK, result);
                }

                return Request.CreateResponse(HttpStatusCode.OK, carPoolDtos);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }
        }


        [Route("api/carpool/add")]
        [HttpPost]
        public async Task<HttpResponseMessage> Post(CarPoolDto carPoolDto)
        {
            User user = _comuteContext.Users.Find(carPoolDto.UserId);
            if(user == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "User does not exist");
            }

            CarPool carPool = new CarPool()
            {
                DepartureTime = carPoolDto.DepartureTime,
                ExpectedArrivalTime = carPoolDto.ExpectedArrivalTime,
                Origin = carPoolDto.Origin,
                Destination =carPoolDto.Destination,
                AvailableSeats = carPoolDto.AvailableSeats,
                Notes = carPoolDto.Notes,
                UserId = carPoolDto.UserId,
                AvailableDays = Converters.Converters.ConvertDtoToAvailableDays(carPoolDto.AvailableDays),
                CreatedDate = DateTime.Now,
                MaximumSeats = carPoolDto.MaximumSeats
            };

            _comuteContext.CarPools.Add(carPool);
            await _comuteContext.SaveChangesAsync();

            _comuteContext.Entry(carPool).Collection(s => s.AvailableDays).Load();
            CarPoolDto savedCarPool = new CarPoolDto
            {
                Id = carPool.Id,
                UserId = carPool.UserId,
                AvailableDays = Converters.Converters.ConvertAvailableDaysToDto(carPool.AvailableDays),
                AvailableSeats = carPool.AvailableSeats,
                DepartureTime = carPool.DepartureTime,
                ExpectedArrivalTime = carPool.ExpectedArrivalTime,
                Origin = carPool.Origin,
                Destination = carPool.Destination,
                Notes = carPool.Notes
            };

            return Request.CreateResponse(HttpStatusCode.Created, savedCarPool);
        }

        [Route("api/carpool/membership/add")]
        [HttpPost]
        public HttpResponseMessage PostMembership(CarPoolMembership membership)
        {
            CarPoolMembership exists = _comuteContext.CarPoolMemberships.FirstOrDefault(x => x.UserId == membership.UserId && x.CarPoolId == membership.CarPoolId);

            if(exists == null)
            {

                CarPool carPool = _comuteContext.CarPools.Find(membership.CarPoolId);
                if (carPool.AvailableSeats > 0)
                {
                    carPool.AvailableSeats--;
                    _comuteContext.Entry(carPool).State = EntityState.Modified;
                    membership = _comuteContext.CarPoolMemberships.Add(membership);
                    EntityState state = _comuteContext.Entry(membership).State;
                    _comuteContext.SaveChanges();
                    if (state == EntityState.Added)
                    {
                        return Request.CreateResponse(HttpStatusCode.Created, membership);
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.BadRequest, "Could  not add membership");
                    }
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, "Car pool has reached capacity");
                }
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, "You are already a member of this car pool");
            }
        }

        [Route("api/user/{userId}/carpool/membership/{membershipId}")]
        [HttpDelete]
        public HttpResponseMessage Delete(int userId, int membershipId)
        {
            User user = _comuteContext.Users.Find(userId);
            if(user == null)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
            CarPoolMembership membership = _comuteContext.CarPoolMemberships.Find(membershipId);
            if(membership != null)
            {
                CarPoolMembership removed = _comuteContext.CarPoolMemberships.Remove(membership);
                if(removed != null)
                {
                    CarPool carPool = _comuteContext.CarPools.Find(removed.CarPoolId);
                    carPool.AvailableSeats++;
                    _comuteContext.Entry(carPool).State = EntityState.Modified;
                    _comuteContext.SaveChanges();
                    return Request.CreateResponse(HttpStatusCode.OK, "Deleted");
                }
            }
            return Request.CreateResponse(HttpStatusCode.NotFound, "Could not delete");
        }
    }
}
