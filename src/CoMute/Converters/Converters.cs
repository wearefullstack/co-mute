using CoMute.Web.Models;
using CoMute.Web.Models.Dto;
using System.Collections.Generic;

namespace CoMute.Web.Converters
{
    public static class Converters
    {
        public static ICollection<AvailableDayDto> ConvertAvailableDaysToDto(ICollection<AvailableDay> availableDays)
        {
            ICollection<AvailableDayDto> data = new List<AvailableDayDto>();
            foreach (AvailableDay availableDay in availableDays)
            {
                data.Add(new AvailableDayDto()
                {
                    Id = availableDay.Id,
                    Day = availableDay.Day,
                    CarPoolId = availableDay.CarPoolId
                });
            }
            return data;
        }

        public static ICollection<AvailableDay> ConvertDtoToAvailableDays(ICollection<AvailableDayDto> availableDays)
        {
            ICollection<AvailableDay> data = new List<AvailableDay>();
            foreach (AvailableDayDto availableDay in availableDays)
            {
                data.Add(new AvailableDay()
                {
                    Id = availableDay.Id,
                    Day = availableDay.Day,
                    CarPoolId = availableDay.CarPoolId
                });
            }
            return data;
        }
    }
}