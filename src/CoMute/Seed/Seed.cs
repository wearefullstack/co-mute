using Bogus;
using CoMute.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace CoMute.Web.Seed
{
    public static class SeedData
    {
        public static List<User> Users { get; set; } = new List<User>();
        public static List<CarPool> CarPools { get; set; } = new List<CarPool>();
        public static List<AvailableDay> AvailableDays { get; set; } = new List<AvailableDay>();

        private static string[] origin = new[] { "Johannesburg CBD", "Soweto", "Daveyton", "Sandton", "Meyerton",
            "Benoni", "Pretoria CBD", "Boksburg", "Rosebank", "Randburg"};

        private static string[] destination = new[] { "Vaal", "Sun City", "Katlehong", "Silver Lake Estates", "Centurion",
            "Rynfield", "Mbomebela", "Sasolburg", "Tzaneen", "Malamulele"};

        public static void Init(int numberofUsers)
        {
            int availableDayId = 1;
            Faker<AvailableDay> availableDayFaker = new Faker<AvailableDay>()
                .RuleFor(a => a.Id, _ => availableDayId++)
                .RuleFor(a => a.Day, f => f.PickRandom<DayEnumeration>());

            int carPoolId = 1;
            int days = 1;
            Faker<CarPool> carPoolsFaker = new Faker<CarPool>()
                .RuleFor(c => c.Id, _ => carPoolId++)
                .RuleFor(c => c.DepartureTime, _ => DateTime.Now.AddDays(days++))
                .RuleFor(c => c.ExpectedArrivalTime, (f, c) => c.DepartureTime.AddHours(4))
                .RuleFor(c => c.Origin, f => f.PickRandom(origin))
                .RuleFor(c => c.Destination, f => f.PickRandom(destination))
                .RuleFor(c => c.MaximumSeats, f => f.Random.Number(min: 1, max: 15))
                .RuleFor(c => c.AvailableSeats, (f, c) => c.MaximumSeats)
                .RuleFor(c => c.CreatedDate, _ => DateTime.Now)
                .RuleFor(c => c.AvailableDays, (f, c) =>
                {
                    availableDayFaker.RuleFor(a => a.CarPoolId, _ => c.Id);
                    var availableDays = availableDayFaker.GenerateBetween(min: 2, max: 4);
                    AvailableDays.AddRange(availableDays);
                    return null;
                });

            int Id = 1;
            Faker<User> usersFaker = new Faker<User>()
                .RuleFor(u => u.Id, _ => Id++)
                .RuleFor(u => u.Name, (f, u) => f.Name.FirstName())
                .RuleFor(u => u.Surname, (f, u) => f.Name.LastName())
                .RuleFor(u => u.EmailAddress, (f, u) => f.Internet.Email(u.Name))
                .RuleFor(u => u.Password, (f, u) => f.Random.String(length: 8, minChar: '0', maxChar: ']'))
                .RuleFor(u => u.CarPools, (f, u) =>
                {
                    carPoolsFaker.RuleFor(c => c.UserId, _ => u.Id);
                    var carPools = carPoolsFaker.GenerateBetween(min: 3, max: 5);
                    CarPools.AddRange(carPools);

                    return null;
                });

            var users = usersFaker.Generate(numberofUsers);
            Users.AddRange(users);
        }
    }
}