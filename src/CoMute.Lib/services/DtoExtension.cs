using System;
using System.Collections.Generic;
using System.Linq;
using CoMute.Lib.Dao.comute;
using CoMute.Lib.Dto;

namespace CoMute.Lib.services
{
    static class DtoExtension
    {
        public static PoolDto ToDto(this Pool pool)
        {
            var dto = pool.CopyPropertiesTo<PoolDto>();
            dto.DepartTime = pool.DepartTime.ToString();
            dto.ArriveTime = pool.ArriveTime.ToString();
            dto.Route = $"{pool.Origin} - {pool.Destination}";
            dto.Duration = $@"{pool.DepartTime:hh\:mm} - {pool.ArriveTime:hh\:mm}";
            dto.AvailableDays = pool.AvailableDays.Split(',');
            return dto;
        }

        public static Pool ToDao(this PoolDto dto)
        {
            if (TimeSpan.TryParse(dto.DepartTime, out var dTime) == false)
                throw new Exception("Bad time format");

            if (TimeSpan.TryParse(dto.ArriveTime, out var aTime) == false)
                throw new Exception("Bad time format");

            if (dTime > aTime)
                throw new Exception("Check the time duration");

            if (dto.AvailableDays == null || dto.AvailableDays.Any() == false)
                throw new Exception("Select available days");
            //if (dto.AvailableDays.Length == 0)
            //    throw new Exception("No available days specified");

            var pool = dto.CopyPropertiesTo<Pool>();
            pool.ArriveTime = aTime;
            pool.DepartTime = dTime;
            pool.AvailableDays = string.Join(",", dto.AvailableDays ?? new string[] { });
            return pool;
        }

        public static UserDto ToDto(this User user)
        {
            var dto = user.CopyPropertiesTo<UserDto>();
            return dto;
        }

        public static User ToDao(this UserDto dto)
        {
            var dao = dto.CopyPropertiesTo<User>();
            return dao;
        }

        public static IList<T> AddSn<T>(this IEnumerable<T> list) where T : BaseSn
        {
            var items = list.ToList();

            for (var i = 0; i < items.Count; i++)
            {
                items[i].Sn = i + 1;
            }

            return items.ToList();
        }
    }
}