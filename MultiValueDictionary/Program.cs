using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Events;
using System;

namespace MultiValueDictionary
{
    class Program
    {
        static void Main(string[] args)
        {
            var configuration = new ConfigurationBuilder().AddJsonFile("appSettings.json", true, true).Build();

            //setup DI
            var serviceProvider = new ServiceCollection()
            .AddLogging(builder => builder.AddSerilog(
                    new LoggerConfiguration()
                        .ReadFrom.Configuration(configuration)
                        .WriteTo.File($"MultiValueDictionaryTool_{DateTime.Now.ToFileTime()}.log", LogEventLevel.Verbose)
                        .CreateLogger()))

            .AddSingleton<IGenericMultiValueDictionary<string, string>, GenericMultiValueDictionary<string, string>>()
            .AddSingleton<IGenericMultiValueDictionary<int, int>, GenericMultiValueDictionary<int, int>>()
            .BuildServiceProvider();

            //Create logger to use in main method
            var logger = serviceProvider.GetService<ILogger<IGenericMultiValueDictionary<string, string>>>();

            logger.LogInformation("Application Started");

            //service which supports key and value to be string type
            var genericDictService = serviceProvider.GetService<IGenericMultiValueDictionary<string, string>>();

            //service which supports key and value to be of int type
            //var genericIntDictService = serviceProvider.GetService<IGenericMultiValueDictionary<int, int>>();

            //Execute Commands for service which supports key and value to be string
            RunGeneric(genericDictService);

            //Execute Commands for service which supports key and value to be int
            //RunGenericInt(genericIntDictService);


        }

        public static void RunGenericInt(IGenericMultiValueDictionary<int, int> service)
        {
            while (true)
            {
                Console.Write(">");
                string input = Console.ReadLine();
                string[] args = input.Split(' ');


                string command = args[0];

                switch (command)
                {
                    case "ADD" when args.Length == 3 && ((!string.IsNullOrEmpty(args[1])) && (!string.IsNullOrEmpty(args[2]))):
                        bool isNumericKey = int.TryParse(args[1], out int key);
                        bool isNumericVal = int.TryParse(args[2], out int val);
                        if ((!isNumericKey) || (!isNumericVal))
                        {
                            Console.WriteLine("Invalid command");
                            break;
                        }
                        service.Add(int.Parse(args[1]), int.Parse(args[2]));
                        break;
                    case "MEMBERS" when args.Length == 2 && (!string.IsNullOrEmpty(args[1])):
                        var members = service.GetMembers(int.Parse(args[1]));
                        service.DisplayMembers(members);
                        break;
                    case "KEYS" when args.Length == 1 && (!string.IsNullOrEmpty(args[0])):
                        service.GetKeys();
                        break;
                    case "REMOVE" when args.Length == 3 && ((!string.IsNullOrEmpty(args[1])) && (!string.IsNullOrEmpty(args[2]))):
                        service.RemoveMember(int.Parse(args[1]), int.Parse(args[2]));
                        break;
                    case "REMOVEALL" when args.Length == 2 && (!string.IsNullOrEmpty(args[1])):
                        service.RemoveKey(int.Parse(args[1]));
                        break;
                    case "CLEAR" when args.Length == 1 && (!string.IsNullOrEmpty(args[0])):
                        service.ClearAll();
                        break;
                    case "KEYEXISTS" when args.Length == 2 && (!string.IsNullOrEmpty(args[1])):
                        var keyExists = service.KeyExists(int.Parse(args[1]));
                        Console.WriteLine(keyExists);
                        break;
                    case "MEMBEREXISTS" when args.Length == 3 && ((!string.IsNullOrEmpty(args[1])) && (!string.IsNullOrEmpty(args[2]))):
                        var memberExists = service.MemberExists(int.Parse(args[1]), int.Parse(args[2]));
                        Console.WriteLine(memberExists);
                        break;
                    case "ALLMEMBERS" when args.Length == 1 && (!string.IsNullOrEmpty(args[0])):
                        var allMembers = service.GetAllMembers();
                        service.DisplayMembers(allMembers);
                        break;
                    case "ITEMS" when args.Length == 1 && (!string.IsNullOrEmpty(args[0])):
                        service.GetItems();
                        break;
                    default:
                        Console.WriteLine("Invalid command");
                        break;
                }

            }
        }


        public static void RunGeneric(IGenericMultiValueDictionary<string, string> service)
        {
            while (true)
            {
                Console.Write(">");
                string input = Console.ReadLine();
                string[] args = input.Split(' ');

                string command = args[0];

                switch (command)
                {
                    case "ADD" when args.Length == 3 && ((!string.IsNullOrEmpty(args[1])) && (!string.IsNullOrEmpty(args[2]))):
                        service.Add(args[1], args[2]);
                        break;
                    case "MEMBERS" when args.Length == 2 && (!string.IsNullOrEmpty(args[1])):
                        var members = service.GetMembers(args[1]);
                        service.DisplayMembers(members);
                        break;

                    case "KEYS" when args.Length == 1 && (!string.IsNullOrEmpty(args[0])):
                        service.GetKeys();
                        break;
                    case "REMOVE" when args.Length == 3 && ((!string.IsNullOrEmpty(args[1])) && (!string.IsNullOrEmpty(args[2]))):
                        service.RemoveMember(args[1], args[2]);
                        break;
                    case "REMOVEALL" when args.Length == 2 && (!string.IsNullOrEmpty(args[1])):
                        service.RemoveKey(args[1]);
                        break;
                    case "CLEAR" when args.Length == 1 && (!string.IsNullOrEmpty(args[0])):
                        service.ClearAll();
                        break;
                    case "KEYEXISTS" when args.Length == 2 && (!string.IsNullOrEmpty(args[1])):
                        var keyExists = service.KeyExists(args[1]);
                        Console.WriteLine(keyExists);
                        break;
                    case "MEMBEREXISTS" when args.Length == 3 && ((!string.IsNullOrEmpty(args[1])) && (!string.IsNullOrEmpty(args[2]))):
                        var memberExists = service.MemberExists(args[1], args[2]);
                        Console.WriteLine(memberExists);
                        break;
                    case "ALLMEMBERS" when args.Length == 1 && (!string.IsNullOrEmpty(args[0])):
                        var allMembers = service.GetAllMembers();
                        service.DisplayMembers(allMembers);
                        break;
                    case "ITEMS" when args.Length == 1 && (!string.IsNullOrEmpty(args[0])):
                        service.GetItems();
                        break;
                    default:
                        Console.WriteLine("Invalid command");
                        break;

                }

            }
        }

    }
}
