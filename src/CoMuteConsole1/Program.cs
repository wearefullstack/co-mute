using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ansyl;
using CoMute.Lib;
using CoMute.Lib.Dao;
using CoMute.Lib.Dao.comute;
using CoMute.Lib.Dto;
using CoMute.Lib.services;
using Dapper;
using MySql.Data.MySqlClient;

namespace CoMuteConsole1
{
    class Program
    {
        static void Main(string[] args)
        {
            ExecuteAction(GetUser);
            ExecuteAction(AddUser);
            ExecuteAction(AddUser);
            ExecuteAction(GetUser);
            ExecuteAction(CreatePool);
            //ExecuteAction(CreatePool);
            //ExecuteAction(CreatePool);
            //ExecuteAction(CreatePool);
            ExecuteAction(JoinPool);
            ExecuteAction(CreatePool);
            return;
            //ExecuteAction(JoinPool);
            ExecuteAction(GetOwnedPools);
            return;

            AddUser();

            var dto = new PoolDto
            {
                DepartTime = "09:0:00",
                ArriveTime = "10:01",
                OwnerId = 1,
                AvailableSeats = 3,
                CreatedTime = DateTime.Now,
                Destination = "Stellenbosch",
                Origin = "Mowbray",
                AvailableDays = new[] { "Monday", "Tuesday", "Wednesday" },
                Notes = null,
                PoolId = 0
            };

            var pool = new UserService().JoinPool(1, 5);


        }

        static void AddUser()
        {
            //var user = new UserService().AddUser(email: "isaac.inyang@gmail.com",
            //                                     password: "secret",
            //                                     name: "Isaac",
            //                                     surname: "Inyang",
            //                                     phone: null
            //                                    );

            //Obj.ShowObjectH(user);
        }

        static void GetUser()
        {
            var user = new UserService().GetLogin(email: "isaac.inyang@gmail.comt",
                                                 password: "secreta"
                                                );

            Obj.ShowObjectH(user);
        }

        static void CreatePool()
        {
            var dto = new PoolDto
            {
                DepartTime = "09:30:00",
                ArriveTime = "10:41",
                OwnerId = 1,
                AvailableSeats = 3,
                CreatedTime = DateTime.Now,
                Destination = "Stellenbosch",
                Origin = "Mowbray",
                AvailableDays = new[] { "Monday", "Tuesday", "Wednesday" },
                Notes = null,
                PoolId = 0
            };

            var pool = new UserService().CreatePool(dto);
        }

        static void GetOwnedPools()
        {
            var ownerPools = new UserService().GetOwnedPools(1);

            Obj.ShowObjectH(ownerPools);
        }

        public static void ExecuteAction(Action action)
        {
            var time1 = DateTime.Now;

            try
            {
                var method = action.Method;
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("EXECUTING METHOD: {0}.{1}", method.DeclaringType?.FullName, method.Name);
                Console.ResetColor();

                action.Invoke();
            }
            catch (NetQueException exception)
            {
                Console.WriteLine(exception.Message);
            }
            catch (Exception exception)
            {
                Console.WriteLine(exception);
            }
            finally
            {
            }
        }

        static void JoinPool()
        {
            new UserService().JoinPool(1, 2);
        }

        static void LeavePool()
        {
            new UserService().LeavePool(1, 2);
        }
    }
}
